using Microsoft.EntityFrameworkCore;
using TrainingAPI.Models;
using TrainingAPI.Models.DTO;

namespace TrainingAPI.Data
{
    public class AplicationDbContext : DbContext
    {

        public AplicationDbContext(DbContextOptions<AplicationDbContext> options)
            :base(options) 
        {
                
        }
        public DbSet<Villa> Villas { get; set; }
        public DbSet<VillaNumber> VillaNumbers { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Villa>()
                .HasData(
                new Villa
                { 
                    Id = 1, 
                    Name = "Pool View", 
                    Details = "Nice villa",
                    Rate = 5,
                    ImageUrl = "https://www.myluxoria.com/storage/app/uploads/public/630/77d/1e4/63077d1e4e7a2970728706.jpg",
                    Sqft = 100, 
                    Occupancy = 8 ,
                    Amenity = "",
                    CreatedTime = DateTime.Now,
                    UpdatedTime = DateTime.Now,
                },
                new Villa
                {
                    Id = 2,
                    Name = "Sea view",
                    Details = "10 people vilal with good villa",
                    Rate = 5,
                    ImageUrl = "https://media.architecturaldigest.com/photos/61b24b1bdf5163297d83ae8c/4:3/w_3763,h_2822,c_limit/Stella_Maris_Exterior.jpg",
                    Sqft = 120,
                    Occupancy = 15,
                    Amenity = "",
                    CreatedTime = DateTime.Now,
                    UpdatedTime = DateTime.Now,
                }
                );

            base.OnModelCreating(modelBuilder);
        }
    }
}
