
using Appointments.DataObjects;
using Appointments.DataObjects.Security;
using Microsoft.EntityFrameworkCore;

namespace Appointments.DAL
{
    public class AppointmentsDbContext : DbContext
    {
        public DbSet<Service> ServiceList { get; set; }
        public DbSet<Specialist> Specialists { get; set; }
        public DbSet<ScheduleUnit> Schedule {  get; set; }
        public DbSet<ServiceData> FullServiceData { get; set; }
        public DbSet<Appointment> Appointments { get; set; }
        public DbSet<Price> Prices { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Token> Tokens { get; set; }
        public AppointmentsDbContext(DbContextOptions<AppointmentsDbContext> options) : base(options)
        {
        }
    }
}
