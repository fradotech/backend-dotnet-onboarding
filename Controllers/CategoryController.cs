using Microsoft.AspNetCore.Mvc;
using Product.Models;
using Product.Services;

namespace Product.Controllers;

[ApiController]
[Route("categories")]
public class CategoryController(CategoryService categoryService) : ControllerBase
{
    private readonly CategoryService _categoryService = categoryService;

    [HttpGet]
    public async Task<ActionResult<List<Category>>> GetAll()
    {
        return await _categoryService.GetAll();
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] Category category)
    {
        await _categoryService.Create(category);
        return CreatedAtAction(nameof(Get), new { id = category.Id }, category);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<Category>> Get(int id)
    {
        Category? category = await _categoryService.Get(id);
        if (category == null) return NotFound();
        return category;
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, [FromBody] Category categoryRequest)
    {
        Category? category = await _categoryService.Get(id);
        if (category is null) return NotFound();

        await _categoryService.Update(category, categoryRequest);

        return NoContent();
    }


    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        Category? category = await _categoryService.Get(id);
        if (category is null) return NotFound();

        await _categoryService.Delete(id);

        return NoContent();
    }
}