using EIMS.Constants;
using EIMS.Data;
using EIMS.Dtos.UserDto.UserDto;
using EIMS.Dtos.UserDtos;
using EIMS.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace EIMS.Services
{
    public class UserService
    {
        private readonly EimsDbContext _context;
        private readonly IPasswordHasher<User> _passwordHasher;

        public UserService(EimsDbContext context, IPasswordHasher<User> passwordHasher)
        {
            _context = context;
            _passwordHasher = passwordHasher;
        }

        public async Task<IEnumerable<UserDto>> GetAllAsync()
        {
            return await _context.Users
                .Select(u => new UserDto
                {
                    UserId = u.UserId,
                    Name = u.Name,
                    Email = u.Email,
                    Role = u.Role,
                    Department = u.Department,
                    CreatedAt = u.CreatedAt,
                    Status = u.Status
                })
                .ToListAsync();
        }

        public async Task<UserDto?> GetByIdAsync(int id)
        {
            return await _context.Users
                .Where(u => u.UserId == id)
                .Select(u => new UserDto
                {
                    UserId = u.UserId,
                    Name = u.Name,
                    Email = u.Email,
                    Role = u.Role,
                    Department = u.Department,
                    CreatedAt = u.CreatedAt,
                    Status = u.Status
                })
                .FirstOrDefaultAsync();
        }

        public async Task<UserDto> CreateAsync(CreateUserDto dto)
        {
            // Check if email exists
            if (await _context.Users.AnyAsync(u => u.Email == dto.Email))
            {
                throw new ArgumentException("Email already exists");
            }

            var user = new User
            {
                Name = dto.Name,
                Email = dto.Email,
                Role = dto.Role,
                Department = dto.Department,
                CreatedAt = DateTime.UtcNow,
                Status = "Active"
            };

            // Hash password
            user.PasswordHash = _passwordHasher.HashPassword(user, dto.Password);

            _context.Users.Add(user);
            await _context.SaveChangesAsync();

            return new UserDto
            {
                UserId = user.UserId,
                Name = user.Name,
                Email = user.Email,
                Role = user.Role,
                Department = user.Department,
                CreatedAt = user.CreatedAt,
                Status = user.Status
            };
        }

        public async Task<bool> UpdateAsync(int id, UpdateUserDto dto)
        {
            var existing = await _context.Users.FindAsync(id);
            if (existing == null) return false;

            if (!string.IsNullOrEmpty(dto.Name))
                existing.Name = dto.Name;

            if (!string.IsNullOrEmpty(dto.Email))
                existing.Email = dto.Email;

            if (!string.IsNullOrEmpty(dto.Role))
                existing.Role = dto.Role;

            if (!string.IsNullOrEmpty(dto.Department))
                existing.Department = dto.Department;

            if (!string.IsNullOrEmpty(dto.Password))
                existing.PasswordHash = _passwordHasher.HashPassword(existing, dto.Password);

            if (!string.IsNullOrEmpty(dto.Status))
                existing.Status = dto.Status;

            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var existing = await _context.Users.FindAsync(id);
            if (existing == null) return false;

            _context.Users.Remove(existing);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
