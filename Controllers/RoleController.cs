using Microsoft.AspNetCore.Mvc;
using Iam.Models;
using Iam.Services;
using Product.Services;

namespace Iam.Controllers;

[ApiController]
[Route("roles")]
public class RoleController(RoleService roleService) : ControllerBase
{
    private readonly RoleService _roleService = roleService;

    [HttpGet]
    public async Task<ActionResult<List<Role>>> GetAll()
    {
        return await _roleService.GetAll();
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] Role role)
    {
        await _roleService.Create(role);
        return CreatedAtAction(nameof(Get), new { id = role.Id }, role);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<Role>> Get(int id)
    {
        Role? role = await _roleService.Get(id);
        if (role == null) return NotFound();
        return role;
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, [FromBody] Role roleRequest)
    {
        Role? role = await _roleService.Get(id);
        if (role is null) return NotFound();

        await _roleService.Update(role, roleRequest);

        return NoContent();
    }


    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        Role? role = await _roleService.Get(id);
        if (role is null) return NotFound();

        await _roleService.Delete(id);

        return NoContent();
    }
}