using EIMS.Dto.IncidentDto;
using EIMS.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EIMS.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class IncidentReportController : ControllerBase
    {
        private readonly IncidentReportServices _service;

        public IncidentReportController(IncidentReportServices service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<IncidentReportDto>>> GetAll()
        {
            var result = await _service.GetAllAsync();
            return Ok(result);
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<IncidentReportDto>> GetById(int id)
        {
            var result = await _service.GetByIdAsync(id);
            if (result == null) return NotFound();
            return Ok(result);
        }
    
    [HttpGet("user/{userId}")]
        public async Task<ActionResult<IEnumerable<IncidentReportDto>>> GetByUserId(int userId)
        {
            var result = await _service.GetByUserIdAsync(userId);
            return Ok(result);
        }

        [HttpGet("category/{category}")]
        public async Task<ActionResult<IEnumerable<IncidentReportDto>>> GetByCategory(string category)
        {
            var result = await _service.GetByCategoryAsync(category);
            return Ok(result);
        }



        [HttpPost]
        [Consumes("multipart/form-data")]
        public async Task<ActionResult<IncidentReportDto>> Create([FromForm] CreateIncidentReportDto dto, IFormFile? photo)
        {
            try
            {
                var result = await _service.CreateAsync(dto, photo);
                return CreatedAtAction(nameof(GetById), new { id = result.IncidentId }, result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> Update(int id, UpdateIncidentReportDto dto)
        {
            var result = await _service.UpdateAsync(id, dto);
            if (!result) return NotFound();
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            var result = await _service.DeleteAsync(id);
            if (!result) return NotFound();
            return NoContent();
        }

        [HttpPatch("{id}/status")]
        public async Task<ActionResult> UpdateStatus(int id, [FromBody] string status)
        {
            var result = await _service.UpdateStatusAsync(id, status);
            if (!result) return NotFound();
            return NoContent();
        }


    }
}
