using System.ComponentModel.DataAnnotations;

namespace NatureBreaks.Models
{
    public class User
    {
        public int Id { get; set; }

        [Required]
        [StringLength(255, MinimumLength = 1)]
        public string FirebaseUserId { get; set; }

        [Required]
        [StringLength(25, MinimumLength = 1)]
        public string FirstName { get; set; }
        
        [Required]
        [StringLength(255, MinimumLength = 1)]
        public string Email { get; set; }

        [Required]
        [StringLength(255, MinimumLength = 1)]
        public string ProfileImage { get; set; }


        [Required]
        public bool IsAdmin { get; set; }

    }
}
