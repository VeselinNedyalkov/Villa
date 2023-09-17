﻿using MagicVilla_web.Models.DTO;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace MagicVilla_web.Models.VM
{
    public class VillaNUmberDeleteVM
    {
        public VillaNumberDTO VillaNumber { get; set; } = new VillaNumberDTO();

        [ValidateNever]
        public IEnumerable<SelectListItem> VillaList { get; set; }
    }
}
