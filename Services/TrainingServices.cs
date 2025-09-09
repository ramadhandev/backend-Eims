using EIMS.Data;
using EIMS.Dtos.TrainingDtos;
using EIMS.Models;
using Microsoft.EntityFrameworkCore;

namespace EIMS.Services
{
    public class TrainingServices
    {
        private readonly EimsDbContext _context;

        public TrainingServices(EimsDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<TrainingDto>> GetAllAsync()
        {
            return await _context.Trainings
                .Include(t => t.User)
                .Select(t => new TrainingDto
                {
                    TrainingId = t.TrainingId,
                    UserId = t.UserId,
                    UserName = t.User != null ? t.User.Name : string.Empty,
                    TrainingName = t.TrainingName,
                    CompletionDate = t.CompletionDate,
                    ExpiryDate = t.ExpiryDate,
                    CertificateURL = t.CertificateURL,
                    Status = t.Status,
                    Notes = t.Notes
                })
                .OrderByDescending(t => t.CompletionDate)
                .ToListAsync();
        }

        public async Task<TrainingDto?> GetByIdAsync(int id)
        {
            return await _context.Trainings
                .Include(t => t.User)
                .Where(t => t.TrainingId == id)
                .Select(t => new TrainingDto
                {
                    TrainingId = t.TrainingId,
                    UserId = t.UserId,
                    UserName = t.User != null ? t.User.Name : string.Empty,
                    TrainingName = t.TrainingName,
                    CompletionDate = t.CompletionDate,
                    ExpiryDate = t.ExpiryDate,
                    CertificateURL = t.CertificateURL,
                    Status = t.Status,
                    Notes = t.Notes
                })
                .FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<TrainingDto>> GetByUserIdAsync(int userId)
        {
            return await _context.Trainings
                .Include(t => t.User)
                .Where(t => t.UserId == userId)
                .Select(t => new TrainingDto
                {
                    TrainingId = t.TrainingId,
                    UserId = t.UserId,
                    UserName = t.User != null ? t.User.Name : string.Empty,
                    TrainingName = t.TrainingName,
                    CompletionDate = t.CompletionDate,
                    ExpiryDate = t.ExpiryDate,
                    CertificateURL = t.CertificateURL,
                    Status = t.Status,
                    Notes = t.Notes
                })
                .OrderByDescending(t => t.CompletionDate)
                .ToListAsync();
        }

        public async Task<IEnumerable<TrainingDto>> GetExpiringTrainingsAsync(int daysThreshold = 30)
        {
            var thresholdDate = DateTime.UtcNow.AddDays(daysThreshold);
            return await _context.Trainings
                .Include(t => t.User)
                .Where(t => t.ExpiryDate <= thresholdDate && t.ExpiryDate > DateTime.UtcNow)
                .Select(t => new TrainingDto
                {
                    TrainingId = t.TrainingId,
                    UserId = t.UserId,
                    UserName = t.User != null ? t.User.Name : string.Empty,
                    TrainingName = t.TrainingName,
                    CompletionDate = t.CompletionDate,
                    ExpiryDate = t.ExpiryDate,
                    CertificateURL = t.CertificateURL,
                    Status = t.Status,
                    Notes = t.Notes
                })
                .OrderBy(t => t.ExpiryDate)
                .ToListAsync();
        }

        public async Task<TrainingDto> CreateAsync(CreateTrainingDto dto)
        {
            var training = new Training
            {
                UserId = dto.UserId,
                TrainingName = dto.TrainingName,
                CompletionDate = dto.CompletionDate,
                ExpiryDate = dto.ExpiryDate,
                CertificateURL = dto.CertificateURL,
                Status = "Completed",
                Notes = dto.Notes
            };

            _context.Trainings.Add(training);
            await _context.SaveChangesAsync();

            var created = await _context.Trainings
                .Include(t => t.User)
                .FirstOrDefaultAsync(t => t.TrainingId == training.TrainingId);

            if (created == null) throw new Exception("Failed to create training");

            return new TrainingDto
            {
                TrainingId = created.TrainingId,
                UserId = created.UserId,
                UserName = created.User != null ? created.User.Name : string.Empty,
                TrainingName = created.TrainingName,
                CompletionDate = created.CompletionDate,
                ExpiryDate = created.ExpiryDate,
                CertificateURL = created.CertificateURL,
                Status = created.Status,
                Notes = created.Notes
            };
        }

        public async Task<bool> UpdateAsync(int id, UpdateTrainingDto dto)
        {
            var existing = await _context.Trainings.FindAsync(id);
            if (existing == null) return false;

            if (dto.TrainingName != null) existing.TrainingName = dto.TrainingName;
            if (dto.CompletionDate.HasValue) existing.CompletionDate = dto.CompletionDate.Value;
            if (dto.ExpiryDate.HasValue) existing.ExpiryDate = dto.ExpiryDate.Value;
            if (dto.CertificateURL != null) existing.CertificateURL = dto.CertificateURL;
            if (dto.Status != null) existing.Status = dto.Status;
            if (dto.Notes != null) existing.Notes = dto.Notes;

            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var existing = await _context.Trainings.FindAsync(id);
            if (existing == null) return false;

            _context.Trainings.Remove(existing);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> UpdateStatusAsync(int id, string status)
        {
            var existing = await _context.Trainings.FindAsync(id);
            if (existing == null) return false;

            existing.Status = status;
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
