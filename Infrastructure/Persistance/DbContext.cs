using Domain;
using Microsoft.EntityFrameworkCore;
using System.Reflection;
using System.Reflection.Emit;

namespace Infrastructure.Persistance
{
    public class KardinoTemplateDbContext : DbContext
    {
        public KardinoTemplateDbContext(DbContextOptions<KardinoTemplateDbContext> contextOptions)
        : base(contextOptions)
        {
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Account> Accounts { get; set; }
        public DbSet<UserLog> UserLogs { get; set; }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            var typesToRegister = Assembly.GetExecutingAssembly().GetTypes()
                                         .Where(type => !String.IsNullOrEmpty(type.Namespace))
                                         .Where(type => type.BaseType != null &&
                                                        type.BaseType.IsGenericType &&
                                                        type.BaseType.GetGenericTypeDefinition() == typeof(IEntityTypeConfiguration<>));
            //foreach (var type in typesToRegister)
            //{
            //    dynamic configurationInstance = Activator.CreateInstance(type);
            //    builder.ApplyConfiguration(configurationInstance);
            //}
            foreach (var type in typesToRegister)
            {
                builder.HasSequence<int>(type.Namespace ?? "").StartsAt(1000).IncrementsBy(1);
            }
            builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
            builder.HasSequence<int>("Account").StartsAt(1000).IncrementsBy(1);
            builder.HasSequence<int>("User").StartsAt(1000).IncrementsBy(1);
            builder.HasSequence<int>("UserLog").StartsAt(1000).IncrementsBy(1);
            base.OnModelCreating(builder);


            builder.Entity<User>().HasData(new User
            {
                Id = 1,
                AccountId = null,
                UserName = "admin",
                Password= "5e933ACDs+eNfMMOpz4K3t+5PjmsInYW0R47T5xCilw=",
                Salt= "N1y9R5uiDN7nFChyBQKylw==",
                Email = "admin@example.com",
                UserType=Domain.Enums.UserType.SystemUser,
                IsActive = true,
            });
        }

    }
}
