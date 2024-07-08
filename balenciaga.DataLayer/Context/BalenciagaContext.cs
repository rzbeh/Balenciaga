using balenciaga.DataLayer.Entities.User;
using balenciaga.DataLayer.Entities.Wallet;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace balenciaga.DataLayer.Context
{
    public class BalenciagaContext : DbContext
    {
        public BalenciagaContext(DbContextOptions<BalenciagaContext> options) : base(options)
        {

        }

        #region User 
        public DbSet<Role> Roles { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<UserRoles> UserRoles { get; set; }
        #endregion

        #region wallet
        public DbSet<WalletType> walletTypes { get; set; }
        public DbSet<Wallet> Wallets { get; set; }

        #endregion

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>()
                .HasQueryFilter(u => !u.IsDelete);
            modelBuilder.Entity<Role>()
                .HasQueryFilter(r => !r.IsDelete);
            base.OnModelCreating(modelBuilder);
        }
    }
}
