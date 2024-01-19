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
    public async Task<ActionResult<List<User>>> GetAll()
    {
        return await _userService.GetAll();
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] User user)
    {
        await _userService.Create(user);
        return CreatedAtAction(nameof(Get), new { id = user.Id }, user);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<User>> Get(int id)
    {
        var user = await _userService.Get(id);
        if (user == null) return NotFound();
        return user;
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, [FromBody] User user)
    {
        if (id != user.Id) return BadRequest();

        var existingUser = _userService.Get(id);
        if (existingUser is null) return NotFound();

        await _userService.Update(user);

        return NoContent();
    }


    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var user = _userService.Get(id);
        if (user is null) return NotFound();

        await _userService.Delete(id);

        return NoContent();
    }

    [HttpPatch("{id}/is-active")]
    public async Task<IActionResult> UpdateIsActive(int id, bool isActive)
    {
        await _userService.UpdateUserIsActive(id, isActive);
        return NoContent();
    }
}