using System.ComponentModel.DataAnnotations;

namespace TrainingAPI.Models.DTO
{
    public class VillaUpdateDTO
    {
        [Required]
        public int Id { get; set; }

        [Required]
        [MaxLength(30)]
        public string Name { get; set; } = null!;
        public string Details { get; set; } = null!;
        [Required]
        public double Rate { get; set; }
        [Required]
        public int Occupancy { get; set; }
        [Required]
        public int Sqft { get; set; }
        public string ImageUrl { get; set; } = null!;
        public string Amenity { get; set; } = null!;
    }
}
