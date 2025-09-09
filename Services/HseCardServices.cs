using EIMS.Data;
using EIMS.Dtos.HseCardDtos;
using EIMS.Models;
using Microsoft.EntityFrameworkCore;

namespace EIMS.Services
{
    public class HseCardServices
    {
        private readonly EimsDbContext _context;

        public HseCardServices(EimsDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<HseCardDto>> GetAllAsync()
        {
            return await _context.HseCards
                .Include(h => h.User)
                .Include(h => h.CardType)
                .Include(h => h.IssueByUser)
                .Select(h => new HseCardDto
                {
                    CardId = h.CardId,
                    UserId = h.UserId,
                    UserName = h.User != null ? h.User.Name : string.Empty,
                    CardNumber = h.CardNumber,
                    CardTypeId = h.CardTypeId,
                    CardTypeName = h.CardType != null ? h.CardType.Name : string.Empty,
                    IssuedBy = h.IssuedBy,
                    IssuedByName = h.IssueByUser != null ? h.IssueByUser.Name : string.Empty,
                    IssuedDate = h.IssuedDate,
                    ExpiredDate = h.ExpiredDate,
                    Status = h.Status,
                    Notes = h.Notes
                })
                .OrderByDescending(h => h.IssuedDate)
                .ToListAsync();
        }

        public async Task<HseCardDto?> GetByIdAsync(int id)
        {
            return await _context.HseCards
                .Include(h => h.User)
                .Include(h => h.CardType)
                .Include(h => h.IssueByUser)
                .Where(h => h.CardId == id)
                .Select(h => new HseCardDto
                {
                    CardId = h.CardId,
                    UserId = h.UserId,
                    UserName = h.User != null ? h.User.Name : string.Empty,
                    CardNumber = h.CardNumber,
                    CardTypeId = h.CardTypeId,
                    CardTypeName = h.CardType != null ? h.CardType.Name : string.Empty,
                    IssuedBy = h.IssuedBy,
                    IssuedByName = h.IssueByUser != null ? h.IssueByUser.Name : string.Empty,
                    IssuedDate = h.IssuedDate,
                    ExpiredDate = h.ExpiredDate,
                    Status = h.Status,
                    Notes = h.Notes
                })
                .FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<HseCardDto>> GetByUserIdAsync(int userId)
        {
            return await _context.HseCards
                .Include(h => h.User)
                .Include(h => h.CardType)
                .Include(h => h.IssueByUser)
                .Where(h => h.UserId == userId)
                .Select(h => new HseCardDto
                {
                    CardId = h.CardId,
                    UserId = h.UserId,
                    UserName = h.User != null ? h.User.Name : string.Empty,
                    CardNumber = h.CardNumber,
                    CardTypeId = h.CardTypeId,
                    CardTypeName = h.CardType != null ? h.CardType.Name : string.Empty,
                    IssuedBy = h.IssuedBy,
                    IssuedByName = h.IssueByUser != null ? h.IssueByUser.Name : string.Empty,
                    IssuedDate = h.IssuedDate,
                    ExpiredDate = h.ExpiredDate,
                    Status = h.Status,
                    Notes = h.Notes
                })
                .OrderByDescending(h => h.IssuedDate)
                .ToListAsync();
        }

        public async Task<IEnumerable<HseCardDto>> GetExpiringCardsAsync(int daysThreshold = 30)
        {
            var thresholdDate = DateTime.UtcNow.AddDays(daysThreshold);
            return await _context.HseCards
                .Include(h => h.User)
                .Include(h => h.CardType)
                .Include(h => h.IssueByUser)
                .Where(h => h.ExpiredDate <= thresholdDate && h.ExpiredDate > DateTime.UtcNow)
                .Select(h => new HseCardDto
                {
                    CardId = h.CardId,
                    UserId = h.UserId,
                    UserName = h.User != null ? h.User.Name : string.Empty,
                    CardNumber = h.CardNumber,
                    CardTypeId = h.CardTypeId,
                    CardTypeName = h.CardType != null ? h.CardType.Name : string.Empty,
                    IssuedBy = h.IssuedBy,
                    IssuedByName = h.IssueByUser != null ? h.IssueByUser.Name : string.Empty,
                    IssuedDate = h.IssuedDate,
                    ExpiredDate = h.ExpiredDate,
                    Status = h.Status,
                    Notes = h.Notes
                })
                .OrderBy(h => h.ExpiredDate)
                .ToListAsync();
        }

        public async Task<HseCardDto> CreateAsync(CreateHseCardDto dto)
        {
            var hseCard = new HseCard
            {
                UserId = dto.UserId,
                CardNumber = dto.CardNumber,
                CardTypeId = dto.CardTypeId,
                IssuedBy = dto.IssuedBy,
                IssuedDate = dto.IssuedDate,
                ExpiredDate = dto.ExpiredDate,
                Status = "Active",
                Notes = dto.Notes
            };

            _context.HseCards.Add(hseCard);
            await _context.SaveChangesAsync();

            var created = await _context.HseCards
                .Include(h => h.User)
                .Include(h => h.CardType)
                .Include(h => h.IssueByUser)
                .FirstOrDefaultAsync(h => h.CardId == hseCard.CardId);

            if (created == null) throw new Exception("Failed to create HSE card");

            return new HseCardDto
            {
                CardId = created.CardId,
                UserId = created.UserId,
                UserName = created.User != null ? created.User.Name : string.Empty,
                CardNumber = created.CardNumber,
                CardTypeId = created.CardTypeId,
                CardTypeName = created.CardType != null ? created.CardType.Name : string.Empty,
                IssuedBy = created.IssuedBy,
                IssuedByName = created.IssueByUser != null ? created.IssueByUser.Name : string.Empty,
                IssuedDate = created.IssuedDate,
                ExpiredDate = created.ExpiredDate,
                Status = created.Status,
                Notes = created.Notes
            };
        }

        public async Task<bool> UpdateAsync(int id, UpdateHseCardDto dto)
        {
            var existing = await _context.HseCards.FindAsync(id);
            if (existing == null) return false;

            if (dto.CardNumber != null) existing.CardNumber = dto.CardNumber;
            if (dto.CardTypeId.HasValue) existing.CardTypeId = dto.CardTypeId.Value;
            if (dto.ExpiredDate.HasValue) existing.ExpiredDate = dto.ExpiredDate.Value;
            if (dto.IssuedBy.HasValue) existing.IssuedBy = dto.IssuedBy.Value;
            if (dto.Status != null) existing.Status = dto.Status;
            if (dto.Notes != null) existing.Notes = dto.Notes;

            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var existing = await _context.HseCards.FindAsync(id);
            if (existing == null) return false;

            _context.HseCards.Remove(existing);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> UpdateStatusAsync(int id, string status)
        {
            var existing = await _context.HseCards.FindAsync(id);
            if (existing == null) return false;

            existing.Status = status;
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> RenewCardAsync(int id, DateTime newExpiryDate)
        {
            var existing = await _context.HseCards.FindAsync(id);
            if (existing == null) return false;

            existing.ExpiredDate = newExpiryDate;
            existing.Status = "Active";
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
