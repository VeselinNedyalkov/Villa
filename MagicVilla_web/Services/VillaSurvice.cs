using MagicVilla_web.Models.DTO;
using MagicVilla_web.Services.Contacts;

namespace MagicVilla_web.Services
{
    public class VillaSurvice : BaseService ,  IVillaSurvice
    {
        public Task<T> CreateAsync<T>(VillaCreateDTO dto)
        {
            throw new NotImplementedException();
        }

        public Task<T> DeleteAsync<T>(int id)
        {
            throw new NotImplementedException();
        }

        public Task<T> GetAllAsync<T>()
        {
            throw new NotImplementedException();
        }

        public Task<T> GetAsync<T>(int id)
        {
            throw new NotImplementedException();
        }

        public Task<T> UpdateAsync<T>(VillaUpdateDTO dto)
        {
            throw new NotImplementedException();
        }
    }
}
