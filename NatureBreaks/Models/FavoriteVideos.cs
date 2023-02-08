using System.ComponentModel.DataAnnotations;

namespace NatureBreaks.Models
{
    public class FavoriteVideos
    {
        public int Id { get; set; }
        [Required]
        public int UserId { get; set; }

        [Required]
        public int VideoId { get; set; }

    }
}
