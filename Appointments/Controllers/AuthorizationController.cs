using Appointments.BL.Commands;
using Appointments.BL.Queries;
using Appointments.DataObjects;
using Appointments.DataObjects.Requests;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Appointments.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthorizationController(IMediator mediator) : ControllerBase
    {
        [HttpPost("status")]
        public async Task<IActionResult> CheckRegistrationStatus([FromBody] RegistrationStatusRequest request)
        {
            var query = new CheckCustomersRegistrationQuery { Phone = request.Phone };
            var result = await mediator.Send(query);
            return Ok(result);
        }

        [HttpPost("registration")]
        public async Task<IActionResult> CreateNewCustomer([FromBody] Customer customer)
        {
            var command = new CreateNewCustomerCommand
            {
                FirstName = customer.FirstName,
                LastName = customer.LastName,
                Email = customer.Email,
                Phone = customer.Phone,
                Password = customer.Password
            };
            var result = await mediator.Send(command);
            return Ok(result);
        }

        [HttpPost("authorize")]
        public async Task<IActionResult> Authorize([FromBody] AuthRequest request)
        {
            var command = new CustomerAuthorizationCommand { Phone = request.Phone, Password = request.Password };
            var result = await mediator.Send(command);
            return Ok(result);
        }

        [HttpPost("refresh")]
        public async Task<IActionResult> RefreshTokens ([FromBody] RefreshTokenRequest request)
        {
            var command = new RefreshTokensCommand {UserId = request.UserId, RefreshToken = request.RefreshToken};
            var result = await mediator.Send(command);
            return Ok(result);
        }
    }
}