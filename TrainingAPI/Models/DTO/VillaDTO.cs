﻿using System.ComponentModel.DataAnnotations;

namespace TrainingAPI.Models.DTO
{
    public class VillaDTO
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(30)]
        public string Name { get; set; } = null!;
        public string Detaisl { get; set; } = null!;
        [Required]
        public double Rate { get; set; }
        public int Occupancy { get; set; }
        public int Sqft { get; set; }
        public string ImageUrl { get; set; } = null!;
        public string Amenity { get; set; } = null!;
    }
}
