
using EIMS.Dtos.HseCardDtos;
using EIMS.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EIMS.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HseCardsController : ControllerBase
    {
        private readonly HseCardServices _service;

        public HseCardsController(HseCardServices service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<HseCardDto>>> GetAll()
        {
            var result = await _service.GetAllAsync();
            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<HseCardDto>> GetById(int id)
        {
            var result = await _service.GetByIdAsync(id);
            if (result == null) return NotFound();
            return Ok(result);
        }

        [HttpGet("user/{userId}")]
        public async Task<ActionResult<IEnumerable<HseCardDto>>> GetByUserId(int userId)
        {
            var result = await _service.GetByUserIdAsync(userId);
            return Ok(result);
        }

        [HttpGet("expiring")]
        public async Task<ActionResult<IEnumerable<HseCardDto>>> GetExpiringCards([FromQuery] int days = 30)
        {
            var result = await _service.GetExpiringCardsAsync(days);
            return Ok(result);
        }

        [HttpPost]
        public async Task<ActionResult<HseCardDto>> Create(CreateHseCardDto dto)
        {
            try
            {
                var result = await _service.CreateAsync(dto);
                return CreatedAtAction(nameof(GetById), new { id = result.CardId }, result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> Update(int id, UpdateHseCardDto dto)
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

        [HttpPatch("{id}/renew")]
        public async Task<ActionResult> RenewCard(int id, [FromBody] DateTime newExpiryDate)
        {
            var result = await _service.RenewCardAsync(id, newExpiryDate);
            if (!result) return NotFound();
            return NoContent();
        }
    }
}
