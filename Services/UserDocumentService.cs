using EIMS.Data;
using EIMS.Dtos.UserDocumentDto.UserDocumentDto;
using EIMS.Dtos.UserDocumentDtos;
using EIMS.Models;
using Microsoft.EntityFrameworkCore;


namespace EIMS.Services
{
    public class UserDocumentService
    {
        private readonly EimsDbContext _context;
        private readonly IFileStorageService _fileStorageService;

        public UserDocumentService(EimsDbContext context, IFileStorageService fileStorageService)
        {
            _context = context;
            _fileStorageService = fileStorageService;
        }

        public async Task<IEnumerable<UserDocumentDto>> GetAllAsync()
        {
            var documents = await _context.UseDocuments
                .Include(ud => ud.User)
                .Include(ud => ud.DocumentRequirement)
                .Select(ud => new UserDocumentDto
                {
                    UserDocumentId = ud.UserDocumentId,
                    UserId = ud.UserId,
                    UserName = ud.User != null ? ud.User.Name : string.Empty,
                    DocumentRequirementId = ud.DocumentRequirementId,
                    DocumentName = ud.DocumentRequirement != null ? ud.DocumentRequirement.Name : string.Empty,
                    UploadDate = ud.UploadDate,
                    FileURL = !string.IsNullOrEmpty(ud.FileURL) ? _fileStorageService.GetFileUrl(ud.FileURL) : ud.FileURL
                })
                .ToListAsync();

            return documents;
        }

        public async Task<UserDocumentDto?> GetByIdAsync(int id)
        {
            var document = await _context.UseDocuments
                .Include(ud => ud.User)
                .Include(ud => ud.DocumentRequirement)
                .Where(ud => ud.UserDocumentId == id)
                .Select(ud => new UserDocumentDto
                {
                    UserDocumentId = ud.UserDocumentId,
                    UserId = ud.UserId,
                    UserName = ud.User != null ? ud.User.Name : string.Empty,
                    DocumentRequirementId = ud.DocumentRequirementId,
                    DocumentName = ud.DocumentRequirement != null ? ud.DocumentRequirement.Name : string.Empty,
                    UploadDate = ud.UploadDate,
                    FileURL = !string.IsNullOrEmpty(ud.FileURL) ? _fileStorageService.GetFileUrl(ud.FileURL) : ud.FileURL
                })
                .FirstOrDefaultAsync();

            return document;
        }

        public async Task<IEnumerable<UserDocumentDto>> GetByUserIdAsync(int userId)
        {
            var documents = await _context.UseDocuments
                .Include(ud => ud.User)
                .Include(ud => ud.DocumentRequirement)
                .Where(ud => ud.UserId == userId)
                .Select(ud => new UserDocumentDto
                {
                    UserDocumentId = ud.UserDocumentId,
                    UserId = ud.UserId,
                    UserName = ud.User != null ? ud.User.Name : string.Empty,
                    DocumentRequirementId = ud.DocumentRequirementId,
                    DocumentName = ud.DocumentRequirement != null ? ud.DocumentRequirement.Name : string.Empty,
                    UploadDate = ud.UploadDate,
                    FileURL = !string.IsNullOrEmpty(ud.FileURL) ? _fileStorageService.GetFileUrl(ud.FileURL) : ud.FileURL
                })
                .ToListAsync();

            return documents;
        }

        public async Task<UserDocumentDto> CreateAsync(CreateUserDocumentDto dto, IFormFile? file = null)
        {
            string fileUrl = dto.FileURL ?? string.Empty;

            // Jika ada file yang diupload, simpan file tersebut
            if (file != null)
            {
                fileUrl = await _fileStorageService.SaveFileAsync(file, "user-documents");
            }

            var userDocument = new UserDocument
            {
                UserId = dto.UserId,
                DocumentRequirementId = dto.DocumentRequirementId,
                FileURL = fileUrl,
                UploadDate = DateTime.UtcNow
            };

            _context.UseDocuments.Add(userDocument);
            await _context.SaveChangesAsync();

            // Get the created entity with includes
            var created = await _context.UseDocuments
                .Include(ud => ud.User)
                .Include(ud => ud.DocumentRequirement)
                .FirstOrDefaultAsync(ud => ud.UserDocumentId == userDocument.UserDocumentId);

            if (created == null)
            {
                throw new Exception("Failed to create user document");
            }

            return new UserDocumentDto
            {
                UserDocumentId = created.UserDocumentId,
                UserId = created.UserId,
                UserName = created.User?.Name ?? string.Empty,
                DocumentRequirementId = created.DocumentRequirementId,
                DocumentName = created.DocumentRequirement?.Name ?? string.Empty,
                UploadDate = created.UploadDate,
                FileURL = !string.IsNullOrEmpty(created.FileURL) ?
                         _fileStorageService.GetFileUrl(created.FileURL) : created.FileURL
            };
        }

        public async Task<bool> UpdateAsync(int id, UpdateUserDocumentDto dto, IFormFile? file = null)
        {
            var existing = await _context.UseDocuments.FindAsync(id);
            if (existing == null) return false;

            // Jika ada file baru, simpan dan hapus file lama
            if (file != null)
            {
                // Hapus file lama jika ada
                if (!string.IsNullOrEmpty(existing.FileURL))
                {
                    await _fileStorageService.DeleteFileAsync(existing.FileURL);
                }

                // Simpan file baru
                existing.FileURL = await _fileStorageService.SaveFileAsync(file, "user-documents");
            }
            else if (!string.IsNullOrEmpty(dto.FileURL))
            {
                existing.FileURL = dto.FileURL;
            }

            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var existing = await _context.UseDocuments.FindAsync(id);
            if (existing == null) return false;

            // Hapus file fisik jika ada
            if (!string.IsNullOrEmpty(existing.FileURL))
            {
                await _fileStorageService.DeleteFileAsync(existing.FileURL);
            }

            _context.UseDocuments.Remove(existing);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}