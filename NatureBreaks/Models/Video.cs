using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace NatureBreaks.Models
{
    public class Video
    {
        public int Id { get; set; }

        [Required]
        [StringLength(7, MinimumLength = 4)]
        public string Season { get; set; }

        [Required]
        public int NatureTypeId{ get; set; }
        [Required]
        public int UserId { get; set; }

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
