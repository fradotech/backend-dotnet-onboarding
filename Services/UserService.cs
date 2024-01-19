using Iam.Models;
using Microsoft.EntityFrameworkCore;

namespace Iam.Services;

public partial class UserService
{
    private readonly AppDbContext _context;
    private readonly QueueService _queueService;

    public UserService(AppDbContext context, QueueService queueService)
    {
        _context = context;
        _queueService = queueService;
    }

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

    public async Task Update(User user)
    {
        if (user.RoleId != null)
        {
            user.Role = await _context.Roles.FindAsync(user.RoleId) ?? throw new Exception("Role not found");
        }

        _context.Entry(user).State = EntityState.Modified;
        await _context.SaveChangesAsync();
        await _queueService.SendMessageAsync("User updated!", user);
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
    public async Task<bool> UpdateUserIsActive(int userId, bool isActive)
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