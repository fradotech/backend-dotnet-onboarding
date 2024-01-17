using Iam.Services;
using Microsoft.AspNetCore.Mvc;
using Iam.Models;

namespace Iam.Controllers;

[ApiController]
[Route("users")]
public class UserController(UserService userService) : ControllerBase
{
    private readonly UserService _userService = userService;

    [HttpGet]
    public async Task<ActionResult<List<AppUser>>> GetAll()
    {
        return await _userService.GetAll();
    }

    [HttpPost]
    public async Task<IActionResult> Create(AppUser user)
    {
        await _userService.Create(user);
        return CreatedAtAction(nameof(Get), new { id = user.Id }, user);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<AppUser>> Get(int id)
    {
        var user = await _userService.Get(id);
        if (user == null) return NotFound();
        return user;
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, AppUser user)
    {
        if (id != user.Id) return BadRequest();

        var existingPizza = _userService.Get(id);
        if (existingPizza is null) return NotFound();

        await _userService.Update(user);

        return NoContent();
    }


    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var pizza = _userService.Get(id);
        if (pizza is null) return NotFound();

        await _userService.Delete(id);

        return NoContent();
    }
}