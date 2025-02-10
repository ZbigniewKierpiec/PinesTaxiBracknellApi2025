using Microsoft.EntityFrameworkCore;
using PinesExecutiveTravelApi.Models.Domain;

namespace PinesExecutiveTravelApi.Data
{
    public class UserContext:DbContext
    {
      

        public DbSet<User> Users { get; set; }
        public DbSet<TaxiOrder> Reservations { get; set; }
        public DbSet<UserProfile> UserProfiles { get; set; }

        public DbSet<BlogImage>BlogImages { get; set; }


        public DbSet<SingleProfileImage> SingleProfileImages { get; set; }


        public UserContext(DbContextOptions<UserContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Configuring one-to-many relationship
            modelBuilder.Entity<User>()
                .HasMany(u => u.Reservations)
                .WithOne(r => r.User)
                .HasForeignKey(r => r.UserId)
                .OnDelete(DeleteBehavior.Cascade);


            modelBuilder.Entity<TaxiOrder>()
                .Property(r => r.Price)
                .HasColumnType("decimal(18,2)");

            // Configuring one-to-one relationship between User and UserProfile
            modelBuilder.Entity<User>()
                .HasOne(u => u.UserProfile)
                .WithOne(up => up.User)
                .HasForeignKey<UserProfile>(up => up.UserId)
                .OnDelete(DeleteBehavior.Cascade); // Cascade delete if User is deleted

            modelBuilder.Entity<BlogImage>()
     .HasOne(bi => bi.User)  // Assuming BlogImage has a User property (foreign key)
     .WithMany(u => u.BlogImages)  // Assuming User has a collection of BlogImages
     .HasForeignKey(bi => bi.UserId)  // The foreign key in BlogImage to User
     .OnDelete(DeleteBehavior.Cascade);  // Cascade delete if User is deleted




            //modelBuilder.Entity<SingleProfileImage>()
            //    .HasIndex(si => si.UserId)
            //    .IsUnique(); // Użytkownik może mieć tylko jedno zdjęcie

            modelBuilder.Entity<SingleProfileImage>()
               .HasIndex(s => s.UserId)
               .IsUnique();





        }




    }
}
