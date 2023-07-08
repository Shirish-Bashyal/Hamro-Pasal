using Hamro_Pasal.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Hamro_Pasal.Data
{
    public class ApplicationDbContext:IdentityDbContext<Authenticate>

    {


        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        { }

        public DbSet<Hamro_Pasal.Models.Booking> tbl_bookings { get; set; }

        public DbSet<Hamro_Pasal.Models.Category> tbl_category { get; set; }


        public DbSet<Hamro_Pasal.Models.Location> tbl_location { get; set; }

        public DbSet<Hamro_Pasal.Models.UserAds> tbl_ads{ get; set; }

        public DbSet<Hamro_Pasal.Models.UserDetails> tbl_user_details{ get; set; }






    }
}
