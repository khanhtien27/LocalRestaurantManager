using ManagementRestaurantLocation.Data;
using ManagementRestaurantLocation.Models;
using ManagementRestaurantLocation.Models.ModelDTO;
using ManagementRestaurantLocation.Repository.IRepository;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace ManagementRestaurantLocation.Repository
{
    public class UserRepository : IUserRepository
    {
        private readonly RestaurentDbContext _dbContext;
        private string secret;

        public UserRepository(RestaurentDbContext restaurentDbContext, IConfiguration configuration)
        {
            _dbContext = restaurentDbContext;
            secret = configuration.GetValue<string>("ApiSettings:Secret");
        }

        public bool IsUniqueUser(string username)
        {
            var user = _dbContext.localUsers.FirstOrDefault(x => x.UserName == username);
            if(user == null)
            {
                return true;
            }
            return false;
        }

        public async Task<LoginResponseDTO> Login(LoginRequestDTO loginRequestDTO)
        {
            var user = _dbContext.localUsers.FirstOrDefault(u => u.UserName.ToLower() == loginRequestDTO.UserNam.ToLower()
            && u.Password == loginRequestDTO.Password);
            if(user == null)
            {
                return new LoginResponseDTO()
                {
                    Token = "",
                    User = null
                };
            }
            //if user was found, generate JWT Token
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(secret);

            var tokenDescriptor = new SecurityTokenDescriptor()
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name, user.Id.ToString()),
                    new Claim(ClaimTypes.Role, user.Role)
                }),
                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials = new(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            LoginResponseDTO loginResponseDTO = new LoginResponseDTO()
            {
                Token = tokenHandler.WriteToken(token),
                User = user,
            };
            return loginResponseDTO;
        }

        public async Task<LocalUser> Register(RegisterationRequestDTO registerationRequestDTO)
        {
            var user = new LocalUser()
            {
                UserName = registerationRequestDTO.UserName,
                Password = registerationRequestDTO.Password,
                Name = registerationRequestDTO.Name,
                Role = registerationRequestDTO.Role,
            };
            await _dbContext.localUsers.AddAsync(user);
            await _dbContext.SaveChangesAsync();
            user.Password = "";
            return user;
        }
    }
}
