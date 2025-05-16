using Appointments.BL.Commands;
using Appointments.BL.Queries;
using Appointments.DataObjects.Requests;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Appointments.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class ProfilesController(IMediator mediator) : ControllerBase
    {
        [HttpGet("profile")]
        public async Task<IActionResult> ViewCustomerAccount()
        {
            var userIdFromToken = int.Parse(User.FindFirst("UserId")?.Value);
            var query = new ViewCustomerAccountQuery { CustomerId = userIdFromToken };
            var result = await mediator.Send(query);
            return Ok(result);
            
        }

        [HttpPost("edit")]
        public async Task<IActionResult> EditCustomerProfile([FromBody] EditCustomerProfileRequest request)
        {
            var userIdFromToken = int.Parse(User.FindFirst("UserId")?.Value);
            if(userIdFromToken != request.CustomerId)
            {
                return Forbid();
            }
            else
            {
                var command = new EditCustomerProfileCommand { CustomerId = request.CustomerId, PropertyName = request.PropertyName, NewValue = request.NewValue };
                var result = await mediator.Send(command);
                return Ok(result);
            }
        }
    }
}
