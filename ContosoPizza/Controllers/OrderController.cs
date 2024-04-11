using ContosoPizza.Models;
using ContosoPizza.Models.Users;
using ContosoPizza.Services;
using Microsoft.AspNetCore.Mvc;

namespace ContosoPizza.Controllers;

[ApiController]
[Route("[controller]")]
public class OrderController : ControllerBase
{
    IContosoService<Order> orderService;
    IContosoService<Pizza> pizzaService;
    IContosoService<User> userService;
    IContosoService<PremiumUser> premiumUserService;
    public OrderController(IContosoService<Order> orderService,
                           IContosoService<Pizza> pizzaService,
                           IContosoService<User> userService,
                           IContosoService<PremiumUser> premiumUserService)
    {
        this.orderService = orderService;
        this.pizzaService = pizzaService;
        this.userService = userService;
        this.premiumUserService = premiumUserService;
    }

    [HttpGet]
    public ActionResult<List<Order>> GetAll() => orderService.GetAll();

    [HttpGet("{id}")]
    public ActionResult<Order> Get(int id)
    {
        var order = orderService.Get(id);

        if(order == null)
        {
            return NotFound();
        }

        return order;
    }

    [HttpPost]
    public IActionResult Create(Order order)
    {
        // check if selected user exists as normal or premium user
        var foundUser = userService.Get(order.UserID) ?? premiumUserService.Get(order.UserID);
        if (foundUser is null)
            return NotFound($"User not found! ID: {order.UserID}");

        // chceck if all selcted pizzas exist in DB
        var result = PizzaListItemsCheck(order.PizzaIDs);
        if (result is not null)
            return NotFound($"Pizza not found! ID: {result}");

        orderService.Add(order);

        if (foundUser is PremiumUser foundPremiumUser)
            foundPremiumUser.Points += order.PremiumPoints;
        else
            foundUser.OrdersCount++;

        order.Received = false;

        return CreatedAtAction(nameof(Get), new { id = order.ID }, order);
    }

    [HttpPut("{id}")]
    public IActionResult Update(int id, Order order)
    {
        // check if the request is formed properly
        if (id != order.ID)
            return BadRequest();

        // try and find if order exists
        var existingOrder = orderService.Get(id);
        if (existingOrder is null)
            return NotFound();

        // ensure that ID was not changed
        if(existingOrder.UserID != order.UserID)
            return BadRequest("Changing UserID is not allowed");

        // check pizzas again if they all exist
        var pizzaListResult = PizzaListItemsCheck(order.PizzaIDs);
        if (pizzaListResult is not null)
            return NotFound($"Pizza not found! ID: {pizzaListResult}");

        // NOTE: for now, points are not handeled when updating

        orderService.Update(order);

        return NoContent();
    }

    [HttpDelete("{id}")]
    public IActionResult Delete(int id)
    {
        var order = orderService.Get(id);

        if (order is null)
            return NotFound();

        orderService.Delete(id);

        // NOTE: for now points are not handeled

        return NoContent();
    }

    [HttpPatch("{id}/received")]
    public IActionResult Received(int id)
    {
        var order = orderService.Get(id);
        if (order is null)
            return NotFound();

        order.Received = true;

        return NoContent();
    }


    private int? PizzaListItemsCheck(List<int> pizzaList)
    {
        // NOTE: when DB would be used, it'd be better to retreive only the amount of
        // ID's that are not present in the table. For now with placeholder services it's OK
        foreach (int id in pizzaList)
            if (pizzaService.Get(id) is null)
                return id;

        return null;
    }
}