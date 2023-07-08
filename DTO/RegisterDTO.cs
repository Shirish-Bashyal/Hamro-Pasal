using System.ComponentModel.DataAnnotations;

namespace Hamro_Pasal.DTO
{
    public class RegisterDTO
    {
        [Required]

        public String Name { get; set; } = String.Empty;

        [Required]

        public string PhoneNumber { get; set; } = String.Empty;
        [Required]

        public String AboutMe { get; set; } = String.Empty;
        [Required]

        public String Address { get; set; } = String.Empty;

        [Required]
        [StringLength(50)]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [StringLength(50, MinimumLength = 6)]
        public string Password { get; set; }

        public string ConfirmPassword { get; set; }


       


    }
}
