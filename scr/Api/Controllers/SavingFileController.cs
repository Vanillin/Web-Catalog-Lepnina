using Application.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    [Authorize]
    [ApiController]
    [Route("[controller]")]
    public class SavingFileController : ControllerBase
    {
        private IServicePictureFile _attachmentService;
        public SavingFileController(IServicePictureFile attachmentService)
        {
            _attachmentService = attachmentService;
        }

        [HttpPost]
        public async Task<IActionResult> Upload(IFormFile file, [FromQuery] string category = "pictureFile")
        {
            if (file.Length == 0)
                return BadRequest("File is required");

            var attachment = await _attachmentService.UploadAsync(file, category);

            return Ok(attachment);
        }

        [HttpGet("{id}/download")]
        public async Task<IActionResult> Download(int id)
        {
            try
            {
                var file = await _attachmentService.GetMetadataAsync(id);
                if (file == null)
                    return NotFound();

                var bytes = await _attachmentService.GetFileContentAsync(id);
                return File(bytes, file.ContentType, file.FileName);
            }
            catch (FileNotFoundException)
            {
                return NotFound();
            }
        }

        [HttpGet("{id}/meta")]
        public async Task<IActionResult> GetMetadata(int id)
        {
            var attachment = await _attachmentService.GetMetadataAsync(id);
            if (attachment == null)
                return NotFound();

            return Ok(attachment);
        }

        [HttpGet("{id}/link")]
        public async Task<IActionResult> GetLink(int id)
        {
            try
            {
                var link = await _attachmentService.GetPublicLinkAsync(id);
                return Ok(new { url = link });
            }
            catch (FileNotFoundException)
            {
                return NotFound();
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _attachmentService.DeleteAsync(id);
            return Ok(new { message = "PictureFile deleted" });
        }
    }
}