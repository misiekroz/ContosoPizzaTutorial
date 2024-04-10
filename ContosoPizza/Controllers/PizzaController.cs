using ContosoPizza.Models;
using ContosoPizza.Services;
using Microsoft.AspNetCore.Mvc;

namespace ContosoPizza.Controllers;

[ApiController]
[Route("[controller]")]
public class PizzaController : ControllerBase
{
    IContosoService<Pizza> pizzaService;
    public PizzaController(IContosoService<Pizza> pizzaService)
    {
        this.pizzaService = pizzaService;
    }

    [HttpGet]
    public ActionResult<List<Pizza>> GetAll() => pizzaService.GetAll();

    [HttpGet("{id}")]
    public ActionResult<Pizza> Get(int id)
    {
        var pizza = pizzaService.Get(id);

        if(pizza == null)
        {
            return NotFound();
        }

        return pizza;
    }

    [HttpPost]
    public IActionResult Create(Pizza pizza)
    {
        pizzaService.Add(pizza);
        return CreatedAtAction(nameof(Get), new { id = pizza.ID }, pizza);
    }

    [HttpPut("{id}")]
    public IActionResult Update(int id, Pizza pizza)
    {
        if (id != pizza.ID)
            return BadRequest();

        var existingPizza = pizzaService.Get(id);
        if (existingPizza is null)
            return NotFound();

        pizzaService.Update(pizza);

        return NoContent();
    }

    [HttpDelete("{id}")]
    public IActionResult Delete(int id)
    {
        var pizza = pizzaService.Get(id);

        if (pizza is null)
            return NotFound();

        pizzaService.Delete(id);

        return NoContent();
    }
}