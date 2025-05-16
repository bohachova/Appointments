using Appointments.BL.Queries;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Appointments.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ScheduleController(IMediator mediator) : ControllerBase
    {
        [HttpGet("{id}")]
        public async Task<IActionResult> GetSpecialistsWorkingDays([FromRoute] int id)
        {
            var query = new ViewSpecialistsWorkingDaysQuery { SpecialistId = id };
            var result = await mediator.Send(query);
            return Ok(result);
        }
    }
}
