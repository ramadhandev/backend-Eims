using EIMS.Data;
using EIMS.Dto.PermitRqDto;
using EIMS.Models;
using Microsoft.EntityFrameworkCore;

namespace EIMS.Services
{
    public class PermitRequirementServices
    {
        private readonly EimsDbContext _context;

        public PermitRequirementServices(EimsDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<PermitRequirementDto>> GetAllAsync()
        {
            return await _context.PermitRequirements
                .Include(pr => pr.RequiredCardType)
                .Select(pr => new PermitRequirementDto
                {
                    RequirementId = pr.RequirementId,
                    WorkType = pr.WorkType,
                    RequiredCardTypeId = pr.RequiredCardTypeId,
                    RequiredCardTypeName = pr.RequiredCardType != null ? pr.RequiredCardType.Name : string.Empty,
                    RequiredTraining = pr.RequiredTraining,
                    Notes = pr.Notes
                })
                .ToListAsync();
        }

        public async Task<PermitRequirementDto?> GetByIdAsync(int id)
        {
            return await _context.PermitRequirements
                .Include(pr => pr.RequiredCardType)
                .Where(pr => pr.RequirementId == id)
                .Select(pr => new PermitRequirementDto
                {
                    RequirementId = pr.RequirementId,
                    WorkType = pr.WorkType,
                    RequiredCardTypeId = pr.RequiredCardTypeId,
                    RequiredCardTypeName = pr.RequiredCardType != null ? pr.RequiredCardType.Name : string.Empty,
                    RequiredTraining = pr.RequiredTraining,
                    Notes = pr.Notes
                })
                .FirstOrDefaultAsync();
        }

        public async Task<PermitRequirementDto?> GetByWorkTypeAsync(string workType)
        {
            return await _context.PermitRequirements
                .Include(pr => pr.RequiredCardType)
                .Where(pr => pr.WorkType == workType)
                .Select(pr => new PermitRequirementDto
                {
                    RequirementId = pr.RequirementId,
                    WorkType = pr.WorkType,
                    RequiredCardTypeId = pr.RequiredCardTypeId,
                    RequiredCardTypeName = pr.RequiredCardType != null ? pr.RequiredCardType.Name : string.Empty,
                    RequiredTraining = pr.RequiredTraining,
                    Notes = pr.Notes
                })
                .FirstOrDefaultAsync();
        }

        public async Task<PermitRequirementDto> CreateAsync(CreatePermitRequirementDto dto)
        {
            var permitRequirement = new PermitRequirement
            {
                WorkType = dto.WorkType,
                RequiredCardTypeId = dto.RequiredCardTypeId,
                RequiredTraining = dto.RequiredTraining,
                Notes = dto.Notes
            };

            _context.PermitRequirements.Add(permitRequirement);
            await _context.SaveChangesAsync();

            // Get the created entity with includes
            var created = await _context.PermitRequirements
                .Include(pr => pr.RequiredCardType)
                .FirstOrDefaultAsync(pr => pr.RequirementId == permitRequirement.RequirementId);

            if (created == null)
            {
                throw new Exception("Failed to create permit requirement");
            }

            return new PermitRequirementDto
            {
                RequirementId = created.RequirementId,
                WorkType = created.WorkType,
                RequiredCardTypeId = created.RequiredCardTypeId,
                RequiredCardTypeName = created.RequiredCardType != null ? created.RequiredCardType.Name : string.Empty,
                RequiredTraining = created.RequiredTraining,
                Notes = created.Notes
            };
        }

        public async Task<bool> UpdateAsync(int id, UpdatePermitRequirementDto dto)
        {
            var existing = await _context.PermitRequirements.FindAsync(id);
            if (existing == null) return false;

            if (dto.WorkType != null) existing.WorkType = dto.WorkType;
            if (dto.RequiredCardTypeId.HasValue) existing.RequiredCardTypeId = dto.RequiredCardTypeId.Value;
            if (dto.RequiredTraining != null) existing.RequiredTraining = dto.RequiredTraining;
            if (dto.Notes != null) existing.Notes = dto.Notes;

            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var existing = await _context.PermitRequirements.FindAsync(id);
            if (existing == null) return false;

            _context.PermitRequirements.Remove(existing);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
