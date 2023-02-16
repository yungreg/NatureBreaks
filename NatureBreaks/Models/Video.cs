using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace NatureBreaks.Models
{
    public class Video
    {
        public int Id { get; set; }

        [Required]
        [StringLength(7)]
        public string Season { get; set; }

        [Required]
        public int NatureTypeId { get; set; }
        [Required]
        public int UserId { get; set; }

        // this is to be a list of IDs that are the users that have favorited thsi video.
        public List<int> UsersThatFavorited { get; set; }

        [Required]
        [StringLength(255, MinimumLength = 1)]
        public string ClosestMajorCity { get; set; }

        [Required]
        [StringLength(255, MinimumLength = 1)]
        public string VideoName { get; set; }

        [Required]
        [StringLength(255, MinimumLength = 4)]
        public string VideoUrl { get; set; }
    }
}
