using Appointments.BL.Queries;
using Appointments.DAL;
using Appointments.DataObjects;
using Appointments.DataObjects.MappedResponses;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

namespace Appointments.BL.Handlers
{
    public class ViewSpecialistsHandler(AppointmentsDbContext dbContext, IMapper mapper) 
        : IRequestHandler<ViewSpecialistsQuery, IEnumerable<SpecialistResponseModel>>
    {
        public async Task<IEnumerable<SpecialistResponseModel>> Handle(ViewSpecialistsQuery request, CancellationToken cancellationToken)
        {
            var result = new List<Specialist>();
            if (request.ServiceCode != null && request.RequestedSpecialists.IsNullOrEmpty())
            {
                var serviceExperts = await dbContext.FullServiceData.AsNoTracking().Where(x => x.ServiceCode == request.ServiceCode).Select(x => x.Specialist).ToListAsync(cancellationToken);
                result = await dbContext.Specialists.AsNoTracking().Where(s => serviceExperts.Contains(s.Id)).ToListAsync(cancellationToken);
            }
            else if (request.RequestedSpecialists.Count > 0)
            {
                result = await dbContext.Specialists.AsNoTracking().Where(x => request.RequestedSpecialists.Contains(x.Id)).ToListAsync(cancellationToken);
            }

            result = await dbContext.Specialists.AsNoTracking().ToListAsync(cancellationToken);

            var response = mapper.Map<List<SpecialistResponseModel>>(result);

            if(request.ServiceCode != null)
            {
                foreach (var specialist in response)
                {
                    specialist.Price = await dbContext.Prices.AsNoTracking().Where(x => x.ServiceId == request.ServiceCode && x.Grade == specialist.Grade).Select(x => x.PriceValue).FirstOrDefaultAsync(cancellationToken);
                }
            }
            
            return response;
        }
    }
}

