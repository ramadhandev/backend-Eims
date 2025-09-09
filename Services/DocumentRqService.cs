using EIMS.Data;
using EIMS.Dtos.DocumentRqDtos;
using EIMS.Models;
using Microsoft.EntityFrameworkCore;

namespace EIMS.Services
{
    public class DocumentRqService
    {
        private readonly EimsDbContext _context;

        public DocumentRqService(EimsDbContext context)
        {
            _context = context;
        }

        public async Task<List<DocumentRequirementDto>> GetAllAsync()
        {
            return await _context.DocumentRequirements
                .Select(d => new DocumentRequirementDto
                {
                    DocumentRequirementId = d.DocumentRequirementId,
                    Name = d.Name,
                    Description = d.Description
                })
                .ToListAsync();
        }

        public async Task<DocumentRequirementDto?> GetByIdAsync(int id)
        {
            return await _context.DocumentRequirements
                .Where(d => d.DocumentRequirementId == id)
                .Select(d => new DocumentRequirementDto
                {
                    DocumentRequirementId = d.DocumentRequirementId,
                    Name = d.Name,
                    Description = d.Description
                })
                .FirstOrDefaultAsync();
        }

        public async Task<DocumentRequirement> CreateAsync(CreateDocumentRequirementDto dto)
        {
            var doc = new DocumentRequirement
            {
                Name = dto.Name,
                Description = dto.Description
            };

            _context.DocumentRequirements.Add(doc);
            await _context.SaveChangesAsync();
            return doc;
        }

        public async Task<bool> UpdateAsync(int id, UpdateDocumentRequirementDto dto)
        {
            var doc = await _context.DocumentRequirements.FindAsync(id);
            if (doc == null) return false;

            doc.Name = dto.Name;
            doc.Description = dto.Description;
            

            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var doc = await _context.DocumentRequirements.FindAsync(id);
            if (doc == null) return false;

            _context.DocumentRequirements.Remove(doc);
            await _context.SaveChangesAsync();
            return true;
        }

    }
}
