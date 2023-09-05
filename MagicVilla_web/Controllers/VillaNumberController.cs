using AutoMapper;
using MagicVilla_web.Models.DTO;
using MagicVilla_web.Services;
using MagicVilla_web.Services.Contacts;
using MagicVilMagicVilla_web.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace MagicVilla_web.Controllers
{
    public class VillaNumberController : Controller
    {
        private readonly IVillaNumberSurvice villaNumberSurvice;
        private readonly IMapper mapper;

        public VillaNumberController(IVillaNumberSurvice _villaNumberSurvice, IMapper _mapper)
        {
            villaNumberSurvice = _villaNumberSurvice;
            mapper = _mapper;
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
    }
}
