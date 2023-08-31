using MagicVilla_Utility;
using MagicVilla_web.Models;
using MagicVilla_web.Models.DTO;
using MagicVilla_web.Services.Contacts;

namespace MagicVilla_web.Services
{
    public class VillaSurvice : BaseService ,  IVillaSurvice
    {
        private readonly IHttpClientFactory clientFactory;
        private string villaUrl;

        public VillaSurvice(IHttpClientFactory _clientFactory,
            IConfiguration config) : base(_clientFactory)
        {
            clientFactory  = _clientFactory;
            //take the URL from appsettings.json
            villaUrl = config.GetValue<string>("SurviceUrls:VillaAPI");
        }

        public Task<T> CreateAsync<T>(VillaCreateDTO dto)
        {
            return SendAsync<T>(new APIRequest() 
            { 
                ApiType = SD.ApiType.Post,
                Data = dto,
                Url = villaUrl + "/api/VillaAPI"
            });
        }

        public Task<T> DeleteAsync<T>(int id)
        {
            return SendAsync<T>(new APIRequest()
            {
                ApiType = SD.ApiType.Delete,
                Url = villaUrl + "/api/VillaAPI/" + id
            });
        }

        public Task<T> GetAllAsync<T>()
        {
            return SendAsync<T>(new APIRequest()
            {
                ApiType = SD.ApiType.Get,
                Url = villaUrl + "/api/VillaAPI"
            });
        }

        public Task<T> GetAsync<T>(int id)
        {
            return SendAsync<T>(new APIRequest()
            {
                ApiType = SD.ApiType.Get,
                Url = villaUrl + "/api/VillaAPI/" + id
            });
        }

        public Task<T> UpdateAsync<T>(VillaUpdateDTO dto)
        {
            return SendAsync<T>(new APIRequest()
            {
                ApiType = SD.ApiType.Put,
                Data = dto,
                Url = villaUrl + "/api/VillaAPI/" + dto.Id
            });
        }
    }
}
