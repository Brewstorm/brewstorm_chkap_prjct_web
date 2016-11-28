
using System.ComponentModel.DataAnnotations;
using CheckAppCore.Enumerators;

namespace CheckAppCore.Models
{
    public class RegisterModel
    {
        [Required]
        public string Name { get; set; }
        [Required]
        public string Email { get; set; }
        [Required]
        public string Birthday { get; set; }
        [Required]
        public UserType UserType { get; set; }
        public string PhotoUrl { get; set; }
        public string FacebookID { get; set; }
        [Required]
        public GenderEnum Gender { get; set; }
        public int AppointmentType { get; set; }
        [Required]
        public string Password { get; set; }
    }
}
