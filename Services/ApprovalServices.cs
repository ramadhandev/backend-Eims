using EIMS.Data;
using EIMS.Dto;
using EIMS.Dtos.ApprovalDtos;
using EIMS.Dtos.PermitToWorkDtos;
using EIMS.Models;
using Microsoft.EntityFrameworkCore;

namespace EIMS.Services
{
    public class ApprovalServices
    {
        private readonly EimsDbContext _context;

        public ApprovalServices(EimsDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<ApprovalDto>> GetAllAsync()
        {
            return await _context.Approvals
                .Include(a => a.Approver)
                .Include(a => a.Permit)
                .Select(a => new ApprovalDto
                {
                    ApprovalId = a.ApprovalId,
                    PermitId = a.PermitId,
                    ApproverId = a.ApproverId,
                    ApproverName = a.Approver != null ? a.Approver.Name : string.Empty,
                    Role = a.Role,
                    Decision = a.Decision,
                    DecisionDate = a.DecisionDate,
                    Comment = a.Comment
                })
                .OrderByDescending(a => a.DecisionDate)
                .ToListAsync();
        }

        public async Task<ApprovalDto?> GetByIdAsync(int id)
        {
            return await _context.Approvals
                .Include(a => a.Approver)
                .Include(a => a.Permit)
                .Where(a => a.ApprovalId == id)
                .Select(a => new ApprovalDto
                {
                    ApprovalId = a.ApprovalId,
                    PermitId = a.PermitId,
                    ApproverId = a.ApproverId,
                    ApproverName = a.Approver != null ? a.Approver.Name : string.Empty,
                    Role = a.Role,
                    Decision = a.Decision,
                    DecisionDate = a.DecisionDate,
                    Comment = a.Comment
                })
                .FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<ApprovalDto>> GetByPermitIdAsync(int permitId)
        {
            return await _context.Approvals
                .Include(a => a.Approver)
                .Include(a => a.Permit)
                .Where(a => a.PermitId == permitId)
                .Select(a => new ApprovalDto
                {
                    ApprovalId = a.ApprovalId,
                    PermitId = a.PermitId,
                    ApproverId = a.ApproverId,
                    ApproverName = a.Approver != null ? a.Approver.Name : string.Empty,
                    Role = a.Role,
                    Decision = a.Decision,
                    DecisionDate = a.DecisionDate,
                    Comment = a.Comment
                })
                .OrderBy(a => a.DecisionDate)
                .ToListAsync();
        }

        public async Task<IEnumerable<ApprovalDto>> GetByApproverIdAsync(int approverId)
        {
            return await _context.Approvals
                .Include(a => a.Approver)
                .Include(a => a.Permit)
                .Where(a => a.ApproverId == approverId)
                .Select(a => new ApprovalDto
                {
                    ApprovalId = a.ApprovalId,
                    PermitId = a.PermitId,
                    ApproverId = a.ApproverId,
                    ApproverName = a.Approver != null ? a.Approver.Name : string.Empty,
                    Role = a.Role,
                    Decision = a.Decision,
                    DecisionDate = a.DecisionDate,
                    Comment = a.Comment
                })
                .OrderByDescending(a => a.DecisionDate)
                .ToListAsync();
        }

        public async Task<IEnumerable<ApprovalDto>> GetPendingApprovalsAsync(int approverId)
        {
            return await _context.Approvals
                .Include(a => a.Approver)
                .Include(a => a.Permit)
                .Where(a => a.ApproverId == approverId && a.Decision == "Pending")
                .Select(a => new ApprovalDto
                {
                    ApprovalId = a.ApprovalId,
                    PermitId = a.PermitId,
                    ApproverId = a.ApproverId,
                    ApproverName = a.Approver != null ? a.Approver.Name : string.Empty,
                    Role = a.Role,
                    Decision = a.Decision,
                    DecisionDate = a.DecisionDate,
                    Comment = a.Comment
                })
                .OrderBy(a => a.DecisionDate)
                .ToListAsync();
        }

        public async Task<IEnumerable<ApprovalWithPermitDto>> GetAllWithPermitAsync()
        {
            return await _context.Approvals
                .Include(a => a.Approver)
                .Include(a => a.Permit)
                    .ThenInclude(p => p!.User) // Tambahkan null-forgiving operator
                .Select(a => new ApprovalWithPermitDto
                {
                    ApprovalId = a.ApprovalId,
                    PermitId = a.PermitId,
                    ApproverId = a.ApproverId,
                    ApproverName = a.Approver != null ? a.Approver.Name : string.Empty,
                    Role = a.Role,
                    Decision = a.Decision,
                    DecisionDate = a.DecisionDate,
                    Comment = a.Comment,
                    PermitData = a.Permit != null ? new PermitBasicDto
                    {
                        PermitId = a.Permit.PermitId,
                        WorkType = a.Permit.WorkType,
                        Location = a.Permit.Location,
                        UserName = a.Permit.User != null ? a.Permit.User.Name : string.Empty,
                        StartDate = a.Permit.StartDate,
                        EndDate = a.Permit.EndDate,
                        Status = a.Permit.Status
                    } : null
                })
                .OrderByDescending(a => a.DecisionDate)
                .ToListAsync();
        }

        public async Task<IEnumerable<ApprovalWithPermitDto>> GetByApproverIdWithPermitAsync(int approverId)
        {
            return await _context.Approvals
                .Include(a => a.Approver)
                .Include(a => a.Permit)
                    .ThenInclude(p => p!.User) // Tambahkan null-forgiving operator
                .Where(a => a.ApproverId == approverId)
                .Select(a => new ApprovalWithPermitDto
                {
                    ApprovalId = a.ApprovalId,
                    PermitId = a.PermitId,
                    ApproverId = a.ApproverId,
                    ApproverName = a.Approver != null ? a.Approver.Name : string.Empty,
                    Role = a.Role,
                    Decision = a.Decision,
                    DecisionDate = a.DecisionDate,
                    Comment = a.Comment,
                    PermitData = a.Permit != null ? new PermitBasicDto
                    {
                        PermitId = a.Permit.PermitId,
                        WorkType = a.Permit.WorkType,
                        Location = a.Permit.Location,
                        UserName = a.Permit.User != null ? a.Permit.User.Name : string.Empty,
                        StartDate = a.Permit.StartDate,
                        EndDate = a.Permit.EndDate,
                        Status = a.Permit.Status
                    } : null
                })
                .OrderByDescending(a => a.DecisionDate)
                .ToListAsync();
        }


        public async Task<IEnumerable<ApprovalWithPermitDto>> GetPendingApprovalsWithPermitAsync(int approverId)
        {
            return await _context.Approvals
                .Include(a => a.Approver)
                .Include(a => a.Permit)
                    .ThenInclude(p => p!.User) // Tambahkan null-forgiving operator
                .Where(a => a.ApproverId == approverId && a.Decision == "Pending")
                .Select(a => new ApprovalWithPermitDto
                {
                    ApprovalId = a.ApprovalId,
                    PermitId = a.PermitId,
                    ApproverId = a.ApproverId,
                    ApproverName = a.Approver != null ? a.Approver.Name : string.Empty,
                    Role = a.Role,
                    Decision = a.Decision,
                    DecisionDate = a.DecisionDate,
                    Comment = a.Comment,
                    PermitData = a.Permit != null ? new PermitBasicDto
                    {
                        PermitId = a.Permit.PermitId,
                        WorkType = a.Permit.WorkType,
                        Location = a.Permit.Location,
                        UserName = a.Permit.User != null ? a.Permit.User.Name : string.Empty,
                        StartDate = a.Permit.StartDate,
                        EndDate = a.Permit.EndDate,
                        Status = a.Permit.Status
                    } : null
                })
                .OrderBy(a => a.DecisionDate)
                .ToListAsync();
        }
        public async Task<ApprovalDto> CreateAsync(CreateApprovalDto dto)
        {
            var approval = new Approval
            {
                PermitId = dto.PermitId,
                ApproverId = dto.ApproverId,
                Role = dto.Role,
                Decision = "Pending",
                DecisionDate = DateTime.UtcNow,
                Comment = dto.Comment
            };

            _context.Approvals.Add(approval);
            await _context.SaveChangesAsync();

            var created = await _context.Approvals
                .Include(a => a.Approver)
                .Include(a => a.Permit)
                .FirstOrDefaultAsync(a => a.ApprovalId == approval.ApprovalId);

            if (created == null) throw new Exception("Failed to create approval");

            return new ApprovalDto
            {
                ApprovalId = created.ApprovalId,
                PermitId = created.PermitId,
                ApproverId = created.ApproverId,
                ApproverName = created.Approver != null ? created.Approver.Name : string.Empty,
                Role = created.Role,
                Decision = created.Decision,
                DecisionDate = created.DecisionDate,
                Comment = created.Comment
            };
        }

        public async Task<bool> UpdateAsync(int id, UpdateApprovalDto dto)
        {
            var existing = await _context.Approvals.FindAsync(id);
            if (existing == null) return false;

            existing.Decision = dto.Decision;
            existing.Comment = dto.Comment;

            // Update decisionDate hanya jika status berubah dari/to Pending
            if (existing.Decision != "Pending" && dto.Decision != "Pending")
            {
                existing.DecisionDate = DateTime.UtcNow;
            }

            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var existing = await _context.Approvals.FindAsync(id);
            if (existing == null) return false;

            _context.Approvals.Remove(existing);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> ApproveAsync(int id, string? comment = null)
        {
            var existing = await _context.Approvals.FindAsync(id);
            if (existing == null) return false;

            existing.Decision = "Approved";
            existing.Comment = comment;
            existing.DecisionDate = DateTime.UtcNow; // Update decisionDate

            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> RejectAsync(int id, string? comment = null)
        {
            var existing = await _context.Approvals.FindAsync(id);
            if (existing == null) return false;

            existing.Decision = "Rejected";
            existing.Comment = comment;
            existing.DecisionDate = DateTime.UtcNow;

            await _context.SaveChangesAsync();
            return true;
        }
    }


}
