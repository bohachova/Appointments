using Appointments.DataObjects;
using Appointments.DataObjects.MappedResponses;
using AutoMapper;

namespace Appointments.BL.Profiles
{
    public class ServiceProfile : Profile
    {
        public ServiceProfile() 
        {
            CreateMap<Service, ServiceResponseModel>();
        }
    }
}
