using IMS_InventoryManagmentSystem_.Models;
using IMS_InventoryManagmentSystem_.Models.DTO;
using IMS_InventoryManagmentSystem_.Repositories;
using IMS_InventoryManagmentSystem_.Service.IService;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace IMS_InventoryManagmentSystem_.Service
{
    public class UserService : IUserService
    {

        public readonly UserRepository _repository;
        public readonly ILogger<UserService> _logger;
        private readonly IConfiguration _configuration;
        public UserService(UserRepository repository, ILogger<UserService> logger,IConfiguration configuration)
        {
            _repository = repository;
            _logger = logger;
            _configuration=configuration;
        }

        public async Task<AuthDto> RegisterAsync(RegisterDTO registerDTO)
        {
            if (await _repository.GetUserAsync(registerDTO.UserName) != null)
            {
                throw new Exception("Username already exists.");
            }


            var user = new User()
            {
                UserName = registerDTO.UserName,
                Email = registerDTO.Email,
            };

            user.PasswordHash = new PasswordHasher<User>().HashPassword(user, registerDTO.Password);

            await _repository.AddUserAsync(user);

            await _repository.SaveChangesAsync();

            return new AuthDto
            {
                Message = "User Registered Succesfully"
            };
        }

        public async Task<ActionResult> LoginAsync(LoginDto loginDTO)
        {
            var user=await _repository.GetUserAsync(loginDTO.Email);
            if (user == null)
            {
                return new UnauthorizedObjectResult(new { Message = "Invalid email or password." });
            }

            var passwordHasher=new PasswordHasher<User>();
            var passwordVerificationResult = passwordHasher.VerifyHashedPassword(user, user.PasswordHash, loginDTO.Password);
            if (passwordVerificationResult == PasswordVerificationResult.Failed)
            {
                return new UnauthorizedObjectResult(new { Message = "Invalid email or password." });
            }
            var token = CreateToken();
            return new OkObjectResult(new { token = token });
        }

        public string CreateToken(User user)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Email, user.Email),
                new Claim(ClaimTypes.NameIdentifier,user.Id.ToString()),
                new Claim(ClaimTypes.Role,user.Role)
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["AppSettings:SecretKey"]);
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512);

            var tokenDescriptor = new JwtSecurityToken(
               issuer: _configuration["AppSettings:Issuer"],
               audience: _configuration["AppSettings:Audience"],
               claims: claims,
               expires: DateTime.UtcNow.AddDays(1),
               signingCredentials: creds
        );

            return new JwtSecurityTokenHandler().WriteToken(tokenDescriptor);

        }

     

    }
}
