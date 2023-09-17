using MagicVilla_web.Models.DTO;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace MagicVilla_web.Models.VM
{
    public class VillaNUmberUpdateVM
    {
        public VillaUpdateNumberDTO VillaNumber { get; set; } = new VillaUpdateNumberDTO();

        [ValidateNever]
        public IEnumerable<SelectListItem> VillaList { get; set; }
    }
}
