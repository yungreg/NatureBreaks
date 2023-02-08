using System.ComponentModel.DataAnnotations;


namespace NatureBreaks.Models
{
    public class NatureType
    {
        public int Id { get; set; }
        [Required]
        [StringLength(50, MinimumLength = 1)]
        public string NatureTypeName { get; set; }
    }
}
