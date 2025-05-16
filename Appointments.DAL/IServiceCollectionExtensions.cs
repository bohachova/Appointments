using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using Microsoft.EntityFrameworkCore;

namespace Appointments.DAL
{
    public static class IServiceCollectionExtensions
    {
        public static IServiceCollection AddAppointmentsDbContext(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContextFactory<AppointmentsDbContext>(o =>
            {
                o.UseSqlServer(configuration.GetConnectionString("main"));
            });

            return services;
        }
    }
}
