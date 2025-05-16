using Appointments.BL.Queries;
using Appointments.DAL;
using Appointments.DataObjects;
using Appointments.DataObjects.MappedResponses;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Appointments.BL.Handlers
{
    public class GetServiceListHandler(AppointmentsDbContext dbContext, IMapper mapper, IMediator mediator) 
        : IRequestHandler<GetServiceListQuery, IEnumerable<ServiceResponseModel>>
    {
        public async Task<IEnumerable<ServiceResponseModel>> Handle(GetServiceListQuery request, CancellationToken cancellationToken)
        {
            List<Service> result = new List<Service>();

            if (request.SpecialistId != null)
            {
                var specialistsServiceCodes = await dbContext.FullServiceData.AsNoTracking().Where(x => x.Specialist == request.SpecialistId).Select(x => x.ServiceCode).ToListAsync(cancellationToken);
                result = await dbContext.ServiceList.AsNoTracking().Where(x => specialistsServiceCodes.Contains(x.Id)).ToListAsync(cancellationToken);
            }
            else if (request.Timing != null) 
            {
                foreach(var specialist in request.AvailableSpecialists)
                {
                    var specialistsServiceCodes = await dbContext.FullServiceData.AsNoTracking().Where(x => x.Specialist == specialist).Select(x => x.ServiceCode).ToListAsync(cancellationToken);
                    var services = await dbContext.ServiceList.AsNoTracking().Where(x => specialistsServiceCodes.Contains(x.Id) && x.Timing <= request.Timing).ToListAsync(cancellationToken);
                    if(services.Any())
                    {
                        services.ForEach(x => x.SpecialistId = specialist);
                        result.AddRange(services);
                    }
                }
            }
            else
            {
                result = await dbContext.ServiceList.AsNoTracking().ToListAsync(cancellationToken);
            }

            var listResponse = mapper.Map<List<ServiceResponseModel>>(result);
            foreach (var service in listResponse)
            {
                if(request.SpecialistId != null)
                {
                    var query = new GetServicePricesQuery { ServiceId = service.Id, SpecialistId = request.SpecialistId };
                    var prices = await mediator.Send(query);
                    service.Prices = prices.ToList();
                }
                else
                {
                    var query = new GetServicePricesQuery { ServiceId = service.Id };
                    var prices = await mediator.Send(query);
                    service.Prices = prices.ToList();
                }
            }

            return listResponse;
        }
    }
}