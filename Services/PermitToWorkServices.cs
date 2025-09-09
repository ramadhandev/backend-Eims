using Microsoft.EntityFrameworkCore;
using EIMS.Data;
using EIMS.Models;
using EIMS.Dtos.PermitToWorkDtos;
using EIMS.Dtos.ApprovalDtos;
using EIMS.Dto.PermitToWorkDtos;


namespace EIMS.Services
{
    public class PermitToWorkServices
    {
        private readonly EimsDbContext _context;

        public PermitToWorkServices(EimsDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<PermitToWorkDto>> GetAllAsync()
        {
            return await _context.PermitToWorks
                .Include(p => p.User)
                .Include(p => p.RequiredCardType)
                .Include(p => p.AutoApprovedByUser)
                .Include(p => p.Approvals)
                    .ThenInclude(a => a.Approver)
                .Select(p => new PermitToWorkDto
                {
                    PermitId = p.PermitId,
                    UserId = p.UserId,
                    UserName = p.User != null ? p.User.Name : string.Empty,
                    WorkType = p.WorkType,
                    Location = p.Location,
                    StartDate = p.StartDate,
                    EndDate = p.EndDate,
                    RequiredCardTypeId = p.RequiredCardTypeId,
                    RequiredCardTypeName = p.RequiredCardType != null ? p.RequiredCardType.Name : string.Empty,
                    Status = p.Status,
                    AutoApproved = p.AutoApproved,
                    AutoApprovedDate = p.AutoApprovedDate,
                    AutoApprovedBy = p.AutoApprovedBy,
                    AutoApprovedByName = p.AutoApprovedByUser != null ? p.AutoApprovedByUser.Name : string.Empty,
                    CreatedAt = p.CreatedAt,
                    Notes = p.Notes,
                    Approvals = p.Approvals.Select(a => new ApprovalDto
                    {
                        ApprovalId = a.ApprovalId,
                        PermitId = a.PermitId,
                        ApproverId = a.ApproverId,
                        ApproverName = a.Approver != null ? a.Approver.Name : string.Empty,
                        Role = a.Role,
                        Decision = a.Decision,
                        DecisionDate = a.DecisionDate,
                        Comment = a.Comment
                    }).ToList()
                })
                .OrderByDescending(p => p.CreatedAt)
                .ToListAsync();
        }

        public async Task<PermitToWorkDto?> GetByIdAsync(int id)
        {
            return await _context.PermitToWorks
                .Include(p => p.User)
                .Include(p => p.RequiredCardType)
                .Include(p => p.AutoApprovedByUser)
                .Include(p => p.Approvals)
                    .ThenInclude(a => a.Approver)
                .Where(p => p.PermitId == id)
                .Select(p => new PermitToWorkDto
                {
                    PermitId = p.PermitId,
                    UserId = p.UserId,
                    UserName = p.User != null ? p.User.Name : string.Empty,
                    WorkType = p.WorkType,
                    Location = p.Location,
                    StartDate = p.StartDate,
                    EndDate = p.EndDate,
                    RequiredCardTypeId = p.RequiredCardTypeId,
                    RequiredCardTypeName = p.RequiredCardType != null ? p.RequiredCardType.Name : string.Empty,
                    Status = p.Status,
                    AutoApproved = p.AutoApproved,
                    AutoApprovedDate = p.AutoApprovedDate,
                    AutoApprovedBy = p.AutoApprovedBy,
                    AutoApprovedByName = p.AutoApprovedByUser != null ? p.AutoApprovedByUser.Name : string.Empty,
                    CreatedAt = p.CreatedAt,
                    Notes = p.Notes,
                    Approvals = p.Approvals.Select(a => new ApprovalDto
                    {
                        ApprovalId = a.ApprovalId,
                        PermitId = a.PermitId,
                        ApproverId = a.ApproverId,
                        ApproverName = a.Approver != null ? a.Approver.Name : string.Empty,
                        Role = a.Role,
                        Decision = a.Decision,
                        DecisionDate = a.DecisionDate,
                        Comment = a.Comment
                    }).ToList()
                })
                .FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<PermitToWorkDto>> GetByUserIdAsync(int userId)
        {
            return await _context.PermitToWorks
                .Include(p => p.User)
                .Include(p => p.RequiredCardType)
                .Include(p => p.AutoApprovedByUser)
                .Include(p => p.Approvals)
                    .ThenInclude(a => a.Approver)
                .Where(p => p.UserId == userId)
                .Select(p => new PermitToWorkDto
                {
                    PermitId = p.PermitId,
                    UserId = p.UserId,
                    UserName = p.User != null ? p.User.Name : string.Empty,
                    WorkType = p.WorkType,
                    Location = p.Location,
                    StartDate = p.StartDate,
                    EndDate = p.EndDate,
                    RequiredCardTypeId = p.RequiredCardTypeId,
                    RequiredCardTypeName = p.RequiredCardType != null ? p.RequiredCardType.Name : string.Empty,
                    Status = p.Status,
                    AutoApproved = p.AutoApproved,
                    AutoApprovedDate = p.AutoApprovedDate,
                    AutoApprovedBy = p.AutoApprovedBy,
                    AutoApprovedByName = p.AutoApprovedByUser != null ? p.AutoApprovedByUser.Name : string.Empty,
                    CreatedAt = p.CreatedAt,
                    Notes = p.Notes,
                    Approvals = p.Approvals.Select(a => new ApprovalDto
                    {
                        ApprovalId = a.ApprovalId,
                        PermitId = a.PermitId,
                        ApproverId = a.ApproverId,
                        ApproverName = a.Approver != null ? a.Approver.Name : string.Empty,
                        Role = a.Role,
                        Decision = a.Decision,
                        DecisionDate = a.DecisionDate,
                        Comment = a.Comment
                    }).ToList()
                })
                .OrderByDescending(p => p.CreatedAt)
                .ToListAsync();
        }

        public async Task<IEnumerable<PermitToWorkDto>> GetByStatusAsync(string status)
        {
            return await _context.PermitToWorks
                .Include(p => p.User)
                .Include(p => p.RequiredCardType)
                .Include(p => p.AutoApprovedByUser)
                .Include(p => p.Approvals)
                    .ThenInclude(a => a.Approver)
                .Where(p => p.Status == status)
                .Select(p => new PermitToWorkDto
                {
                    PermitId = p.PermitId,
                    UserId = p.UserId,
                    UserName = p.User != null ? p.User.Name : string.Empty,
                    WorkType = p.WorkType,
                    Location = p.Location,
                    StartDate = p.StartDate,
                    EndDate = p.EndDate,
                    RequiredCardTypeId = p.RequiredCardTypeId,
                    RequiredCardTypeName = p.RequiredCardType != null ? p.RequiredCardType.Name : string.Empty,
                    Status = p.Status,
                    AutoApproved = p.AutoApproved,
                    AutoApprovedDate = p.AutoApprovedDate,
                    AutoApprovedBy = p.AutoApprovedBy,
                    AutoApprovedByName = p.AutoApprovedByUser != null ? p.AutoApprovedByUser.Name : string.Empty,
                    CreatedAt = p.CreatedAt,
                    Notes = p.Notes,
                    Approvals = p.Approvals.Select(a => new ApprovalDto
                    {
                        ApprovalId = a.ApprovalId,
                        PermitId = a.PermitId,
                        ApproverId = a.ApproverId,
                        ApproverName = a.Approver != null ? a.Approver.Name : string.Empty,
                        Role = a.Role,
                        Decision = a.Decision,
                        DecisionDate = a.DecisionDate,
                        Comment = a.Comment
                    }).ToList()
                })
                .OrderByDescending(p => p.CreatedAt)
                .ToListAsync();
        }

        public async Task<PermitToWorkDto> CreateAsync(CreatePermitToWorkDto dto)
        {
            var permit = new PermitToWork
            {
                UserId = dto.UserId,
                WorkType = dto.WorkType,
                Location = dto.Location,
                StartDate = dto.StartDate,
                EndDate = dto.EndDate,
                RequiredCardTypeId = dto.RequiredCardTypeId,
                Status = "Requested",
                AutoApproved = false,
                CreatedAt = DateTime.UtcNow,
                Notes = dto.Notes
            };

            _context.PermitToWorks.Add(permit);
            await _context.SaveChangesAsync();

            // AUTO-CREATE APPROVAL RECORDS - TAMBAHKAN INI
            await CreateAutoApprovals(permit.PermitId);

            var created = await _context.PermitToWorks
                .Include(p => p.User)
                .Include(p => p.RequiredCardType)
                .Include(p => p.AutoApprovedByUser)
                .Include(p => p.Approvals)
                    .ThenInclude(a => a.Approver)
                .FirstOrDefaultAsync(p => p.PermitId == permit.PermitId);

            if (created == null) throw new Exception("Failed to create permit");

            return new PermitToWorkDto
            {
                PermitId = created.PermitId,
                UserId = created.UserId,
                UserName = created.User != null ? created.User.Name : string.Empty,
                WorkType = created.WorkType,
                Location = created.Location,
                StartDate = created.StartDate,
                EndDate = created.EndDate,
                RequiredCardTypeId = created.RequiredCardTypeId,
                RequiredCardTypeName = created.RequiredCardType != null ? created.RequiredCardType.Name : string.Empty,
                Status = created.Status,
                AutoApproved = created.AutoApproved,
                AutoApprovedDate = created.AutoApprovedDate,
                AutoApprovedBy = created.AutoApprovedBy,
                AutoApprovedByName = created.AutoApprovedByUser != null ? created.AutoApprovedByUser.Name : string.Empty,
                CreatedAt = created.CreatedAt,
                Notes = created.Notes,
                Approvals = created.Approvals.Select(a => new ApprovalDto
                {
                    ApprovalId = a.ApprovalId,
                    PermitId = a.PermitId,
                    ApproverId = a.ApproverId,
                    ApproverName = a.Approver != null ? a.Approver.Name : string.Empty,
                    Role = a.Role,
                    Decision = a.Decision,
                    DecisionDate = a.DecisionDate,
                    Comment = a.Comment
                }).ToList()
            };
        }

        private async Task CreateAutoApprovals(int permitId)
        {
            // Dapatkan user yang memiliki role sebagai approver
            var approvers = await _context.Users
                .Where(u => u.Role == "Supervisor" || u.Role == "Manager" || u.Role == "HSE Officer" || u.Role == "Approver")
                .ToListAsync();

            foreach (var approver in approvers)
            {
                var approval = new Approval
                {
                    PermitId = permitId,
                    ApproverId = approver.UserId,
                    Role = approver.Role,
                    Decision = "Pending",
                    DecisionDate = DateTime.UtcNow
                };

                _context.Approvals.Add(approval);
            }

            await _context.SaveChangesAsync();
        }
        public async Task<bool> UpdateAsync(int id, UpdatePermitToWorkDto dto)
        {
            var existing = await _context.PermitToWorks.FindAsync(id);
            if (existing == null) return false;

            if (dto.Location != null) existing.Location = dto.Location;
            if (dto.StartDate.HasValue) existing.StartDate = dto.StartDate.Value;
            if (dto.EndDate.HasValue) existing.EndDate = dto.EndDate.Value;
            if (dto.RequiredCardTypeId.HasValue) existing.RequiredCardTypeId = dto.RequiredCardTypeId.Value;
            if (dto.Notes != null) existing.Notes = dto.Notes;

            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var existing = await _context.PermitToWorks.FindAsync(id);
            if (existing == null) return false;

            _context.PermitToWorks.Remove(existing);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> UpdateStatusAsync(int id, string status)
        {
            var existing = await _context.PermitToWorks.FindAsync(id);
            if (existing == null) return false;

            existing.Status = status;
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> AutoApprovePermitAsync(int id, int approvedByUserId)
        {
            var existing = await _context.PermitToWorks.FindAsync(id);
            if (existing == null) return false;

            existing.Status = "Approved";
            existing.AutoApproved = true;
            existing.AutoApprovedBy = approvedByUserId;
            existing.AutoApprovedDate = DateTime.UtcNow;

            await _context.SaveChangesAsync();
            return true;
        }
    }
}
