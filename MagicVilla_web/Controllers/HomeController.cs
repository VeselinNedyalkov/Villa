using AutoMapper;
using MagicVilla_Utility;
using MagicVilla_web.Models.DTO;
using MagicVilla_web.Services.Contacts;
using MagicVilMagicVilla_web.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace MagicVilla_web.Controllers
{
    public class HomeController : Controller
    {
        private readonly IVillaSurvice villaSurvice;
        private readonly IMapper mapper;

        public HomeController(IVillaSurvice villaSurvice, IMapper mapper)
        {
            this.villaSurvice = villaSurvice;
            this.mapper = mapper;
        }

        public async Task<IActionResult> Index()
        {
            List<VillaDTO> list = new List<VillaDTO>();

            var response = await villaSurvice.GetAllAsync<APIResponse>(HttpContext.Session.GetString(SD.SessionToken));

            if (response != null && response.IsUsccess)
            {
                list = JsonConvert.DeserializeObject<List<VillaDTO>>(Convert.ToString(response.Result));
            }

            return View(list);
        }
    }
}