using Application.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    [Authorize]
    [ApiController]
    [Route("[controller]")]
    public class PictureFileController : ControllerBase
    {
        private IServicePictureFile _servicePictureFile;
        public PictureFileController(IServicePictureFile servicePictureFile)
        {
            _servicePictureFile = servicePictureFile;
        }

        [HttpGet("{id}/download")]
        public async Task<IActionResult> Download(int id)
        {
            var file = await _servicePictureFile.GetMetadataAsync(id);
            var bytes = await _servicePictureFile.GetFileContentAsync(id);

            return File(bytes, file.ContentType, file.FileName);
        }

        [HttpGet("{id}/meta")]
        public async Task<IActionResult> GetMetadata(int id)
        {
            return Ok(await _servicePictureFile.GetMetadataAsync(id));
        }

        [HttpGet("{id}/link")]
        public async Task<IActionResult> GetLink(int id)
        {
            return Ok(await _servicePictureFile.GetPublicLinkAsync(id));
        }

        [HttpPost]
        public async Task<IActionResult> Upload(IFormFile file, [FromQuery] string category = "pictureFile")
        {
            return Ok(await _servicePictureFile.UploadAsync(file, category));
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            return Ok(await _servicePictureFile.DeleteAsync(id));
        }
    }
}