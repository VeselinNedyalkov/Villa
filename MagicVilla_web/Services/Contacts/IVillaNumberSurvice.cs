using MagicVilla_web.Models.DTO;

namespace MagicVilla_web.Services.Contacts
{
    public interface IVillaNumberSurvice
    {
        Task<T> GetAllAsync<T>();
        Task<T> GetAsync<T>(int id);
        Task<T> CreateAsync<T>(VillaCreateNumberDTO dto);
        Task<T> UpdateAsync<T>(VillaUpdateNumberDTO dto);
        Task<T> DeleteAsync<T>(int id);
    }
}
