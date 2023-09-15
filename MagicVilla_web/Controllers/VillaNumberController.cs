using AutoMapper;
using MagicVilla_web.Models.DTO;
using MagicVilla_web.Models.VM;
using MagicVilla_web.Services.Contacts;
using MagicVilMagicVilla_web.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;

namespace MagicVilla_web.Controllers
{
    public class VillaNumberController : Controller
    {
        private readonly IVillaNumberSurvice villaNumberSurvice;
        private readonly IVillaSurvice villaSurvice;
        private readonly IMapper mapper;

        public VillaNumberController(IVillaNumberSurvice _villaNumberSurvice,
            IMapper _mapper,
            IVillaSurvice _villaSurvice)
        {
            villaNumberSurvice = _villaNumberSurvice;
            mapper = _mapper;
            villaSurvice = _villaSurvice;
        }

        public async Task<IActionResult> IndexVillaNumber()
        {
            List<VillaNumberDTO> list = new List<VillaNumberDTO>();

            var response = await villaNumberSurvice.GetAllAsync<APIResponse>();

            if (response != null && response.IsUsccess)
            {
                list = JsonConvert.DeserializeObject<List<VillaNumberDTO>>(Convert.ToString(response.Result));
            }

            return View(list);
        }

        public async Task<IActionResult> CreateVillaNumber()
        {
            VillaNUmberCreateVM villaNUmberCreateVM = new VillaNUmberCreateVM();
            var response = await villaSurvice.GetAllAsync<APIResponse>();

            if (response != null && response.IsUsccess)
            {
                villaNUmberCreateVM.VillaList = JsonConvert.DeserializeObject<List<VillaDTO>>
                    (Convert.ToString(response.Result)).Select(i => new SelectListItem 
                    { 
                        Text = i.Name,
                        Value = i.Id.ToString()
                    });

            return View(villaNUmberCreateVM);
            }

            return NotFound(villaNUmberCreateVM);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateVillaNumber(VillaNUmberCreateVM model)
        {
            if (ModelState.IsValid)
            {
                var response = await villaNumberSurvice.CreateAsync<APIResponse>(model.VillaNumber);

                if (response != null && response.IsUsccess)
                {
                    return RedirectToAction(nameof(IndexVillaNumber));
                }
            }

            var resp = await villaSurvice.GetAllAsync<APIResponse>();

            if (resp != null && resp.IsUsccess)
            {
                model.VillaList = JsonConvert.DeserializeObject<List<VillaDTO>>
                    (Convert.ToString(resp.Result)).Select(i => new SelectListItem
                    {
                        Text = i.Name,
                        Value = i.Id.ToString()
                    });
            }
            return View(model);
        }

        //public async Task<IActionResult> UpdateVillaNumber(int villaId)
        //{
        //    var response = await villaSurvice.GetAsync<APIResponse>(villaId);

        //    if (response != null && response.IsUsccess)
        //    {
        //        VillaDTO model = JsonConvert.DeserializeObject<VillaNumberDTO>(Convert.ToString(response.Result));
        //        return View(mapper.Map<VillaUpdateNumberDTO>(model));
        //    }

        //    return NotFound();
        //}

        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> UpdateVillaNumber(VillaUpdateNumberDTO model)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        var response = await villaSurvice.UpdateAsync<APIResponse>(model);

        //        if (response != null && response.IsUsccess)
        //        {
        //            return RedirectToAction(nameof(IndexVilla));
        //        }
        //    }

        //    return View(model);
        //}

        //public async Task<IActionResult> DeleteVillaNumber(int villaId)
        //{
        //    var response = await villaSurvice.GetAsync<APIResponse>(villaId);

        //    if (response != null && response.IsUsccess)
        //    {
        //        VillaDTO model = JsonConvert.DeserializeObject<VillaNumberDTO>(Convert.ToString(response.Result));
        //        return View(model);
        //    }

        //    return NotFound();
        //}

        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> DeleteVillaNumber(VillaNumberDTO model)
        //{

        //    var response = await villaSurvice.DeleteAsync<APIResponse>(model.Id);

        //    if (response != null && response.IsUsccess)
        //    {
        //        return RedirectToAction(nameof(IndexVilla));
        //    }


        //    return View(model);
        //}
    }
}
