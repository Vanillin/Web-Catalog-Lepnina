using Application.Dto;
using Application.Services;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers;

[ApiController]
[Route("[controller]")]
public class FavoritesController : ControllerBase
{
    private IServiceFavorites _serviceFavorites;
    public FavoritesController(IServiceFavorites serviceFavorites)
    {
        _serviceFavorites = serviceFavorites;
    }

    [HttpGet("{idUser}/{idProduct}")]
    public async Task<IActionResult> GetByIdAsync(int idUser, int idProduct)
    {
        var result = await _serviceFavorites.ReadById(idUser, idProduct);
        if (result == null) return NotFound();
        return Ok(result);
    }

    [HttpGet("all")]
    public async Task<IActionResult> GetAll()
    {
        var result = await _serviceFavorites.ReadAll();
        return Ok(result);
    }

    [HttpPost]
    public async Task<IActionResult> Add([FromBody] FavoritesDto favorites)
    {
        var result = await _serviceFavorites.Create(favorites);
        if (result == null) return BadRequest();
        return Ok(result);
    }

    [HttpDelete("{idUser}/{idProduct}")]
    public async Task<IActionResult> Delete(int idUser, int idProduct)
    {
        var result = await _serviceFavorites.Delete(idUser, idProduct);
        return Ok(result);
    }
}
