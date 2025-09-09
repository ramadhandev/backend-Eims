using EIMS.Data;
using EIMS.Dto.IncidentDto;
using EIMS.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Writers;

namespace EIMS.Services
{
    public class IncidentReportServices
    {
        private readonly EimsDbContext _context;
        private readonly IFileStorageService _fileStorageService;

        public IncidentReportServices(EimsDbContext context, IFileStorageService fileStorageService)
        {
            _context = context;
            _fileStorageService = fileStorageService;
        }

        public async Task<IEnumerable<IncidentReportDto>> GetAllAsync()
        {
            return await _context.IncidentReports
                .Include(i => i.User)
                .Select(i => new IncidentReportDto
                {
                    IncidentId = i.IncidentId,
                    UserId = i.UserId,
                    UserName = i.User != null ? i.User.Name : string.Empty,
                    Date = i.Date,
                    Location = i.Location,
                    Category = i.Category,
                    Description = i.Description,
                    PhotoURL = i.PhotoURL,
                    Status = i.Status ?? "Reported"
                })
                .OrderByDescending(i => i.Date)
                .ToListAsync();
        }


        public async Task<IncidentReportDto?> GetByIdAsync(int id)
        {
            return await _context.IncidentReports
                .Include(i => i.User)
                .Where(i => i.IncidentId == id)
                .Select(i => new IncidentReportDto
                {
                    IncidentId = i.IncidentId,
                    UserId = i.UserId,
                    UserName = i.User != null ? i.User.Name : String.Empty,
                    Date = i.Date,
                    Location = i.Location,
                    Category = i.Category,
                    Description = i.Description,
                    PhotoURL = i.PhotoURL,
                    Status = i.Status ?? "Reported"
                })
                .FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<IncidentReportDto>> GetByUserIdAsync(int userId)
        {
            return await _context.IncidentReports
                .Include (i => i.User)
                .Where (i => i.UserId == userId)
                .Select(i => new IncidentReportDto
                {
                    IncidentId= i.IncidentId,
                    UserId = i.UserId,
                    UserName = i.User !=null ? i.User.Name : String.Empty, 
                    Date = i.Date,
                    Location = i.Location,
                    Category = i.Category,
                    Description = i.Description,
                    PhotoURL = i.PhotoURL,
                    Status = i.Status ?? "Reported"
                })
                .OrderByDescending (i => i.Date)
                .ToListAsync();
        }

        public async Task<IEnumerable<IncidentReportDto>> GetByCategoryAsync(string category)
        {
            return await _context.IncidentReports
                .Include (i => i.User)
                .Where(i => i.Category == category)
                .Select(e =>  new IncidentReportDto
                {
                    IncidentId = e.IncidentId,
                    UserId = e.UserId,
                    UserName = e.User !=null ? e.User.Name : String.Empty,
                    Date= e.Date,
                    Location = e.Location,
                    Category = e.Category,
                    Description = e.Description,
                    PhotoURL = e.PhotoURL,
                    Status = e.Status ?? "Reported"
                    
                })
                .OrderByDescending (i => i.Date)
                .ToListAsync();
        }

        public async Task<IncidentReportDto> CreateAsync(CreateIncidentReportDto dto, IFormFile? photo = null)
        {
            string? photoUrl = null;

            // Handle file upload jika ada
            if (photo != null && photo.Length > 0)
            {
                try
                {
                    photoUrl = await _fileStorageService.SaveFileAsync(photo, "incidents");
                    photoUrl = _fileStorageService.GetFileUrl(photoUrl);
                }
                catch (Exception ex)
                {
                    throw new Exception($"Gagal mengupload gambar: {ex.Message}");
                }
            }

            // Generate incident number
            string incidentNumber = await GenerateIncidentNumber();

            var incidentReport = new IncidentReport
            {
                IncidentNumber = incidentNumber, // Set nomor insiden
                UserId = dto.UserId,
                Date = dto.Date,
                Location = dto.Location,
                Category = dto.Category,
                Description = dto.Description,
                PhotoURL = photoUrl,
                Status = "Reported"
            };

            _context.IncidentReports.Add(incidentReport);
            await _context.SaveChangesAsync();

            // Get the created entity with includes
            var created = await _context.IncidentReports
                .Include(i => i.User)
                .FirstOrDefaultAsync(i => i.IncidentId == incidentReport.IncidentId);

            if (created == null)
            {
                throw new Exception("Failed To Create Incident Report");
            }

            return new IncidentReportDto
            {
                IncidentId = created.IncidentId,
                IncidentNumber = created.IncidentNumber, // Tambahkan ini
                UserId = created.UserId,
                UserName = created.User != null ? created.User.Name : string.Empty,
                Date = created.Date,
                Location = created.Location,
                Category = created.Category,
                Description = created.Description,
                PhotoURL = created.PhotoURL,
                Status = "Reported"
            };
        }

        // Method untuk generate incident number
        private async Task<string> GenerateIncidentNumber()
        {
            var lastIncident = await _context.IncidentReports
                .OrderByDescending(i => i.IncidentId)
                .FirstOrDefaultAsync();

            int nextId = lastIncident != null ? lastIncident.IncidentId + 1 : 1;
            return $"INC{nextId.ToString().PadLeft(7, '0')}";
        }
        public async Task<bool> UpdateAsync(int id, UpdateIncidentReportDto dto)
        {
            var existing = await _context.IncidentReports.FindAsync(id);
            if (existing == null) return false;

            if (dto.Location != null) existing.Location = dto.Location;
            if (dto.Description != null) existing.Description = dto.Description;
            if (dto.PhotoURL != null) existing.PhotoURL = dto.PhotoURL;
            if (dto.Status != null) existing.Status = dto.Status;

            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var existing = await _context.IncidentReports.FindAsync(id);
            if (existing == null) return false;

            _context.IncidentReports.Remove(existing);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> UpdateStatusAsync(int id, string status)
        {
            var existing = await _context.IncidentReports.FindAsync(id);
            if (existing == null) return false;

            existing.Status = status;
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
