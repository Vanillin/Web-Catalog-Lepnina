using Application.Dto;
using Application.Request;
using Application.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers;

[ApiController]
[Route("[controller]")]
public class ProductController : ControllerBase
{
    private IServiceProduct _serviceProduct;
    public ProductController(IServiceProduct serviceProduct)
    {
        _serviceProduct = serviceProduct;
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetByIdAsync(int id)
    {
        return Ok(await _serviceProduct.ReadById(id));
    }

    [HttpGet("all")]
    public async Task<IActionResult> GetAll()
    {
        return Ok(await _serviceProduct.ReadAll());
    }

    [Authorize(Roles = "Admin")]
    [HttpPost]
    public async Task<IActionResult> Add([FromBody] CreateProductRequest product)
    {
        return Ok(await _serviceProduct.Create(product));
    }

    [Authorize(Roles = "Admin")]
    [HttpPut]
    public async Task<IActionResult> Update([FromBody] UpdateProductRequest product)
    {
        return Ok(await _serviceProduct.Update(product));
    }

    [Authorize(Roles = "Admin")]
    [HttpDelete]
    public async Task<IActionResult> Delete([FromQuery] int id)
    {
        return Ok(await _serviceProduct.Delete(id));
    }
}
