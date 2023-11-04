using System.ComponentModel.DataAnnotations;

namespace Clothes.DTO
{
    public class RegisterRequestDto
    {
        public string Username { get; set; }

        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [DataType(DataType.Password)]
        public string Password { get; set; }

        public string[] Roles { get; set; }

    }
}
