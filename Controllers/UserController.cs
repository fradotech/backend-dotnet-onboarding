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
    public ActionResult<List<AppUser>> GetAll()
    {
        return _userService.GetAll();
    }

    [HttpPost]
    public IActionResult Create(AppUser user)
    {
        _userService.Add(user);
        return CreatedAtAction(nameof(Get), new { id = user.Id }, user);
    }

    [HttpGet("{id}")]
    public ActionResult<AppUser> Get(int id)
    {
        var user = _userService.Get(id);
        if (user == null) return NotFound();
        return user;
    }

    [HttpPut("{id}")]
    public IActionResult Update(int id, AppUser user)
    {
        if (id != user.Id) return BadRequest();

        var existingPizza = _userService.Get(id);
        if (existingPizza is null) return NotFound();

        _userService.Update(user);

        return NoContent();
    }


    [HttpDelete("{id}")]
    public IActionResult Delete(int id)
    {
        var pizza = _userService.Get(id);
        if (pizza is null) return NotFound();

        _userService.Delete(id);

        return NoContent();
    }
}