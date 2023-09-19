using MagicVilla_Utility;
using MagicVilla_web.Models;
using MagicVilla_web.Models.DTO;
using MagicVilla_web.Services.Contacts;

namespace MagicVilla_web.Services
{
    public class AuthService : BaseService , IAuthService
    {
        private readonly IHttpClientFactory clientFactory;
        private string villaUrl;

        public AuthService(IHttpClientFactory _clientFactory,
            IConfiguration config) :base(_clientFactory)
        {
            clientFactory = _clientFactory;
            villaUrl = config.GetValue<string>("SurviceUrls:VillaAPI");
        }
        public Task<T> LoginAsync<T>(LoginRequestDTO obj)
        {
            return SendAsync<T>(new APIRequest()
            {
                ApiType = SD.ApiType.Post,
                Data = obj,
                Url = villaUrl + "/api/UsersAuth/login"
            });
        }

        public Task<T> RegisterAsync<T>(RegistrationRequestDTO obj)
        {
            return SendAsync<T>(new APIRequest()
            {
                ApiType = SD.ApiType.Post,
                Data = obj,
                Url = villaUrl + "/api/UsersAuth/register"
            });
        }
    }
}
