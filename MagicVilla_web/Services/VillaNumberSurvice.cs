using MagicVilla_Utility;
using MagicVilla_web.Models;
using MagicVilla_web.Models.DTO;
using MagicVilla_web.Services.Contacts;

namespace MagicVilla_web.Services
{
    public class VillaNumberSurvice : BaseService ,  IVillaNumberSurvice
    {
        private readonly IHttpClientFactory clientFactory;
        private string villaUrl;

        public VillaNumberSurvice(IHttpClientFactory _clientFactory,
            IConfiguration config) : base(_clientFactory)
        {
            clientFactory  = _clientFactory;
            //take the URL from appsettings.json
            villaUrl = config.GetValue<string>("SurviceUrls:VillaAPI");
        }

        public Task<T> CreateAsync<T>(VillaCreateNumberDTO dto , string token)
        {
            return SendAsync<T>(new APIRequest() 
            { 
                ApiType = SD.ApiType.Post,
                Data = dto,
                Url = villaUrl + "/api/VillaNumberAPI",
                Token = token
            });
        }

        public Task<T> DeleteAsync<T>(int id , string token)
        {
            return SendAsync<T>(new APIRequest()
            {
                ApiType = SD.ApiType.Delete,
                Url = villaUrl + "/api/VillaNumberAPI/" + id,
                Token = token
            });
        }

        public Task<T> GetAllAsync<T>(string token)
        {
            return SendAsync<T>(new APIRequest()
            {
                ApiType = SD.ApiType.Get,
                Url = villaUrl + "/api/VillaNumberAPI",
                Token = token
            });
        }

        public Task<T> GetAsync<T>(int id , string token)
        {
            return SendAsync<T>(new APIRequest()
            {
                ApiType = SD.ApiType.Get,
                Url = villaUrl + "/api/VillaNumberAPI/" + id
            });
        }

        public Task<T> UpdateAsync<T>(VillaUpdateNumberDTO dto, string token)
        {
            return SendAsync<T>(new APIRequest()
            {
                ApiType = SD.ApiType.Put,
                Data = dto,
                Url = villaUrl + "/api/VillaNumberAPI/" + dto.VillaNo
            });
        }
    }
}
