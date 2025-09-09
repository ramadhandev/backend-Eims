using EIMS.Attributes;
using System.ComponentModel.DataAnnotations;

namespace EIMS.Dtos.UserDto.UserDto
{
    public class UpdateUserDto
    {
        [MaxLength(100)]
        public string? Name { get; set; }

        [EmailAddress]
        [MaxLength(100)]
        public string? Email { get; set; }

        [ValidRole]
        public string? Role { get; set; }

        [MaxLength(50)]
        public string? Department { get; set; }

        [MinLength(6)]
        public string? Password { get; set; }

        [MaxLength(10)]
        public string? Status { get; set; }
    }
}
