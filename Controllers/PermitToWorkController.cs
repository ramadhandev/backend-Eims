
using EIMS.Dto.PermitToWorkDtos;
using EIMS.Dtos.PermitToWorkDtos;
using EIMS.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EIMS.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PermitToWorkController : ControllerBase
    {
        private readonly PermitToWorkServices _service;

        public PermitToWorkController(PermitToWorkServices service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<PermitToWorkDto>>> GetAll()
        {
            var result = await _service.GetAllAsync();
            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<PermitToWorkDto>> GetById(int id)
        {
            var result = await _service.GetByIdAsync(id);
            if (result == null) return NotFound();
            return Ok(result);
        }

        [HttpGet("user/{userId}")]
        public async Task<ActionResult<IEnumerable<PermitToWorkDto>>> GetByUserId(int userId)
        {
            var result = await _service.GetByUserIdAsync(userId);
            return Ok(result);
        }

        [HttpGet("status/{status}")]
        public async Task<ActionResult<IEnumerable<PermitToWorkDto>>> GetByStatus(string status)
        {
            var result = await _service.GetByStatusAsync(status);
            return Ok(result);
        }

        [HttpPost]
        public async Task<ActionResult<PermitToWorkDto>> Create(CreatePermitToWorkDto dto)
        {
            try
            {
                var result = await _service.CreateAsync(dto);
                return CreatedAtAction(nameof(GetById), new { id = result.PermitId }, result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> Update(int id, UpdatePermitToWorkDto dto)
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

        [HttpPatch("{id}/auto-approve")]
        public async Task<ActionResult> AutoApprove(int id, [FromBody] int approvedByUserId)
        {
            var result = await _service.AutoApprovePermitAsync(id, approvedByUserId);
            if (!result) return NotFound();
            return NoContent();
        }
    }
}

