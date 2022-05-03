using ApplicationCore.Definitions;
using System.Threading.Tasks;

namespace ApplicationCore.Interfaces
{
    public interface IAuthService
    {
        LoginResult Login(string account, string password);
        Task<LoginResult> LoginAsync(string account, string password);
        bool ValidateToken(string token);
    }
}
