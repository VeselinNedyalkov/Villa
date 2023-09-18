using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using TrainingAPI.Data;
using TrainingAPI.Models;
using TrainingAPI.Models.DTO;
using TrainingAPI.Repository.Contracts;

namespace TrainingAPI.Repository
{
    public class UserRepository : IUserRepository
    {
        private readonly AplicationDbContext db;
        private string secretKey;

        public UserRepository(AplicationDbContext _db,
            IConfiguration configuration)
        {
            db = _db;
            secretKey = configuration.GetValue<string>("ApiSettings:Secret");
        }
        public bool IsUniqueUser(string username)
        {
            var user = db.LocalUsers.FirstOrDefault(u => u.UserName == username);

            if (user == null)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public async Task<LoginResponseDTO> Login(LoginRequestDTO loginRequesDto)
        {
            var user = db.LocalUsers.FirstOrDefault(u => u.UserName == loginRequesDto.UserName &&
            u.Password == loginRequesDto.Password);

            if (user == null)
            {
                return new LoginResponseDTO()
                {
                    Token = "",
                    User = null
                };
            }
            //is user was found generate JWT Token

            var tokenHandler = new JwtSecurityTokenHandler();
            //convert the secret key in bytes
            var key = Encoding.ASCII.GetBytes(secretKey);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name, user.Id.ToString()),
                    new Claim(ClaimTypes.Role, user.Role)
                }),
                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials = new(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);

            LoginResponseDTO loginRequestDto = new LoginResponseDTO()
            {
                Token = tokenHandler.WriteToken(token),
                User = user,
            };

            return loginRequestDto;
        }

        public async Task<LocalUser> Register(RegistrationRequestDTO registrationRequesDto)
        {
            LocalUser user = new LocalUser()
            {
                UserName = registrationRequesDto.UserName,
                Password = registrationRequesDto.Password,
                Name = registrationRequesDto.Name,
                Role = registrationRequesDto.Role,
            };

            db.LocalUsers.Add(user);
            await db.SaveChangesAsync();
            user.Password = string.Empty;

            return user;
        }
    }
}
