using ApplicationCore.Entities;
using ApplicationCore.Interfaces;
using Infrastructure.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Services
{
    public class RbacService : IRbacService
    {
        private readonly IGenericRepository<UserRole> _userRoleRepository;
        private readonly IGenericRepository<Role> _roleRepository;

        public RbacService()
        {
            _userRoleRepository = new GenericRepository<UserRole>();
            _roleRepository = new GenericRepository<Role>();
        }

        public RbacService(IGenericRepository<UserRole> userRoleRepository, IGenericRepository<Role> roleRepository)
        {
            _userRoleRepository = userRoleRepository;
            _roleRepository = roleRepository;
        }

 
        public async Task<IEnumerable<Role>> GetRolesAsync()
        {
            return await _roleRepository.GetAllAsync();
        }

        public async Task<Role> GetRoleAsync(int id)
        {
            return await _roleRepository.GetByIdAsync(id); //$"Role(Id={id})"
        }

        public async Task CreateRoleAsync(Role model)
        {
            var nameInUse = await _roleRepository.GetCountWhereAsync(x => x.Name == model.Name);
            if (nameInUse > 0)
                throw new Exception($"El nombre '{model.Name}' ya se encuentra en uso.");

            model.CreatedOn = DateTime.Now;
            await _roleRepository.AddAsync(model);
        }

        public async Task UpdateRoleAsync(Role model)
        {
            var record = await _roleRepository.GetByIdAsync(model.Id);
            if (record == null)
                throw new Exception($"El registro no existe.");

            //if (record.IsStatic)
            //    throw new Exception("El registro no es modificable.");

            var nameInUse = await _roleRepository.GetCountWhereAsync(x => x.Name == model.Name && x.Id != model.Id);
            if (nameInUse > 0)
                throw new Exception($"El nombre '{model.Name}' ya se encuentra en uso.");

            model.CreatedOn = record.CreatedOn;
            model.ModifiedOn = DateTime.Now;
            await _roleRepository.EditAsync(model);
        }

        public async Task DeleteRoleAsync(int id)
        {
            var record = await _roleRepository.GetByIdAsync(id);
            if (record == null)
                throw new Exception($"El registro no existe.");

            //if (record.IsStatic)
            //    throw new Exception("El registro no se puede eliminar.");

            await _roleRepository.RemoveAsync(record);
        }

        public async Task SetUserRoleAsync(int idUser, int idRole)
        {
            var userRole = await _userRoleRepository.FirstOrDefaultAsync(x => x.FkUser == idUser);
            if (userRole == null)
            {
                userRole = new UserRole();
                userRole.CreatedOn = DateTime.Now;
                userRole.FkUser = idUser;
                userRole.FkRole = idRole;
                await _userRoleRepository.AddAsync(userRole);
            }
            else
            {
                userRole.ModifiedOn = DateTime.Now;
                userRole.FkRole = idRole;
                await _userRoleRepository.EditAsync(userRole);
            }
        }

        public async Task<Role> GetUserRoleAsync(int idUser)
        {
            Role role = null;
            var userRole = await _userRoleRepository.FirstOrDefaultAsync(x => x.FkUser == idUser);
            if (userRole != null)
                role = await _roleRepository.GetByIdAsync(userRole.FkRole);

            return role;
        }

        public async Task RemoveUserRoleAsync(int idUser)
        {
            var userRole = await _userRoleRepository.FirstOrDefaultAsync(x => x.FkUser == idUser);
            if (userRole != null)
                _userRoleRepository.Remove(userRole);
        }

        public Role GetRole(int id)
        {
            return _roleRepository.GetById(id);
        }

        public Role GetUserRole(int idUser)
        {
            Role role = null;
            var userRole = _userRoleRepository.FirstOrDefault(x => x.FkUser == idUser);
            if (userRole != null)
                role = _roleRepository.GetById(userRole.FkRole);

            return role;
        }
    }
}
