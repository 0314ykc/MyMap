using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using MyMap.Models.identity;
using MyMap.Models.Map;
using MyMap.Models.others;

namespace MyMap.Data
{
    public class ApplicationDbContext : IdentityDbContext<user,IdentityRole<long>,long>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
            

        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            #region Modeify default identity table name
            builder.Entity<user>(x=>x.ToTable("user"));
            #endregion

            #region relationship
            builder.Entity<mapModel>()
                .HasOne(x => x.user)
                .WithMany(x => x.maps)
                .OnDelete(DeleteBehavior.ClientCascade);

            builder.Entity<map_placeModel>()
                .HasOne(x => x.map)
                .WithMany(x => x.map_places)
                .OnDelete(DeleteBehavior.ClientCascade);

            builder.Entity<map_place_talksModel>()
                .HasOne(x => x.map_place)
                .WithMany(x => x.map_place_talks)
                .OnDelete(DeleteBehavior.ClientCascade);
            builder.Entity<map_place_talksModel>()
                .HasOne(x => x.user)
                .WithMany(x => x.map_place_talks)
                .OnDelete(DeleteBehavior.ClientCascade);

            builder.Entity<imageModel>()
                .HasOne(x => x.user)
                .WithMany(x => x.images)
                .OnDelete(DeleteBehavior.ClientCascade);
            
            builder.Entity<talksModel>()
                .HasOne(x => x.user)
                .WithMany(x => x.talks)
                .OnDelete(DeleteBehavior.ClientCascade);
            #endregion

        }

        public DbSet<mapModel> mapModels { get; set; }
        public DbSet<map_placeModel> map_placeModels { get; set; }
        public DbSet<map_place_talksModel> map_place_talksModels { get; set; }
        public DbSet<imageModel> imageModels { get; set; }
        public DbSet<talksModel> talksModels { get; set; }
        public DbSet<user> user { get; set; }
    }
}