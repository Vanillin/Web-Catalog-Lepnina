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
        private IServicePictureFile _servicePictureFile;
        public SavingFileController(IServicePictureFile servicePictureFile)
        {
            _servicePictureFile = servicePictureFile;
        }

        [HttpPost]
        public async Task<IActionResult> Upload(IFormFile file, [FromQuery] string category = "pictureFile")
        {
            if (file.Length == 0)
                return BadRequest("File is required");

            var picture = await _servicePictureFile.UploadAsync(file, category);

            return Ok(picture);
        }

        [HttpGet("{id}/download")]
        public async Task<IActionResult> Download(int id)
        {
            try
            {
                var file = await _servicePictureFile.GetMetadataAsync(id);
                if (file == null)
                    return NotFound();

                var bytes = await _servicePictureFile.GetFileContentAsync(id);
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
            var picture = await _servicePictureFile.GetMetadataAsync(id);
            if (picture == null)
                return NotFound();

            return Ok(picture);
        }

        [HttpGet("{id}/link")]
        public async Task<IActionResult> GetLink(int id)
        {
            try
            {
                var link = await _servicePictureFile.GetPublicLinkAsync(id);
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
            await _servicePictureFile.DeleteAsync(id);
            return Ok(new { message = "PictureFile deleted" });
        }
    }
}