using MagicVilla_web.Models.DTO;

namespace MagicVilla_web.Services.Contacts
{
    public interface IAuthService
    {
        Task<T> LoginAsync<T>(LoginRequestDTO objCreate);
        Task<T> RegisterAsync<T>(RegistrationRequestDTO objCreate);
    }
}
