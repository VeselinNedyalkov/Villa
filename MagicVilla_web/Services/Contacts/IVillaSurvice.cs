using MagicVilla_web.Models.DTO;

namespace MagicVilla_web.Services.Contacts
{
    public interface IVillaSurvice
    {
        Task<T> GetAllAsync<T>();
        Task<T> GetAsync<T>(int id);
        Task<T> CreateAsync<T>(VillaCreateDTO dto);
        Task<T> UpdateAsync<T>(VillaUpdateDTO dto);
        Task<T> DeleteAsync<T>(int id);
    }
}
