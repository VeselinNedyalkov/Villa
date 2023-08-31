using System.ComponentModel.DataAnnotations;

namespace TrainingAPI.Models.DTO
{
    public class VillaCreateDTO
    {

        [Required]
        [MaxLength(30)]
        public string Name { get; set; } = null!;
        public string? Details { get; set; } = null!;
        [Required]
        public double Rate { get; set; }
        public int Occupancy { get; set; }
        public int Sqft { get; set; }
        public string? ImageUrl { get; set; } = null!;
        public string? Amenity { get; set; } = null!;
    }
}
