using Appointments.BL.Commands;
using Appointments.BL.Queries;
using Appointments.DataObjects;
using Appointments.DataObjects.Enums;
using Appointments.DataObjects.Requests;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Appointments.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AppointmentsController(IMediator mediator) : ControllerBase
    {
        [Authorize]
        [HttpPost("list")]
        public async Task<IActionResult> ViewCustomersAppointments([FromBody] AppointmentRequest request)
        {
            var query = new ViewCustomersAppointmentsQuery { CustomerId = request.Id, Status = request.Status };
            var result = await mediator.Send(query);
            return Ok(result);
        }

        [HttpPost("create")]
        public async Task<IActionResult> CreateAppointment ([FromBody] AppointmentData appointment)
        {
            var query = new CheckCustomersRegistrationQuery { Phone = appointment.Customer.Phone };
            var status = await mediator.Send(query);

            if(status.Status == RegistrationStatus.NewCustomer)
            {
                var command = new CreateNewCustomerCommand
                {
                    FirstName = appointment.Customer.FirstName,
                    LastName = appointment.Customer.LastName,
                    Phone = appointment.Customer.Phone,
                    Email = appointment.Customer.Email
                };
                await mediator.Send(command);
            }

            DateTime utcDate = DateTime.SpecifyKind(appointment.DateTime, DateTimeKind.Utc);

            var appointmentCommand = new CreateAppointmentCommand
            {
                CustomerId = status.CustomerId,
                SpecialistId = appointment.SpecialistId,
                ServiceId = appointment.ServiceId,
                Date = utcDate.Date,
                Time = utcDate.TimeOfDay
            };
            var result = await mediator.Send(appointmentCommand);
            return Ok(result);
        }

        [Authorize]
        [HttpPost("cancel")]
        public async Task<IActionResult> CancelAppointment ([FromBody] AppointmentRequest request)
        {
            var command = new CancelAppointmentCommand { AppointmentId = request.Id};
            var result = await mediator.Send(command);
            return Ok(result);
        }

    }
}
