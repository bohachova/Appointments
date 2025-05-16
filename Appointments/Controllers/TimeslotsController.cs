using Appointments.BL.Queries;
using Appointments.DataObjects.Requests;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Appointments.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TimeslotsController(IMediator mediator) : ControllerBase
    {
        [HttpPost("all")]
        public async Task<IActionResult> GetAllTimeSlots([FromBody] DateTime date)
        {
            var query = new GetAllAvailableTimeSlotsQuery { Date = date, DayOfWeek = date.DayOfWeek };
            var result = await mediator.Send(query);
            return Ok(result);
        }

        [HttpPost("requested")]
        public async Task<IActionResult> GetRequestedTimeSlots([FromBody] TimeSlotsRequest request)
        {
            var query = new ViewSpecialistsTimeSlotsQuery { Date = request.Date, DayOfWeek = (int)request.Date.DayOfWeek, SpecialistId = request.SpecialistId, IntervalsCount = request.IntervalsCount, ServiceSelected = request.ServiceSelected, ServiceId = request.ServiceId};
            var result = await mediator.Send(query);
            return Ok(result);
        }
    }
}
