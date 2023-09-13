using MagicVilla_web.Models.DTO;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace MagicVilla_web.Models.VM
{
    public class VillaNUmberCreateVM
    {
        public VillaCreateNumberDTO VillaNumber { get; set; } = new VillaCreateNumberDTO();

        [ValidateNever]
        public IEnumerable<SelectListItem> VillaList { get; set; }
    }
}
