using Microsoft.EntityFrameworkCore;
using ReadME.Models;
namespace ReadME.Database
{
    public class DataContext:DbContext
    {
         public DataContext(DbContextOptions<DataContext> options)
            :base(options)
        {
            
        }

        public DbSet<User> Users { get; set; }

        public DbSet<OTP> OTP { get; set; }
    }
}