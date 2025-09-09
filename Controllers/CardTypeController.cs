using EIMS.Dtos.CardTypeDto;
using EIMS.Models;
using EIMS.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EIMS.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CardTypeController : ControllerBase
    {
        private readonly CardTypeServices _services;
        public CardTypeController(CardTypeServices services)
        {
            _services = services;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var cardTypes = await _services.GetAllAsync();
            return Ok(cardTypes);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var card = await _services.GetByIdAsync(id);
            if (card == null) return NotFound();
            return Ok(card);
        }

        [HttpPost]

        public async Task<IActionResult> Create([FromBody]CreateCardTypeDto cardType)
        {
            var created = await _services.CreateAsync(cardType);
            return CreatedAtAction(nameof(GetById   ), new {id = created.CardTypeId}, created);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] CreateCardTypeDto cardType)
        {
            var update = await _services.UpdateAsync(id, cardType);
            if (!update) return NotFound();
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var delete = await _services.DeleteAsync(id);
            if (!delete) return NotFound();
            return NoContent();
        }
    }
}
