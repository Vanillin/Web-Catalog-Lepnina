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
        return Ok(await _serviceFavorites.ReadById(idUser, idProduct));
    }

    [HttpGet("all")]
    public async Task<IActionResult> GetAll()
    {
        return Ok(await _serviceFavorites.ReadAll());
    }

    [HttpPost]
    public async Task<IActionResult> Add([FromBody] FavoritesDto favorites)
    {
        return Ok(await _serviceFavorites.Create(favorites));
    }

    [HttpDelete("{idUser}/{idProduct}")]
    public async Task<IActionResult> Delete(int idUser, int idProduct)
    {
        return Ok(await _serviceFavorites.Delete(idUser, idProduct));
    }
}
