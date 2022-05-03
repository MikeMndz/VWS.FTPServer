using ApplicationCore.Entities;
using ApplicationCore.Interfaces;
using Infrastructure.Repositories;
using Infrastructure.Services;
using Microsoft.Extensions.DependencyInjection;
using RepoDb;
using System;
using System.Collections.Generic;
using System.Text;

namespace Infrastructure
{
    public static class ServiceRegistration
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services)
        {
            if (!MySqlBootstrap.IsInitialized)
                MySqlBootstrap.Initialize();

            //Generic Repositories
            services.AddSingleton<IGenericRepository<User>, GenericRepository<User>>();
            services.AddSingleton<IGenericRepository<UserRole>, GenericRepository<UserRole>>();
            services.AddSingleton<IGenericRepository<Role>, GenericRepository<Role>>();

            //Specific Repositories
            //services.AddSingleton<IUserRepository, UserRepository>();

            //Services
            services.AddSingleton<IAuthService, AuthService>();
            services.AddSingleton<IUserService, UserService>();
            services.AddSingleton<IRbacService, RbacService>();

            return services;
        }
    }
}
