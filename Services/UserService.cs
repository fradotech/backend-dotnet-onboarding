using Iam.Models;
using Microsoft.EntityFrameworkCore;

namespace Iam.Services;

public partial class UserService(AppDbContext context, QueueService queueService)
{
    private readonly AppDbContext _context = context;
    private readonly QueueService _queueService = queueService;

    public async Task<List<User>> GetAll()
    {
        return await _context.Users
            .Include(u => u.Role)
            .ToListAsync();
    }

    public async Task Create(User user)
    {
        if (user.RoleId != null)
        {
            user.Role = await _context.Roles.FindAsync(user.RoleId) ?? throw new Exception("Role not found");
        }

        _context.Users.Add(user);
        await _context.SaveChangesAsync();
        await _queueService.SendMessageAsync("User created!", user);
    }

    public async Task<User?> Get(int id)
    {
        return await _context.Users
            .Include(u => u.Role)
            .FirstOrDefaultAsync(p => p.Id == id);
    }

    public async Task Update(User user, User userRequest)
    {
        if (userRequest.RoleId != null)
        {
            user.Role = await _context.Roles.FindAsync(userRequest.RoleId) ?? throw new Exception("Role not found");
        }

        user.Name = userRequest.Name;
        user.IsActive = userRequest.IsActive;

        _context.Entry(userRequest).State = EntityState.Modified;
        await _context.SaveChangesAsync();
        await _queueService.SendMessageAsync("User updated!", userRequest);
    }

    public async Task Delete(int id)
    {
        var user = await Get(id);
        if (user is null) return;
        _context.Users.Remove(user);
        await _context.SaveChangesAsync();
    }
}

public partial class UserService
{
    public async Task<bool> UpdateIsActive(int userId, bool isActive)
    {
        var user = await _context.Users.FindAsync(userId);
        if (user == null) return false;

        user.IsActive = isActive;
        _context.Entry(user).State = EntityState.Modified;

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!UserExists(userId)) return false;
            else throw;
        }

        return true;
    }

    private bool UserExists(int id)
    {
        return _context.Users.Any(e => e.Id == id);
    }
}