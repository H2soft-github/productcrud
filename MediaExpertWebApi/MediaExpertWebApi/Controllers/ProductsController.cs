using MediaExpertWebApi.Interfaces;
using MediaExpertWebApi.Models;
using Microsoft.AspNetCore.Mvc;

namespace MediaExpertWebApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ProductsController : ControllerBase
{
    private readonly IProductService productService;
    private readonly DatabaseInMemory databaseInMemory;
    public ProductsController(IProductService productService, DatabaseInMemory databaseInMemory)
    {
        this.productService = productService;
        this.databaseInMemory = databaseInMemory;
    }

    [HttpGet]
    public IEnumerable<Product> Get()
    {
        return productService.GetProducts();
    }

    [HttpGet("{id}")]
    public ActionResult<Product> Get(int id)
    {
        try
        {
            return productService.GetProduct(id);
        }
        catch (KeyNotFoundException)
        {
            return NotFound();
        }
    }

    [HttpPost]
    public IActionResult Post([FromBody] Product product)
    {
        ProductAddValidator validator = new(databaseInMemory);
        var result = validator.Validate(product);
        if (result.IsValid)
        {
            productService.AddProduct(product);
            return Ok();
        }
        else
        {
            return BadRequest(string.Join(" ", result.Errors));
        }
    }

    [HttpPut("{id}")]
    public IActionResult Put(int id, Product product)
    {
        product.Id = id;
        ProductUpdateValidator validator = new(databaseInMemory, product.Id);
        var result = validator.Validate(product);
        if (result.IsValid)
        {
            try
            {
                productService.UpdateProduct(product);
                return Ok();
            } catch(KeyNotFoundException)
            {
                return NotFound();
            }
        }
        else
        {
            return BadRequest(string.Join(" ", result.Errors));
        }
    }

    [HttpDelete("{id}")]
    public IActionResult Delete(int id)
    {
        try
        {
            productService.DeleteProduct(id);
            return Ok();
        }
        catch (KeyNotFoundException)
        {
            return NotFound();
        }
    }

}
