using System.ComponentModel.DataAnnotations;

namespace Experimentator.Models
{
    public class CreateExperimentatorUserModel
    {
        [Required]
        public string Name { get; set; }

        [Required]
        public string Email { get; set; }

        [Required]
        public string Password { get; set; }
    }
}
