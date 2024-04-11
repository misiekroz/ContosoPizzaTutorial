using ContosoPizza.Models;
using ContosoPizza.Models.Users;
using ContosoPizza.Services;
using Microsoft.AspNetCore.Mvc;

namespace ContosoPizza.Controllers;

[ApiController]
[Route("[controller]")]
public class PremiumUserController : ControllerBase
{
    IContosoService<PremiumUser> premiumUserService;
    IContosoService<User> userService;

    const int premiumTreshold = 10;

    public PremiumUserController(IContosoService<PremiumUser> premiumUserService, IContosoService<User> userService)
    {
        this.premiumUserService = premiumUserService;
        this.userService = userService;
    }

    [HttpGet]
    public ActionResult<List<PremiumUser>> GetAll() => premiumUserService.GetAll();

    [HttpGet("{id}")]
    public ActionResult<PremiumUser> Get(int id)
    {
        var User = premiumUserService.Get(id);

        if (User == null)
        {
            return NotFound();
        }

        return User;
    }

    [HttpPost]
    public IActionResult Create(int currentUserId)
    {
        var currentUser = userService.Get(currentUserId);
        if (currentUser is null)
            return NotFound();

        // if user made enough orders, proceed with account type switch
        if (currentUser.OrdersCount < premiumTreshold)
            return BadRequest($"User {currentUserId} did not make enough orders.");

        // remove the user from db
        userService.Delete(currentUserId);
        // add the user to premium users db
        var newUser = new PremiumUser(currentUser);
        premiumUserService.Add(newUser);
        return CreatedAtAction(nameof(Get), new { id = currentUserId }, newUser);
    }

    [HttpPut("{id}")]
    public IActionResult Update(int id, PremiumUser user)
    {
        if (id != user.ID)
            return BadRequest("ID cannot be changed");

        var existingUser = premiumUserService.Get(id);
        if (existingUser is null)
            return NotFound();

        premiumUserService.Update(user);

        return NoContent();
    }

    [HttpDelete("{id}")]
    public IActionResult Delete(int id)
    {
        var User = premiumUserService.Get(id);

        if (User is null)
            return NotFound();

        premiumUserService.Delete(id);

        return NoContent();
    }
}