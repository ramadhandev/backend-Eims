using EIMS.Dtos.DocumentRqDtos;
using EIMS.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EIMS.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DocumentRqController : ControllerBase
    {
        private readonly DocumentRqService _service;

        public DocumentRqController(DocumentRqService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var docs = await _service.GetAllAsync();
            return Ok(docs);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var doc = await _service.GetByIdAsync(id);
            if (doc == null) return NotFound();
            return Ok(doc);
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateDocumentRequirementDto dto)
        {
            var doc = await _service.CreateAsync(dto);
            return CreatedAtAction(nameof(GetById), new { id = doc.DocumentRequirementId }, doc);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, UpdateDocumentRequirementDto dto)
        {
            var result = await _service.UpdateAsync(id, dto);
            if (!result) return NotFound();
            return NoContent();
        }

        [HttpDelete("{id}i")]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _service.DeleteAsync(id);
            if (!result) return NotFound();
            return NoContent();
        }
    }
}
