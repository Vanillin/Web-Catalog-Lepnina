using Api.Exceptions;
using Application.Dto;
using Application.Request;
using Application.Services;
using Domain.Entities;
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

    //[HttpGet("{id}")]
    //public async Task<IActionResult> GetByIdAsync(int id)
    //{
    //    return Ok(await _serviceProduct.ReadById(id));
    //}

    //[HttpGet("all")]
    //public async Task<IActionResult> GetAll()
    //{
    //    return Ok(await _serviceProduct.ReadAll());
    //}

    [HttpGet("catalog/{id}/{isFavorite}")]
    public async Task<IActionResult> GetProductForCatalog(int id, bool isFavorite)
    {
        var productsSection = await _serviceProduct.GetBySection(id);
        if (productsSection == null || productsSection.Count() == 0)
        {
            productsSection = await _serviceProduct.ReadAll();
        }

        if (isFavorite)
            return await GetAllByFavorite(productsSection);
        else 
            return Ok(productsSection);
    }
    [Authorize]
    private async Task<IActionResult> GetAllByFavorite(IEnumerable<ProductDto> products)
    {
        var userId = User.GetUserId();
        if (!userId.HasValue)
            return NotFound();

        var favoriteProducts = await _serviceProduct.GetByFavorite(userId.Value);

        return Ok(favoriteProducts.Where(x => favoriteProducts.Contains(x)));
    }

    //[HttpGet("section/{id}")]
    //public async Task<IActionResult> GetAllBySection(int id)
    //{
    //    return Ok(await _serviceProduct.GetBySection(id));
    //}

    //[Authorize]
    //[HttpGet("user")]
    //public async Task<IActionResult> GetAllByFavorite()
    //{
    //    var userId = User.GetUserId();
    //    if (!userId.HasValue)
    //        return NotFound();

    //    return Ok(await _serviceProduct.GetByFavorite(userId.Value));
    //}

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
