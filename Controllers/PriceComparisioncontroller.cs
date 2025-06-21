using Microsoft.AspNetCore.Mvc;
using PriceComparisiontool.Models;
using PriceComparisiontool.Transformations;

[ApiController]
[Route("api/PriceComparision/")]
public class ProductsController : ControllerBase
{
    private readonly ProductRepository _repository;

    public ProductsController(ProductRepository repository)
    {
        _repository = repository;
    }

    [HttpGet]
    public IActionResult GetByName([FromQuery] string name)
    {
        if (string.IsNullOrWhiteSpace(name))
            return BadRequest("Product name is required.");

        var product = _repository.GetProductByName(name);
        if (product == null)
            return NotFound("Product not found.");

        return Ok(product);
    }


    [HttpPost]
    public IActionResult Add([FromBody] Product product)
    {
        if (!ModelState.IsValid)
            return StatusCode(500);

        _repository.AddProduct(product);
        return CreatedAtAction(nameof(GetByName), new { name = product.Name }, product);
    }
}
