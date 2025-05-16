using Appointments.BL.Queries;
using Appointments.DAL;
using Appointments.DataObjects;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Appointments.BL.Handlers
{
    public class GetServicePricesHandler (AppointmentsDbContext dbContext)
        : IRequestHandler<GetServicePricesQuery, IEnumerable<int>>
    {
        public async Task<IEnumerable<int>> Handle(GetServicePricesQuery request, CancellationToken cancellationToken)
        {
            var prices = await dbContext.Prices.AsNoTracking().Where(x => x.ServiceId == request.ServiceId).ToListAsync(cancellationToken);
            
            if(request.SpecialistId != null && prices.Count > 1)
            {
                var grade = await dbContext.Specialists.AsNoTracking()
                                                           .Where(x => x.Id == request.SpecialistId)
                                                           .Select(x => x.Grade)
                                                           .FirstAsync(cancellationToken);
                return prices.Where(x => x.Grade == grade).Select(x => x.PriceValue);
            }

            return prices.Select(x => x.PriceValue).OrderBy(x => x).ToList();
        }
    }
}
