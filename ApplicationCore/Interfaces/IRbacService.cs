using ApplicationCore.Definitions;
using ApplicationCore.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ApplicationCore.Interfaces
{
    public interface IRbacService
    {
        Task<IEnumerable<Role>> GetRolesAsync();
        Task<Role> GetRoleAsync(int id);
        Task CreateRoleAsync(Role model);
        Task UpdateRoleAsync(Role model);
        Task DeleteRoleAsync(int id);
        Task SetUserRoleAsync(int idUser, int idRole);
        Task RemoveUserRoleAsync(int idUser);
        Task<Role> GetUserRoleAsync(int idUser);
        Role GetRole(int id);
        Role GetUserRole(int idUser);


    }
}
