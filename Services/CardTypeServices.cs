using EIMS.Data;
using EIMS.Dtos.CardTypeDto;
using EIMS.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EIMS.Services
{
    public class CardTypeServices
    {
        private readonly EimsDbContext _context;
        public CardTypeServices(EimsDbContext context)
        {
            _context = context;
        }

       public async Task<IEnumerable<CardTypeDto>> GetAllAsync()
        {
            return await _context.CardTypes
                .Select(e => new CardTypeDto
                {
                    CardTypeId = e.CardTypeId,
                    Name = e.Name,
                    Description = e.Description
                }).ToListAsync();
        }

        public async Task<CardTypeDto?> GetByIdAsync(int id)
        {
            return await _context.CardTypes
                .Where(ct => ct.CardTypeId == id)
                .Select(ct => new CardTypeDto
                {
                    CardTypeId = ct.CardTypeId,
                    Name = ct.Name,
                    Description = ct.Description
                }).FirstOrDefaultAsync();
        }

        public async Task<CardTypeDto> CreateAsync(CreateCardTypeDto dto)
        {
            var cardType = new CardType
            {
                Name = dto.Name,
                Description = dto.Description
            };

            _context.CardTypes.Add(cardType);
            await _context.SaveChangesAsync();

            return new CardTypeDto
            {
                CardTypeId = cardType.CardTypeId,
                Name = cardType.Name,
                Description = cardType.Description
            };
        }

        public async Task<bool> UpdateAsync(int id, CreateCardTypeDto updateCard)
        {
            var existing = await _context.CardTypes.FindAsync(id);
            if (existing == null) return false;

            existing.Name = updateCard.Name;
            existing.Description = updateCard.Description;

            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var existing = await _context.CardTypes.FindAsync(id);
            if (existing == null) return false;
            _context.CardTypes.Remove(existing);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
