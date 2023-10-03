using Ballast.Application.Products.Commands.AddProductStock;
using Ballast.Application.Products.Commands.CreateProduct;
using Ballast.Application.Products.Commands.DeleteProduct;
using Ballast.Application.Products.Commands.UpdateProduct;
using Ballast.Application.Products.Queries.GetProductDetailById;
using Ballast.Application.Products.Queries.GetProductList;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Ballast.Api.Controllers;

[Route("api/products")]
public class ProductsController : ControllerBase
{
    private readonly IMediator _mediator;

    public ProductsController(IMediator mediator)
    {
        _mediator = mediator;
    }

   
    [HttpGet]
    [Route("{id:guid}")]
    public async Task<ActionResult<ProductDetailModel>> GetProductDetailById(Guid id)
    {
        var product = await _mediator.Send(new GetProductDetailByIdQuery(id));
        return Ok(product);
    }
    
    [HttpGet]
    public async Task<ActionResult<ProductDetailModel>> GetProductList([FromQuery]GetProductListQuery query)
    {
        var products = await _mediator.Send(query);
        return Ok(products);
    }

    [HttpPost]
    public async Task<IActionResult> CreateProduct([FromBody] CreateProductCommand command)
    {
        var id = await _mediator.Send(command);
        return CreatedAtAction(nameof(GetProductDetailById), new { id = id }, null);
    }

    [HttpPut("{id:guid}")]
    public async Task<IActionResult> UpdateProduct(Guid id, UpdateProductCommand command)
    {
        await _mediator.Send(command);
        return NoContent();
    }

    [HttpDelete("{id:guid}")]
    public async Task<ActionResult> DeleteProduct(Guid id)
    {
        await _mediator.Send(new DeleteProductCommand { Id = id });
        return NoContent();
    }

    [HttpPost("{id:guid}/stock/add")]
    public async Task<ActionResult> AddProductStock(Guid id, AddProductStockCommand command)
    {
        if (id != command.Id)
        {
            return BadRequest();
        }
        await _mediator.Send(command);
        return NoContent();
    }
}