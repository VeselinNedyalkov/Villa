using MagicVilla_web.Models.DTO;

namespace MagicVilla_web.Services.Contacts
{
    public interface IVillaNumberSurvice
    {
        Task<T> GetAllAsync<T>(string token);
        Task<T> GetAsync<T>(int id , string token);
        Task<T> CreateAsync<T>(VillaCreateNumberDTO dto , string token);
        Task<T> UpdateAsync<T>(VillaUpdateNumberDTO dto , string token);
        Task<T> DeleteAsync<T>(int id , string token);
    }
}
