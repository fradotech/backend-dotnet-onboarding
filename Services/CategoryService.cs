using Microsoft.EntityFrameworkCore;
using Product.Models;

namespace Product.Services;

public class CategoryService(AppDbContext context)
{
    private readonly AppDbContext _context = context;

    public async Task<List<Category>> GetAll()
    {
        return await _context.Categories.ToListAsync();
    }

    public async Task Create(Category category)
    {
        _context.Categories.Add(category);
        await _context.SaveChangesAsync();
    }

    public async Task<Category?> Get(int id)
    {
        return await _context.Categories.FirstOrDefaultAsync(p => p.Id == id);
    }

    public async Task Update(Category category, Category categoryRequest)
    {
        category.Name = categoryRequest.Name;
        category.Description = categoryRequest.Description;

        _context.Entry(category).State = EntityState.Modified;
        await _context.SaveChangesAsync();
    }

    public async Task Delete(int id)
    {
        var category = await Get(id);
        if (category is null) return;
        _context.Categories.Remove(category);
        await _context.SaveChangesAsync();
    }
}
