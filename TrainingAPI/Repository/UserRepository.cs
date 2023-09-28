using AutoMapper;
using Microsoft.AspNetCore.Identity;
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
        private readonly UserManager<ApplicationUser> userManager;
        private readonly RoleManager<IdentityRole> roleManager;
        private string secretKey;
        private readonly IMapper mapper;

        public UserRepository(AplicationDbContext _db,
            IConfiguration configuration, UserManager<ApplicationUser> _userManager,
            IMapper _mapper, RoleManager<IdentityRole> _roleManager)
        {
            db = _db;
            userManager = _userManager;
            secretKey = configuration.GetValue<string>("ApiSettings:Secret");
            mapper = _mapper;
            roleManager = _roleManager;
        }
        public bool IsUniqueUser(string username)
        {
            var user = db.ApplicationUsers.FirstOrDefault(u => u.UserName == username);

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
            var user = db.ApplicationUsers.FirstOrDefault(u => u.UserName == loginRequesDto.UserName);

            bool isValid = await userManager.CheckPasswordAsync(user, loginRequesDto.Password);

            if (user == null || !isValid)
            {
                return new LoginResponseDTO()
                {
                    Token = "",
                    User = null
                };
            }
            var role = await userManager.GetRolesAsync(user);
            //is user was found generate JWT Token
            var tokenHandler = new JwtSecurityTokenHandler();
            //convert the secret key in bytes
            var key = Encoding.ASCII.GetBytes(secretKey);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name, user.UserName.ToString()),
                    new Claim(ClaimTypes.Role, role.FirstOrDefault())
                }),
                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials = new(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);

            LoginResponseDTO loginRequestDto = new LoginResponseDTO()
            {
                Token = tokenHandler.WriteToken(token),
                User = mapper.Map<UserDTO>(user)
            };

            return loginRequestDto;
        }

        public async Task<UserDTO> Register(RegistrationRequestDTO registrationRequesDto)
        {
            ApplicationUser user = new ApplicationUser()
            {
                UserName = registrationRequesDto.UserName,
                Email = registrationRequesDto.UserName,
                NormalizedEmail = registrationRequesDto.UserName.ToUpper(),
                Name = registrationRequesDto.Name,
            };

            try
            {
                var result = await userManager.CreateAsync(user, registrationRequesDto.Password);
                if (result.Succeeded)
                {
                    if (!roleManager.RoleExistsAsync("admin").GetAwaiter().GetResult())
                    {
                        await roleManager.CreateAsync(new IdentityRole("admin"));
                        await roleManager.CreateAsync(new IdentityRole("customer"));
                    }
                    //for example is admin 
                    await userManager.AddToRoleAsync(user, "admin");
                    var userToReturn = db.ApplicationUsers
                        .FirstOrDefault(u => u.UserName == registrationRequesDto.UserName);

                    
                    return mapper.Map<UserDTO>(userToReturn);
                }
            }
            catch (Exception ex)
            {

            }

            return new UserDTO();
        }
    }
}
