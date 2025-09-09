
using EIMS.Dtos.ApprovalDtos;
using EIMS.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EIMS.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ApprovalsController : ControllerBase
    {
        private readonly ApprovalServices _service;

        public ApprovalsController(ApprovalServices service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ApprovalDto>>> GetAll()
        {
            var result = await _service.GetAllAsync();
            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ApprovalDto>> GetById(int id)
        {
            var result = await _service.GetByIdAsync(id);
            if (result == null) return NotFound();
            return Ok(result);
        }

        [HttpGet("with-permit")]
        public async Task<ActionResult<IEnumerable<ApprovalWithPermitDto>>> GetWithPermit()
        {
            var result = await _service.GetAllWithPermitAsync();
            return Ok(result);
        }

        [HttpGet("approver/{approverId}/with-permit")]
        public async Task<ActionResult<IEnumerable<ApprovalWithPermitDto>>> GetByApproverWithPermit(int approverId)
        {
            var result = await _service.GetByApproverIdWithPermitAsync(approverId);
            return Ok(result);
        }

        [HttpGet("approver/{approverId}/pending/with-permit")]
        public async Task<ActionResult<IEnumerable<ApprovalWithPermitDto>>> GetPendingApprovalsWithPermit(int approverId)
        {
            var result = await _service.GetPendingApprovalsWithPermitAsync(approverId);
            return Ok(result);
        }

        [HttpGet("approver/{approverId}")]
        public async Task<ActionResult<IEnumerable<ApprovalDto>>> GetByApproverId(int approverId)
        {
            var result = await _service.GetByApproverIdAsync(approverId);
            return Ok(result);
        }

        [HttpGet("approver/{approverId}/pending")]
        public async Task<ActionResult<IEnumerable<ApprovalDto>>> GetPendingApprovals(int approverId)
        {
            var result = await _service.GetPendingApprovalsAsync(approverId);
            return Ok(result);
        }

        [HttpGet("permit/{permitId}")]
        public async Task<ActionResult<IEnumerable<ApprovalDto>>> GetByPermitId(int permitId)
        {
            var result = await _service.GetByPermitIdAsync(permitId);
            return Ok(result);
        }


        [HttpPost]
        public async Task<ActionResult<ApprovalDto>> Create(CreateApprovalDto dto)
        {
            try
            {
                var result = await _service.CreateAsync(dto);
                return CreatedAtAction(nameof(GetById), new { id = result.ApprovalId }, result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> Update(int id, UpdateApprovalDto dto)
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

        [HttpPatch("{id}/approve")]
        public async Task<ActionResult> Approve(int id, [FromBody] string? comment = null)
        {
            var result = await _service.ApproveAsync(id, comment);
            if (!result) return NotFound();
            return NoContent();
        }

        [HttpPatch("{id}/reject")]
        public async Task<ActionResult> Reject(int id, [FromBody] string? comment = null)
        {
            var result = await _service.RejectAsync(id, comment);
            if (!result) return NotFound();
            return NoContent();
        }
    }
}

