using TrainingAPI.Models.DTO;

namespace TrainingAPI.Repository.Contracts
{
    public interface IUserRepository
    {
        bool IsUniqueUser(string username);
        Task<LoginResponseDTO> Login(LoginRequestDTO loginRequesDto);

        Task<UserDTO> Register(RegistrationRequestDTO registrationRequesDto);
    }
}
