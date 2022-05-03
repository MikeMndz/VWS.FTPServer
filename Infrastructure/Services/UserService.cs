using ApplicationCore.Definitions;
using ApplicationCore.Entities;
using ApplicationCore.Interfaces;
using Common;
using Infrastructure.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Infrastructure.Services
{
    public class UserService : IUserService
    {
        private readonly IGenericRepository<User> _userRepository;
        private readonly IRbacService _rbacService;

        public UserService()
        {
            _userRepository = new GenericRepository<User>();
            _rbacService = new RbacService();
        }

        public UserService(IGenericRepository<User> userRepository, IRbacService rbacService)
        {
            _userRepository = userRepository;
            _rbacService = rbacService;
        }

        public async Task CreateAsync(UserData model)
        {
            var accountInUse = await _userRepository.GetCountWhereAsync(x => x.Account == model.Account && x.Deleted == false); //, $"User(Account={model.Account},Deleted=false)"
            if (accountInUse > 0)
                throw new Exception($"El usuario '{model.Account}' ya se encuentra en uso.");

            if (string.IsNullOrEmpty(model.Password))
                throw new Exception("Se requiere una contraseña.");

            var record = new User();
            record.CreatedOn = DateTime.Now;
            record.Account = model.Account;
            record.Enabled = model.Enabled;
            record.Name = model.Name;
            record.LastName = model.LastName;
            record.MothersLastName = model.MothersLastName;
            record.Password = CipherSecurity.Encrypt(model.Password, GlobalParameters.ENCRYPTION_KEY);
            await _userRepository.AddAsync(record);
            if (model.IdRole > 0)
                await _rbacService.SetUserRoleAsync(record.Id, model.IdRole);

        }

        public async Task UpdateAsync(UserData model)
        {
            var record = await _userRepository.GetByIdAsync(model.Id);
            if (record == null)
                throw new Exception("El registro no existe.");

            var accountInUse = await _userRepository.GetCountWhereAsync(x => x.Account == model.Account && x.Id != model.Id && x.Deleted == false);
            if (accountInUse > 0)
                throw new Exception($"El usuario '{model.Account}' ya se encuentra en uso.");

            if (!string.IsNullOrEmpty(model.Password))
            {
                var decryptPassword = CipherSecurity.Decrypt(record.Password, GlobalParameters.ENCRYPTION_KEY);
                if (model.Password != decryptPassword)
                    record.Password = CipherSecurity.Encrypt(model.Password, GlobalParameters.ENCRYPTION_KEY);

            }

            record.ModifiedOn = DateTime.Now;
            record.Account = model.Account;
            record.Enabled = model.Enabled;
            record.Name = model.Name;
            record.LastName = model.LastName;
            record.MothersLastName = model.MothersLastName;
            await _userRepository.EditAsync(record);
            if (model.IdRole > 0)
                await _rbacService.SetUserRoleAsync(record.Id, model.IdRole);

        }

        public async Task DeleteAsync(int id)
        {
            var model = await _userRepository.GetByIdAsync(id);
            model.Enabled = false;
            model.Deleted = true;
            await _userRepository.EditAsync(model);
        }

        public async Task<UserData> GetAsync(int id)
        {
            var record = await _userRepository.FirstOrDefaultAsync(x => x.Id == id && x.Deleted == false);
            if (record == null)
                throw new Exception("El registro no existe.");

            var decryptPassword = CipherSecurity.Decrypt(record.Password, GlobalParameters.ENCRYPTION_KEY);
            var model = new UserData()
            {
                Id = record.Id,
                Account = record.Account,
                Enabled = record.Enabled,
                Name = record.Name,
                LastName = record.LastName,
                MothersLastName = record.MothersLastName,
                Uid = record.Uid,
                Password = decryptPassword
            };

            var role = await _rbacService.GetUserRoleAsync(record.Id);
            if (role != null)
            {
                model.IdRole = role.Id;
                model.Role = role.Name;
            }

            return model;
        }

        public async Task<List<UserData>> GetAllAsync()
        {
            var records = await _userRepository.GetWhereAsync(x => x.Deleted == false);
            var models = new List<UserData>();
            foreach (var record in records)
            {
                var decryptPassword = CipherSecurity.Decrypt(record.Password, GlobalParameters.ENCRYPTION_KEY);
                var model = new UserData()
                {
                    Id = record.Id,
                    Account = record.Account,
                    Enabled = record.Enabled,
                    Name = record.Name,
                    LastName = record.LastName,
                    MothersLastName = record.MothersLastName,
                    Uid = record.Uid,
                    Password = decryptPassword
                };

                var role = await _rbacService.GetUserRoleAsync(record.Id);
                if (role != null)
                {
                    model.IdRole = role.Id;
                    model.Role = role.Name;
                }
                models.Add(model);
            }
            return models;
        }

        public async Task<UserData> GetByAccountAsync(string account)
        {
            var record = await _userRepository.FirstOrDefaultAsync(x => x.Account == account && x.Deleted == false);
            if (record == null)
                throw new Exception("El registro no existe.");

            var model = new UserData()
            {
                Id = record.Id,
                Account = record.Account,
                Enabled = record.Enabled,
                Name = record.Name,
                LastName = record.LastName,
                MothersLastName = record.MothersLastName,
                Uid = record.Uid,
                Password = record.Password
            };

            var role = await _rbacService.GetUserRoleAsync(record.Id);
            if (role != null)
            {
                model.IdRole = role.Id;
                model.Role = role.Name;
            }
            return model;
        }

    }
}
