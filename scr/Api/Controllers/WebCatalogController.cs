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
    private IServiceFavorites _serviceUserProduct;
    public WebCatalogController(IServiceAttachment serviceAttachment, IServiceProduct serviceProduct, IServiceReview serviceReview, 
        IServiceSection serviceSection, IServiceUser serviceUser, IServiceFavorites serviceUserProduct)
    {
        _serviceAttachment = serviceAttachment;
        _serviceProduct = serviceProduct;
        _serviceReview = serviceReview;
        _serviceSection = serviceSection;
        _serviceUser = serviceUser;
        _serviceUserProduct = serviceUserProduct;
    }

    // /quiz/getbyid?id=2

    [HttpGet("{id}")]
    public async Task<IActionResult> GetByIdAsync([FromQuery] int id)
    {
        var quiz = await _serviceAttachment.ReadById(id);
        return Ok(quiz);
    }

    [HttpGet("all")]
    public async Task<IActionResult> GetAll()
    {
        var quiz = await _serviceAttachment.ReadAll();
        return Ok(quiz);
    }

    [HttpPost]
    public async Task<IActionResult> Add([FromBody] AttachmentDto quiz)
    {
        await _serviceAttachment.Create(quiz);
        return Created();
    }

    [HttpPut]
    public async Task<IActionResult> Update([FromBody] AttachmentDto quiz)
    {
        var result = await _serviceAttachment.Update(quiz);
        return Ok(result);
    }

    [HttpDelete]
    public async Task<IActionResult> Delete([FromQuery] int id)
    {
        var result = await _serviceAttachment.Delete(id);
        return Ok(result);
    }
}
