using AutoMapper;
using MagicVilla_web.Models.DTO;
using MagicVilla_web.Services.Contacts;
using MagicVilMagicVilla_web.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Reflection;
using System.Reflection.Metadata.Ecma335;

namespace MagicVilla_web.Controllers
{
    public class VillaController : Controller
    {
        private readonly IVillaSurvice villaSurvice;
        private readonly IMapper mapper;

        public VillaController(IVillaSurvice villaSurvice, IMapper mapper)
        {
            this.villaSurvice = villaSurvice;
            this.mapper = mapper;
        }

        public async Task<IActionResult> IndexVilla()
        {
            List<VillaDTO> list = new List<VillaDTO>();

            var response = await villaSurvice.GetAllAsync<APIResponse>();

            if (response != null && response.IsUsccess)
            {
                list = JsonConvert.DeserializeObject<List<VillaDTO>>(Convert.ToString(response.Result));
            }

            return View(list);
        }

        
        public async Task<IActionResult> CreateVilla()
        {

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateVilla(VillaCreateDTO model)
        {
            if(ModelState.IsValid)
            {
                var response = await villaSurvice.CreateAsync<APIResponse>(model);

                if (response != null && response.IsUsccess)
                {
                    return RedirectToAction(nameof(IndexVilla));
                }
            }

            return View(model);
        }

        public async Task<IActionResult> UpdateVilla(int villaId)
        {
            var response = await villaSurvice.GetAsync<APIResponse>(villaId);

            if (response != null && response.IsUsccess)
            {
                VillaDTO model = JsonConvert.DeserializeObject<VillaDTO>(Convert.ToString(response.Result));
                return View(mapper.Map<VillaUpdateDTO>(model));
            }

            return NotFound();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> UpdateVilla(VillaUpdateDTO model)
        {
            if (ModelState.IsValid)
            {
                var response = await villaSurvice.UpdateAsync<APIResponse>(model);

                if (response != null && response.IsUsccess)
                {
                    return RedirectToAction(nameof(IndexVilla));
                }
            }

            return View(model);
        }
    }
}
