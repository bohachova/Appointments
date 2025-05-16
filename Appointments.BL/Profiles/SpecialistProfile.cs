

using Appointments.DataObjects;
using Appointments.DataObjects.MappedResponses;
using AutoMapper;

namespace Appointments.BL.Profiles
{
    public class SpecialistProfile : Profile
    {
        public SpecialistProfile()
        {
            CreateMap<Specialist, SpecialistResponseModel>();
        }
    }
}
