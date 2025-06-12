using Application.Dto;
using Application.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers;

[Authorize]
[ApiController]
[Route("[controller]")]
public class FavoritesController : ControllerBase
{
    private IServiceFavorites _serviceFavorites;
    public FavoritesController(IServiceFavorites serviceFavorites)
    {
        _serviceFavorites = serviceFavorites;
    }

    //[HttpGet("{idUser}/{idProduct}")]
    //public async Task<IActionResult> GetByIdAsync(int idUser, int idProduct)
    //{
    //    return Ok(await _serviceFavorites.ReadById(idUser, idProduct));
    //}

    //[HttpGet("all")]
    //public async Task<IActionResult> GetAll()
    //{
    //    return Ok(await _serviceFavorites.ReadAll());
    //}

    [HttpPost("{idUser}/{idProduct}")]
    public async Task<IActionResult> Add(int idUser, int idProduct)
    {

        return Ok(await _serviceFavorites.Create(
            new FavoritesDto()
            {
                IdUser = idUser,
                IdProduct = idProduct
            }));
    }

    [HttpDelete("{idUser}/{idProduct}")]
    public async Task<IActionResult> Delete(int idUser, int idProduct)
    {
        return Ok(await _serviceFavorites.Delete(idUser, idProduct));
    }
}
