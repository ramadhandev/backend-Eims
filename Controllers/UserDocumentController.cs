using EIMS.Dtos.UserDocumentDto;
using EIMS.Dtos.UserDocumentDtos;
using EIMS.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.IO;
using System.Threading.Tasks;

namespace EIMS.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserDocumentController : ControllerBase
    {
        private readonly UserDocumentService _service;
        public UserDocumentController(UserDocumentService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var ud = await _service.GetAllAsync();
            return Ok(ud);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetId(int id)
        {
            var ud = await _service.GetByIdAsync(id);
            if (ud == null) return NotFound();
            return Ok(ud);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromForm] CreateUserDocumentWithFileDto dto)
        {
            try
            {
                var ud = await _service.CreateAsync(dto.ToCreateDto(), dto.File);
                return CreatedAtAction(nameof(GetId), new { id = ud.UserDocumentId }, ud);
            }
            catch (ArgumentException exception)
            {
                return BadRequest(new { message = exception.Message });
            }
            catch (Exception)
            {

                // Log the exception if needed
                return StatusCode(500, new { message = "Internal server error" });
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromForm] UpdateUserDocumentWithFileDto dto)
        {
            try
            {
                var result = await _service.UpdateAsync(id, dto.ToUpdateDto(), dto.File);
                if (!result) return NotFound();
                return Ok(new { message = "Updated successfully" });
            }
            catch (ArgumentException exception)
            {
                return BadRequest(new { message = exception.Message });
            }
            catch (Exception)
            {
                // Log the exception if needed
                return StatusCode(500, new { message = "Internal server error" });
            }
        }

        // Endpoint untuk download file
        [HttpGet("download/{id}")]
        public async Task<IActionResult> DownloadFile(int id)
        {
            var document = await _service.GetByIdAsync(id);
            if (document == null || string.IsNullOrEmpty(document.FileURL))
                return NotFound();

            // Extract relative path from full URL
            var uri = new Uri(document.FileURL);
            var relativePath = uri.AbsolutePath;

            var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", relativePath.TrimStart('/'));

            if (!System.IO.File.Exists(filePath))
                return NotFound();

            var memory = new MemoryStream();
            using (var stream = new FileStream(filePath, FileMode.Open))
            {
                await stream.CopyToAsync(memory);
            }
            memory.Position = 0;

            var contentType = GetContentType(filePath);
            return File(memory, contentType, Path.GetFileName(filePath));
        }

        private string GetContentType(string path)
        {
            var types = new Dictionary<string, string>
            {
                { ".pdf", "application/pdf" },
                { ".png", "image/png" },
                { ".jpg", "image/jpeg" },
                { ".jpeg", "image/jpeg" },
                { ".gif", "image/gif" }
            };

            var ext = Path.GetExtension(path).ToLowerInvariant();
            return types.ContainsKey(ext) ? types[ext] : "application/octet-stream";
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _service.DeleteAsync(id);
            if (!result) return NotFound();
            return Ok(new { message = "Deleted successfully" });
        }

    }
}