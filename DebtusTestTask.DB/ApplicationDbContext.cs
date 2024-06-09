using DebtusTestTask.DB.Entities;
using Microsoft.EntityFrameworkCore;

namespace DebtusTestTask.DB
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
            Database.EnsureCreated();
        }

        public DbSet<Employee> Employees { get; set; }
        public DbSet<WorkShift> WorkShifts { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Employee>()
                .HasMany(e => e.WorkShifts)
                .WithOne(ws => ws.Employee)
                .HasForeignKey(ws => ws.EmployeeId)
                .OnDelete(DeleteBehavior.Cascade);

            base.OnModelCreating(modelBuilder);
        }
    }
}