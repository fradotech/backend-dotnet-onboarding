using User.Models;
using User.Services;
using Microsoft.AspNetCore.Mvc;

namespace User.Controllers;

[ApiController]
[Route("users")]
public class UserController : ControllerBase
{
    public UserController()
    {
    }

    [HttpGet]
    public ActionResult<List<UserModel>> GetAll() => UserService.GetAll();

    // POST action

    [HttpGet("{id}")]
    public ActionResult<UserModel> Get(int id)
    {
        var pizza = UserService.Get(id);
        if (pizza == null) return NotFound();
        return pizza;
    }

    // PUT action

    // DELETE action
}