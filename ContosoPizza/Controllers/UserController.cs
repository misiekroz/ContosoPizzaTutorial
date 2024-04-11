using ContosoPizza.Models;
using ContosoPizza.Services;
using Microsoft.AspNetCore.Mvc;

namespace ContosoPizza.Controllers;

[ApiController]
[Route("[controller]")]
public class UserController : ControllerBase
{
    IContosoService<User> userService;
    public UserController(IContosoService<User> UserService)
    {
        this.userService = UserService;
    }

    [HttpGet]
    public ActionResult<List<User>> GetAll() => userService.GetAll();

    [HttpGet("{id}")]
    public ActionResult<User> Get(int id)
    {
        var User = userService.Get(id);

        if (User == null)
        {
            return NotFound();
        }

        return User;
    }

    [HttpPost]
    public IActionResult Create(User user)
    {
        if (!UserMailExists(user.Email))
            return BadRequest($"Email {user.Email} already taken");
        userService.Add(user);
        return CreatedAtAction(nameof(Get), new { id = user.ID }, user);
    }

    [HttpPut("{id}")]
    public IActionResult Update(int id, User user)
    {
        if (id != user.ID)
            return BadRequest("ID cannot be changed");

        var existingUser = userService.Get(id);
        if (existingUser is null)
            return NotFound();

        userService.Update(user);

        return NoContent();
    }

    [HttpDelete("{id}")]
    public IActionResult Delete(int id)
    {
        var User = userService.Get(id);

        if (User is null)
            return NotFound();

        userService.Delete(id);

        return NoContent();
    }


    private bool UserMailExists(string email)
    {
        // same story as in OrdersController
        // better, LINQ/SQL approach is planned
        foreach (var user in userService.GetAll())
        {
            if (user.Email == email)
                return true;
        }
        return false;
    }
}