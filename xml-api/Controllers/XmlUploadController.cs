using Microsoft.AspNetCore.Mvc;
using xml_api.Services;

namespace xml_api.Controllers
{
    [ApiController]
    [Route("api/xml")]
    public class XmlUploadController : ControllerBase
    {
        private readonly XmlProcessingService _xmlProcessingService;

        public XmlUploadController(XmlProcessingService xmlProcessingService)
        {
            _xmlProcessingService = xmlProcessingService;
        }

        [HttpPost("upload")]
        public async Task<IActionResult> Upload(IFormFile file)
        {
            if (file == null || file.Length == 0)
                return BadRequest("Invalid file");

            using var stream = file.OpenReadStream();
            var xmlOriginId = await _xmlProcessingService.ProcessXmlFileAsync(stream, file.FileName);
            return Ok(new { message = "Processed successfully", xmlOriginId });
        }
    }
}
