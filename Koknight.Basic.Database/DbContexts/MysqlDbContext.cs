/*************************************************************************************
 *
 * File name:   MysqlDbContext.cs
 * Description: 
 * 
 * Version：  V1.0
 * Creator  Eason Huang
 * Create time：  2024/11/14 22:28
 * ======================================
*************************************************************************************/
using Koknight.Basic.Database.Oauth;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace Koknight.Basic.Database.DbContexts
{
    public class MysqlDbContext : DbContext
    {
        public IConfiguration Configuration;

        public MysqlDbContext(IConfiguration configuration) : base()
        {
            this.Configuration = configuration;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
            var dbConnectionString = Configuration.GetConnectionString("DefaultConnection");
            optionsBuilder.UseMySql(dbConnectionString, new MySqlServerVersion(new Version(8, 0, 40)));
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfigurationsFromAssembly(this.GetType().Assembly);
        }

        public DbSet<User> Users { get; set; }

        public DbSet<UserInfo> UserInfos { get; set; }  
    }
}
