using BackendService.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace BackendService.Infrastructure.Persistence
{
    public class ApplicationContext : DbContext
    {
        public ApplicationContext(DbContextOptions<ApplicationContext> options) : base(options) { }
        public ApplicationContext() { }

        //Add Db here
        public virtual DbSet<MsUser> MsUsers { get; set; }
        public virtual DbSet<Approval> Approvals { get; set; }
        public virtual DbSet<Booking> Bookings { get; set; }
        public virtual DbSet<Vehicle> Vehicles { get; set; }
        public virtual DbSet<VehicleUsage> VehicleUsages { get; set; }
        //End
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseNpgsql("Name=DefaultConnection");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

        }
    }
}
