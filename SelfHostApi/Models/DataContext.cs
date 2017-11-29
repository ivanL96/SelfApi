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

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ApplicationUser>()
                .HasMany(x => x.UserMessages).WithRequired(x => x.ApplicationUser)
                .HasForeignKey(x => x.ApplicationUserId).WillCascadeOnDelete(true);


            base.OnModelCreating(modelBuilder);
        }

        public virtual DbSet<Client> Clients { get; set; }
        public virtual DbSet<RefreshToken> RefreshTokens { get; set; }

        public virtual DbSet<Message> Messages { get; set; }
    }
}