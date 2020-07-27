using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Advantage.API.Database.Entities
{
    public class ApiContext : DbContext
    {
        #region Constructors
        public ApiContext(DbContextOptions<ApiContext> options) : base(options) { }

        #endregion

        #region DbSets
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<Server> Servers { get; set; }

        #endregion
    }
}
