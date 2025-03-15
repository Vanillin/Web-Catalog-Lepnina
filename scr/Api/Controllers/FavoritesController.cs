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

    [HttpGet("{idU} {idP}")]
    public async Task<IActionResult> GetByIdAsync([FromQuery] int idU, [FromQuery] int idP)
    {
        var quiz = await _serviceFavorites.ReadById(idU, idP);
        return Ok(quiz);
    }

    [HttpGet("all")]
    public async Task<IActionResult> GetAll()
    {
        var quiz = await _serviceFavorites.ReadAll();
        return Ok(quiz);
    }

    [HttpPost]
    public async Task<IActionResult> Add([FromBody] FavoritesDto quiz)
    {
        await _serviceFavorites.Create(quiz);
        return Created();
    }

    [HttpDelete("{idU} {idP}")]
    public async Task<IActionResult> Delete([FromQuery] int idU, [FromQuery] int idP)
    {
        var result = await _serviceFavorites.Delete(idU, idP);
        return Ok(result);
    }
}
