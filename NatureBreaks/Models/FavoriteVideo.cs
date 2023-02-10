using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

namespace NatureBreaks.Models
{
    public class FavoriteVideo
    {
        public int Id { get; set; }
        
        [Required]
        public int UserId { get; set; }
        //might not need this.
        //public List<User> UsersThatFavorited { get; set; }

        [Required]
        public int VideoId { get; set; }
        public Video Video { get; set; }
    }
}
