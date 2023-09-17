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
            }


            return View(villaNUmberCreateVM);
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
                else
                {
                    if(response.ErrorMessages.Count > 0)
                    {
                        ModelState.AddModelError("ErrorMessages", response.ErrorMessages.FirstOrDefault());
                    }
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

        public async Task<IActionResult> UpdateVillaNumber(int villaNo)
        {
            VillaNUmberUpdateVM villaNUmberVM = new VillaNUmberUpdateVM();
            var response = await villaNumberSurvice.GetAsync<APIResponse>(villaNo);

            if (response != null && response.IsUsccess)
            {
                VillaNumberDTO model = JsonConvert.DeserializeObject<VillaNumberDTO>(Convert.ToString(response.Result));
                villaNUmberVM.VillaNumber = mapper.Map<VillaUpdateNumberDTO>(model);
            }

            response = await villaSurvice.GetAllAsync<APIResponse>();

            if (response != null && response.IsUsccess)
            {
                villaNUmberVM.VillaList = JsonConvert.DeserializeObject<List<VillaDTO>>
                    (Convert.ToString(response.Result)).Select(i => new SelectListItem
                    {
                        Text = i.Name,
                        Value = i.Id.ToString()
                    });
                return View(villaNUmberVM);
            }
            return NotFound();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> UpdateVillaNumber(VillaNUmberUpdateVM model)
        {
            if (ModelState.IsValid)
            {
                var response = await villaNumberSurvice.UpdateAsync<APIResponse>(model.VillaNumber);

                if (response != null && response.IsUsccess)
                {
                    return RedirectToAction(nameof(IndexVillaNumber));
                }
                else
                {
                    if (response.ErrorMessages.Count > 0)
                    {
                        ModelState.AddModelError("ErrorMessages", response.ErrorMessages.FirstOrDefault());
                    }
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

        public async Task<IActionResult> DeleteVillaNumber(int villaNo)
        {
            VillaNUmberDeleteVM villaNUmberVM = new VillaNUmberDeleteVM();
            var response = await villaNumberSurvice.GetAsync<APIResponse>(villaNo);

            if (response != null && response.IsUsccess)
            {
                VillaNumberDTO model = JsonConvert.DeserializeObject<VillaNumberDTO>(Convert.ToString(response.Result));
                villaNUmberVM.VillaNumber = model;
            }

            response = await villaSurvice.GetAllAsync<APIResponse>();

            if (response != null && response.IsUsccess)
            {
                villaNUmberVM.VillaList = JsonConvert.DeserializeObject<List<VillaDTO>>
                    (Convert.ToString(response.Result)).Select(i => new SelectListItem
                    {
                        Text = i.Name,
                        Value = i.Id.ToString()
                    });
                return View(villaNUmberVM);
            }
            return NotFound();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteVillaNumber(VillaNUmberDeleteVM model)
        {

            var response = await villaNumberSurvice.DeleteAsync<APIResponse>(model.VillaNumber.VillaNo);

            if (response != null && response.IsUsccess)
            {
                return RedirectToAction(nameof(IndexVillaNumber));
            }


            return View(model);
        }
    }
}
