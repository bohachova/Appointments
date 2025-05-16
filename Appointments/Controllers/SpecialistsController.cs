using Appointments.BL.Queries;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Appointments.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SpecialistsController(IMediator mediator) : ControllerBase
    {

        [HttpGet("all")]
        public async Task<IActionResult> ViewAllSpecialists()
        {
            var query = new ViewSpecialistsQuery();
            var result = await mediator.Send(query);
            return Ok(result);
        }

        [HttpGet("{serviceCode}")]
        public async Task<IActionResult> GetSpecialistsByServiceCode([FromRoute] int serviceCode)
        {
            var query = new ViewSpecialistsQuery { ServiceCode = serviceCode };
            var result = await mediator.Send(query);
            return Ok(result);
        }

        [HttpPost("{serviceCode}/available")]
        public async Task<IActionResult> GetAvailableSpecialists([FromRoute] int serviceCode, [FromBody] List<int> specialists)
        {
            var query = new ViewSpecialistsQuery { RequestedSpecialists = specialists };
            var result = await mediator.Send(query);
            return Ok(result);
        }
    }
}
