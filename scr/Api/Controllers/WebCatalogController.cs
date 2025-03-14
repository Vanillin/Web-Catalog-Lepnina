using Application.Dto;
using Application.Services;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers;

[ApiController]
[Route("[controller]")]
public class WebCatalogController : ControllerBase
{
    private IServiceAttachment _serviceAttachment;
    private IServiceProduct _serviceProduct;
    private IServiceReview _serviceReview;
    private IServiceSection _serviceSection;
    private IServiceUser _serviceUser;
    private IServiceFavorites _serviceFavorites;
    public WebCatalogController(IServiceAttachment serviceAttachment, IServiceProduct serviceProduct, IServiceReview serviceReview, 
        IServiceSection serviceSection, IServiceUser serviceUser, IServiceFavorites serviceFavorites)
    {
        _serviceAttachment = serviceAttachment;
        _serviceProduct = serviceProduct;
        _serviceReview = serviceReview;
        _serviceSection = serviceSection;
        _serviceUser = serviceUser;
        _serviceFavorites = serviceFavorites;
    }

    #region Attachment endpoints
    [HttpGet("attachment {id}")]
    public async Task<IActionResult> GetAttachmentByIdAsync([FromQuery] int id)
    {
        var quiz = await _serviceAttachment.ReadById(id);
        return Ok(quiz);
    }

    [HttpGet("attachment all")]
    public async Task<IActionResult> GetAllAttachment()
    {
        var quiz = await _serviceAttachment.ReadAll();
        return Ok(quiz);
    }

    [HttpPost("attachment")]
    public async Task<IActionResult> AddAttachment([FromBody] AttachmentDto quiz)
    {
        await _serviceAttachment.Create(quiz);
        return Created();
    }

    [HttpPut("attachment")]
    public async Task<IActionResult> UpdateAttachment([FromBody] AttachmentDto quiz)
    {
        var result = await _serviceAttachment.Update(quiz);
        return Ok(result);
    }

    [HttpDelete("attachment")]
    public async Task<IActionResult> DeleteAttachment([FromQuery] int id)
    {
        var result = await _serviceAttachment.Delete(id);
        return Ok(result);
    }
    #endregion

    #region Product endpoints
    [HttpGet("product {id}")]
    public async Task<IActionResult> GetProductByIdAsync([FromQuery] int id)
    {
        var quiz = await _serviceProduct.ReadById(id);
        return Ok(quiz);
    }

    [HttpGet("product all")]
    public async Task<IActionResult> GetAllProduct()
    {
        var quiz = await _serviceProduct.ReadAll();
        return Ok(quiz);
    }

    [HttpPost("product")]
    public async Task<IActionResult> AddProduct([FromBody] ProductDto quiz)
    {
        await _serviceProduct.Create(quiz);
        return Created();
    }

    [HttpPut("product")]
    public async Task<IActionResult> UpdateProduct([FromBody] ProductDto quiz)
    {
        var result = await _serviceProduct.Update(quiz);
        return Ok(result);
    }

    [HttpDelete("product")]
    public async Task<IActionResult> DeleteProduct([FromQuery] int id)
    {
        var result = await _serviceProduct.Delete(id);
        return Ok(result);
    }
    #endregion

    #region Review endpoints
    [HttpGet("review {id}")]
    public async Task<IActionResult> GetReviewByIdAsync([FromQuery] int id)
    {
        var quiz = await _serviceReview.ReadById(id);
        return Ok(quiz);
    }

    [HttpGet("review all")]
    public async Task<IActionResult> GetAllReview()
    {
        var quiz = await _serviceReview.ReadAll();
        return Ok(quiz);
    }

    [HttpPost("review")]
    public async Task<IActionResult> AddReview([FromBody] ReviewDto quiz)
    {
        await _serviceReview.Create(quiz);
        return Created();
    }

    [HttpPut("review")]
    public async Task<IActionResult> UpdateReview([FromBody] ReviewDto quiz)
    {
        var result = await _serviceReview.Update(quiz);
        return Ok(result);
    }

    [HttpDelete("review")]
    public async Task<IActionResult> DeleteReview([FromQuery] int id)
    {
        var result = await _serviceReview.Delete(id);
        return Ok(result);
    }
    #endregion

    #region Section endpoints
    [HttpGet("section {id}")]
    public async Task<IActionResult> GetSectionByIdAsync([FromQuery] int id)
    {
        var quiz = await _serviceSection.ReadById(id);
        return Ok(quiz);
    }

    [HttpGet("section all")]
    public async Task<IActionResult> GetAllSection()
    {
        var quiz = await _serviceSection.ReadAll();
        return Ok(quiz);
    }

    [HttpPost("section")]
    public async Task<IActionResult> AddSection([FromBody] SectionDto quiz)
    {
        await _serviceSection.Create(quiz);
        return Created();
    }

    [HttpPut("section")]
    public async Task<IActionResult> UpdateSection([FromBody] SectionDto quiz)
    {
        var result = await _serviceSection.Update(quiz);
        return Ok(result);
    }

    [HttpDelete("section")]
    public async Task<IActionResult> DeleteSection([FromQuery] int id)
    {
        var result = await _serviceSection.Delete(id);
        return Ok(result);
    }
    #endregion

    #region User endpoints
    [HttpGet("user {id}")]
    public async Task<IActionResult> GetUserByIdAsync([FromQuery] int id)
    {
        var quiz = await _serviceUser.ReadById(id);
        return Ok(quiz);
    }

    [HttpGet("user all")]
    public async Task<IActionResult> GetAllUser()
    {
        var quiz = await _serviceUser.ReadAll();
        return Ok(quiz);
    }

    [HttpPost("user")]
    public async Task<IActionResult> AddUser([FromBody] UserDto quiz)
    {
        await _serviceUser.Create(quiz);
        return Created();
    }

    [HttpPut("user")]
    public async Task<IActionResult> UpdateUser([FromBody] UserDto quiz)
    {
        var result = await _serviceUser.Update(quiz);
        return Ok(result);
    }

    [HttpDelete("user")]
    public async Task<IActionResult> DeleteUser([FromQuery] int id)
    {
        var result = await _serviceUser.Delete(id);
        return Ok(result);
    }
    #endregion

    #region Favorites endpoints
    [HttpGet("favorites {idU} {idP}")]
    public async Task<IActionResult> GetFavoritesByIdAsync([FromQuery] int idU, [FromQuery] int idP)
    {
        var quiz = await _serviceFavorites.ReadById(idU, idU);
        return Ok(quiz);
    }

    [HttpGet("favorites all")]
    public async Task<IActionResult> GetAllFavorites()
    {
        var quiz = await _serviceFavorites.ReadAll();
        return Ok(quiz);
    }

    [HttpPost("favorites")]
    public async Task<IActionResult> AddFavorites([FromBody] FavoritesDto quiz)
    {
        await _serviceFavorites.Create(quiz);
        return Created();
    }

    [HttpDelete("favorites {idU} {idP}")]
    public async Task<IActionResult> DeleteFavorites([FromQuery] int idU, [FromQuery] int idP)
    {
        var result = await _serviceFavorites.Delete(idU, idU);
        return Ok(result);
    }
    #endregion
}
