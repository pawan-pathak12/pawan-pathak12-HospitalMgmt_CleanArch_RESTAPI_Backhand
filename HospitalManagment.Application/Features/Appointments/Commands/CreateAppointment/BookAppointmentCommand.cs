using HospitalManagment.Application.Features.Appointments.DTOs;
using HospitalManagment.Domain.Entity;
using MediatR;

namespace HospitalManagment.Application.Features.Appointments.Commands.CreateAppointments
{
    public class BookAppointmentCommand : IRequest<Appointment>
    {
        public CreateAppointmentDto Appointment { get; set; }
    }
}
