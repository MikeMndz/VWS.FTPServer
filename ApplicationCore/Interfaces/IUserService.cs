using ApplicationCore.Definitions;
using ApplicationCore.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ApplicationCore.Interfaces
{
    public interface IUserService
    {
        Task CreateAsync(UserData model);
        Task UpdateAsync(UserData model);
        Task DeleteAsync(int id);
        Task<List<UserData>> GetAllAsync();
        Task<UserData> GetAsync(int id);
        Task<UserData> GetByAccountAsync(string account);
    }
}
