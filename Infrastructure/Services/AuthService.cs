using ApplicationCore.Definitions;
using ApplicationCore.Entities;
using ApplicationCore.Interfaces;
using Common;
using Infrastructure.Repositories;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Services
{
    public class AuthService : IAuthService
    {
        private readonly IGenericRepository<User> _userRepo;
        private readonly IRbacService _rbacService;

        public AuthService()
        {
            _userRepo = new GenericRepository<User>();
            _rbacService = new RbacService();
        }

        public AuthService(IGenericRepository<User> userRepository, IRbacService rbacService)
        {
            _userRepo = userRepository;
            _rbacService = rbacService;
        }

        public LoginResult Login(string account, string password)
        {
            try
            {
                var currentDatetime = DateTime.Now;
                var loginResult = new LoginResult();
                var user = _userRepo.FirstOrDefault(x => x.Account == account && x.Deleted == false);
                if (user == null) { throw new Exception("El usuario no existe"); }
                var decryptPassword = CipherSecurity.Decrypt(user.Password, GlobalParameters.ENCRYPTION_KEY);
                if (password != decryptPassword) { throw new Exception("La contraseña es incorrecta."); }
                if (!user.Enabled) { throw new Exception("El usuario se encuentra inactivo, consulte con un administrador."); }

                loginResult.LastAccess = currentDatetime;
                loginResult.Name = $"{user.Name} {user.LastName} {user.MothersLastName}";
                loginResult.Account = user.Account;
                loginResult.Uid = user.Uid;

                var role = _rbacService.GetUserRole(user.Id);
                if (role != null)
                    loginResult.Role = role.Name;

                user.LastAccess = currentDatetime;
                _userRepo.Edit(user);
                return loginResult;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public async Task<LoginResult> LoginAsync(string account, string password)
        {
            try
            {
                var currentDatetime = DateTime.Now;
                var loginResult = new LoginResult();
                var user = await _userRepo.FirstOrDefaultAsync(x => x.Account == account && x.Deleted == false);
                if (user == null) { throw new Exception("El usuario no existe"); }
                var decryptPassword = CipherSecurity.Decrypt(user.Password, GlobalParameters.ENCRYPTION_KEY);
                if (password != decryptPassword) { throw new Exception("La contraseña es incorrecta."); }
                if (!user.Enabled) { throw new Exception("El usuario se encuentra inactivo, consulte con un administrador."); }

                loginResult.LastAccess = currentDatetime;
                loginResult.Name = $"{user.Name} {user.LastName} {user.MothersLastName}";
                loginResult.Account = user.Account;
                loginResult.Uid = user.Uid;

                var role = await _rbacService.GetUserRoleAsync(user.Id);
                if (role != null)
                    loginResult.Role = role.Name;

                user.LastAccess = currentDatetime;
                await _userRepo.EditAsync(user);

                //var token = BuildToken(user.Account, "Default");
                return loginResult;
            }
            catch (Exception)
            {
                return null;
            }
        }

        private TokenResult BuildToken(string account, string role)
        {
            var config = ConfigurationHandler.Get();
            var notBeforeDate = DateTime.Now;
            var expireDate = notBeforeDate.AddHours(1);
            var claims = new[] {
                new Claim(ClaimTypes.Name, account),
                new Claim(ClaimTypes.Role, role),
                new Claim(ClaimTypes.NameIdentifier,
                Guid.NewGuid().ToString())
            };
            var symmetricSecurityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(config["Jwt:Key"]));
            var signingCredentials = new SigningCredentials(symmetricSecurityKey, SecurityAlgorithms.HmacSha256);
            var header = new JwtHeader(signingCredentials);
            var payload = new JwtPayload(
                    issuer: config["Jwt:Issuer"],
                    audience: config["Jwt:Audience"],
                    claims: claims,
                    notBefore: notBeforeDate,
                    expires: expireDate
                );

            var tokenResult = new JwtSecurityTokenHandler().WriteToken(new JwtSecurityToken(header, payload));
            return new TokenResult() { Expires = expireDate, Token = tokenResult };
        }

        public bool ValidateToken(string token)
        {
            var config = ConfigurationHandler.Get();
            var mySecret = Encoding.UTF8.GetBytes(config["Jwt:Key"]);
            var mySecurityKey = new SymmetricSecurityKey(mySecret);
            var tokenHandler = new JwtSecurityTokenHandler();
            try
            {
                tokenHandler.ValidateToken(token,
                new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidIssuer = config["Jwt:Issuer"],
                    ValidAudience = config["Jwt:Audience"],
                    IssuerSigningKey = mySecurityKey,
                }, out SecurityToken validatedToken);
            }
            catch
            {
                return false;
            }
            return true;
        }

    }
}
