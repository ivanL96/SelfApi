namespace SelfHostApi.Models
{
    using Microsoft.AspNet.Identity.EntityFramework;
    using System;
    using System.Data.Entity;
    using System.Linq;

    public class DataContext : IdentityDbContext<ApplicationUser>
    {
        public DataContext()
            : base("name=DataContext", throwIfV1Schema: false)
        {
        }

        public DbSet<Client> Clients { get; set; }
        public DbSet<RefreshToken> RefreshTokens { get; set; }
    }
}