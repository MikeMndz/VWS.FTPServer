using ApplicationCore.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Infrastructure.Repositories
{
    public class ExtendedRepository : GenericRepository<User>
    {
        void TestingMethod()
        {
            var e = GetAll();
        }
    }
}
