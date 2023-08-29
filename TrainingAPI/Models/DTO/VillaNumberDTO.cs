﻿using System.ComponentModel.DataAnnotations;

namespace TrainingAPI.Models.DTO
{
    public class VillaNumberDTO
    {
        [Required]
        public int VillaNo { get; set; }
        [Required]
        public int VillaID { get; set; }
        public string SpecialDeails { get; set; }
    }
}
