using Iam.Models;
using Microsoft.EntityFrameworkCore;

namespace Product.Services;

public class RoleService(AppDbContext context)
{
    private readonly AppDbContext _context = context;

    public async Task<List<Role>> GetAll()
    {
        return await _context.Roles.ToListAsync();
    }

    public async Task Create(Role role)
    {
        _context.Roles.Add(role);
        await _context.SaveChangesAsync();
    }

    public async Task<Role?> Get(int id)
    {
        return await _context.Roles.FirstOrDefaultAsync(p => p.Id == id);
    }

    public async Task Update(Role role, Role roleRequest)
    {
        role.Name = roleRequest.Name;
        
        _context.Entry(role).State = EntityState.Modified;
        await _context.SaveChangesAsync();
    }

    public async Task Delete(int id)
    {
        var role = await Get(id);
        if (role is null) return;
        _context.Roles.Remove(role);
        await _context.SaveChangesAsync();
    }
}
