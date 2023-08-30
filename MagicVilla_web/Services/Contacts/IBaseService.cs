using MagicVilla_web.Models;
using MagicVilMagicVilla_web.Models;

namespace MagicVilla_web.Services.Contacts
{
    public interface IBaseService
    {
        APIResponse responseModel { get; set; }

        Task<T> SendAsync<T>(APIRequest apiRequest);
    }
}
