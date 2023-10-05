using MagicVilla_Utility;
using MagicVilla_web.Models;
using MagicVilla_web.Services.Contacts;
using MagicVilMagicVilla_web.Models;
using Newtonsoft.Json;
using System.Net;
using System.Net.Http.Headers;
using System.Text;

namespace MagicVilla_web.Services
{
    public class BaseService : IBaseService
    {
        public APIResponse responseModel { get; set; }
        public IHttpClientFactory httpClient { get; set; }

        public BaseService(IHttpClientFactory _httpClient)
        {
            responseModel = new APIResponse();
            httpClient = _httpClient;

        }

        public async Task<T> SendAsync<T>(APIRequest apiRequest)
        {
            try
            {
                var client = httpClient.CreateClient("MagicAPI");
                HttpRequestMessage message = new HttpRequestMessage();
                message.Headers.Add("Accept", "application/json");
                message.RequestUri = new Uri(apiRequest.Url);

                if (apiRequest.Data != null)
                {
                    message.Content = new StringContent(JsonConvert.SerializeObject(apiRequest.Data),
                        Encoding.UTF8, "application/json");
                }

                switch (apiRequest.ApiType)
                {
                    case SD.ApiType.Post:
                        message.Method = HttpMethod.Post;
                        break;

                    case SD.ApiType.Put:
                        message.Method = HttpMethod.Put;
                        break;

                    case SD.ApiType.Delete:
                        message.Method = HttpMethod.Delete;
                        break;

                    default:
                        message.Method = HttpMethod.Get;
                        break;
                }

                HttpResponseMessage apiResponse = null;

                if (!string.IsNullOrEmpty(apiRequest.Token))
                {
                    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer" , apiRequest.Token);
                }

                apiResponse = await client.SendAsync(message);
                var apiContet = await apiResponse.Content.ReadAsStringAsync();

                try
                {
                    APIResponse ApiResponse = JsonConvert.DeserializeObject<APIResponse>(apiContet);
                    if (apiResponse.StatusCode != HttpStatusCode.BadRequest || apiResponse.StatusCode != HttpStatusCode.NotFound)
                    {
                        ApiResponse.StatusCode = HttpStatusCode.OK;
                        ApiResponse.IsUsccess = true;
                        var res = JsonConvert.SerializeObject(ApiResponse);
                        var returnObj = JsonConvert.DeserializeObject<T>(res);
                        return returnObj;
                    }
                }
                catch (Exception ex)
                {
                    var exeptionResponse = JsonConvert.DeserializeObject<T>(apiContet);
                    return exeptionResponse;
                }

                var APIResponse = JsonConvert.DeserializeObject<T>(apiContet);
                return APIResponse;
            }
            catch (Exception ex)
            {
                APIResponse dto = new APIResponse
                {
                    ErrorMessages = new List<string> { Convert.ToString(ex.Message) },
                    IsUsccess = false
                };
                var res = JsonConvert.SerializeObject(dto);
                var APIResponse = JsonConvert.DeserializeObject<T>(res);
                return APIResponse;
            }
        }
    }
}
