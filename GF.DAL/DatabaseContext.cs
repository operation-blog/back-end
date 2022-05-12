using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GF.DAL.Entities;
using Microsoft.EntityFrameworkCore;

namespace GF.DAL
{
   public class DatabaseContext : DbContext
    {
        public DatabaseContext(DbContextOptions<DatabaseContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<BlogUser>().HasKey(sc => new { sc.UserId, sc.BlogId });

            builder.Entity<BlogUser>()
                .HasOne<User>(sc => sc.User)
                .WithMany(s => s.Blogs)
                .HasForeignKey(sc => sc.UserId);


            builder.Entity<BlogUser>()
                .HasOne<Blog>(sc => sc.Blog)
                .WithMany(s => s.Authors)
                .HasForeignKey(sc => sc.BlogId);

            base.OnModelCreating(builder);
        }

        public DbSet<User> Users { get; set; }

        public DbSet<Blog> Blogs { get; set; }

        public DbSet<BlogUser> BlogUsers { get; set; }

        public DbSet<AccessToken> AccessTokens { get; set; }
    }
}
