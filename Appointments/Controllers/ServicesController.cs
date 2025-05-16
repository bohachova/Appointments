using Appointments.BL.Queries;
using Appointments.DataObjects.Requests;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Identity.Client;

namespace Appointments.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ServicesController(IMediator mediator) : ControllerBase
    {
        [HttpGet("all")]
        public async Task<IActionResult> GetAllServices()
        {
            var query = new GetServiceListQuery();
            var list = await mediator.Send(query);
            return Ok(list);
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetSpecialistsServices([FromRoute] int id)
        {
            var query = new GetServiceListQuery { SpecialistId = id };
            var list = await mediator.Send(query);
            return Ok(list);
        }

        [HttpPost("time")]
        public async Task<IActionResult> GetAvailableServices(GetAvailableServicesRequest request)
        {
            var durationQuery = new CheckTimeSlotDurationQuery { Date = request.DateTime.Date, Time = request.DateTime.TimeOfDay, AvailableSpecialists = request.Specialists };
            var duration = await mediator.Send(durationQuery);

            var servicesQuery = new GetServiceListQuery { Timing = duration, AvailableSpecialists = request.Specialists };
            var result = await mediator.Send(servicesQuery);
            return Ok(result);
        }
    }
}
