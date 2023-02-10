using System.ComponentModel.DataAnnotations;

namespace NatureBreaks.Models
{
    public class FavoriteVideo
    {
        public int Id { get; set; }
        [Required]
        public int UserId { get; set; }

        [Required]
        public int VideoId { get; set; }

    }
}
