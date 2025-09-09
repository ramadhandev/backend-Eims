
using EIMS.Dtos.InvestigationDtos;
using EIMS.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EIMS.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class InvestigationController : ControllerBase
    {
        private readonly InvestigationServices _service;

        public InvestigationController(InvestigationServices service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<InvestigationDto>>> GetAll()
        {
            var result = await _service.GetAllAsync();
            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<InvestigationDto>> GetById(int id)
        {
            var result = await _service.GetByIdAsync(id);
            if (result == null) return NotFound();
            return Ok(result);
        }

        [HttpGet("incident/{incidentId}")]
        public async Task<ActionResult<InvestigationDto>> GetByIncidentId(int incidentId)
        {
            var result = await _service.GetByIncidentIdAsync(incidentId);
            if (result == null) return NotFound();
            return Ok(result);
        }

        [HttpGet("hse-officer/{hseOfficerId}")]
        public async Task<ActionResult<IEnumerable<InvestigationDto>>> GetByHSEOfficerId(int hseOfficerId)
        {
            var result = await _service.GetByHSEOfficerIdAsync(hseOfficerId);
            return Ok(result);
        }

        [HttpPost]
        public async Task<ActionResult<InvestigationDto>> Create(CreateInvestigationDto dto)
        {
            try
            {
                var result = await _service.CreateAsync(dto);
                return CreatedAtAction(nameof(GetById), new { id = result.InvestigationId }, result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> Update(int id, UpdateInvestigationDto dto)
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

        [HttpPatch("{id}/close")]
        public async Task<ActionResult> CloseInvestigation(int id)
        {
            var result = await _service.CloseInvestigationAsync(id);
            if (!result) return NotFound();
            return NoContent();
        }
    }
}
