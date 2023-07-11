using Microsoft.EntityFrameworkCore;
using TrainingAPI.Models.DTO;

namespace TrainingAPI.Data
{
    public class AplicationDbContext : DbContext
    {

        public AplicationDbContext(DbContextOptions<AplicationDbContext> options)
            :base(options) 
        {
                
        }
        DbSet<Villa> Villas { get; set; }

       
    }
}
