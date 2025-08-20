using MediatR;

namespace HospitalManagment.Application.Features.Appointments.Commands.Cancel_Appointment
{
    public class CancelAppointmentCommand : IRequest
    {
        public int Id { get; set; }
        public int PatientId { get; set; }
    }
}
