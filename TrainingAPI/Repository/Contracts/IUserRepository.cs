using TrainingAPI.Models;
using TrainingAPI.Models.DTO;

namespace TrainingAPI.Repository.Contracts
{
    public interface IUserRepository
    {
        bool IsUniqueUser(string username);
        Task<LoginResponseDTO> Login(LoginRequestDTO loginRequesDto);

        Task<LocalUser> Register(RegistrationRequestDTO registrationRequesDto);
    }
}
