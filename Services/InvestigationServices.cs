using EIMS.Data;
using EIMS.Dtos.InvestigationDtos;
using EIMS.Models;
using Microsoft.EntityFrameworkCore;

namespace EIMS.Services
{
    public class InvestigationServices
    {
        private readonly EimsDbContext _context;

        public InvestigationServices(EimsDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<InvestigationDto>> GetAllAsync()
        {
            return await _context.Investigations
                .Include(i => i.HSEOfficer)
                .Include(i => i.Incident)
                .Select(i => new InvestigationDto
                {
                    InvestigationId = i.InvestigationId,
                    IncidentId = i.IncidentId,
                    IncidentNumber = i.Incident!.IncidentNumber ?? string.Empty,  
                    IncidentTitle = i.Incident.Category ?? string.Empty,
                    HSEOfficerId = i.HSEOfficerId,
                    HSEOfficerName = i.HSEOfficer != null ? i.HSEOfficer.Name : string.Empty,
                    RootCause = i.RootCause,
                    CorrectiveAction = i.CorrectiveAction,
                    PreventiveAction = i.PreventiveAction,
                    CloseDate = i.CloseDate
                })
                .OrderByDescending(i => i.CloseDate)
                .ToListAsync();
        }


        public async Task<InvestigationDto?> GetByIdAsync(int id)
        {
            return await _context.Investigations
                .Include(i => i.HSEOfficer)
                .Include(i => i.Incident)
                .Where(i => i.InvestigationId == id)
                .Select(i => new InvestigationDto
                {
                    InvestigationId = i.InvestigationId,
                    IncidentId = i.IncidentId,
                    HSEOfficerId = i.HSEOfficerId,
                    HSEOfficerName = i.HSEOfficer != null ? i.HSEOfficer.Name : string.Empty,
                    RootCause = i.RootCause,
                    CorrectiveAction = i.CorrectiveAction,
                    PreventiveAction = i.PreventiveAction,
                    CloseDate = i.CloseDate
                })
                .FirstOrDefaultAsync();
        }

        public async Task<InvestigationDto?> GetByIncidentIdAsync(int incidentId)
        {
            return await _context.Investigations
                .Include(i => i.HSEOfficer)
                .Include(i => i.Incident)
                .Where(i => i.IncidentId == incidentId)
                .Select(i => new InvestigationDto
                {
                    InvestigationId = i.InvestigationId,
                    IncidentId = i.IncidentId,
                    HSEOfficerId = i.HSEOfficerId,
                    HSEOfficerName = i.HSEOfficer != null ? i.HSEOfficer.Name : string.Empty,
                    RootCause = i.RootCause,
                    CorrectiveAction = i.CorrectiveAction,
                    PreventiveAction = i.PreventiveAction,
                    CloseDate = i.CloseDate
                })
                .FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<InvestigationDto>> GetByHSEOfficerIdAsync(int hseOfficerId)
        {
            return await _context.Investigations
                .Include(i => i.HSEOfficer)
                .Include(i => i.Incident)
                .Where(i => i.HSEOfficerId == hseOfficerId)
                .Select(i => new InvestigationDto
                {
                    InvestigationId = i.InvestigationId,
                    IncidentId = i.IncidentId,
                    HSEOfficerId = i.HSEOfficerId,
                    HSEOfficerName = i.HSEOfficer != null ? i.HSEOfficer.Name : string.Empty,
                    RootCause = i.RootCause,
                    CorrectiveAction = i.CorrectiveAction,
                    PreventiveAction = i.PreventiveAction,
                    CloseDate = i.CloseDate
                })
                .OrderByDescending(i => i.CloseDate)
                .ToListAsync();
        }

        public async Task<InvestigationDto> CreateAsync(CreateInvestigationDto dto)
        {
            var investigation = new Investigation
            {
                IncidentId = dto.IncidentId,
                HSEOfficerId = dto.HSEOfficerId,
                RootCause = dto.RootCause,
                CorrectiveAction = dto.CorrectiveAction,
                PreventiveAction = dto.PreventiveAction
            };

            _context.Investigations.Add(investigation);
            await _context.SaveChangesAsync();

            // Get the created entity with includes
            var created = await _context.Investigations
                .Include(i => i.HSEOfficer)
                .Include(i => i.Incident)
                .FirstOrDefaultAsync(i => i.InvestigationId == investigation.InvestigationId);

            if (created == null)
            {
                throw new Exception("Failed to create investigation");
            }

            return new InvestigationDto
            {
                InvestigationId = created.InvestigationId,
                IncidentId = created.IncidentId,
                HSEOfficerId = created.HSEOfficerId,
                HSEOfficerName = created.HSEOfficer != null ? created.HSEOfficer.Name : string.Empty,
                RootCause = created.RootCause,
                CorrectiveAction = created.CorrectiveAction,
                PreventiveAction = created.PreventiveAction,
                CloseDate = created.CloseDate
            };
        }

        public async Task<bool> UpdateAsync(int id, UpdateInvestigationDto dto)
        {
            var existing = await _context.Investigations.FindAsync(id);
            if (existing == null) return false;

            if (dto.RootCause != null) existing.RootCause = dto.RootCause;
            if (dto.CorrectiveAction != null) existing.CorrectiveAction = dto.CorrectiveAction;
            if (dto.PreventiveAction != null) existing.PreventiveAction = dto.PreventiveAction;
            if (dto.CloseDate.HasValue) existing.CloseDate = dto.CloseDate.Value;

            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var existing = await _context.Investigations.FindAsync(id);
            if (existing == null) return false;

            _context.Investigations.Remove(existing);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> CloseInvestigationAsync(int id)
        {
            var existing = await _context.Investigations.FindAsync(id);
            if (existing == null) return false;

            existing.CloseDate = DateTime.UtcNow;
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
