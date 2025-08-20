using HospitalManagment.Application.Features.Appointments.DTOs;
using MediatR;

namespace HospitalManagment.Application.Features.LogicLab.Query.GetFutureAppointment
{
    public class GetFutureAppointmentsByDoctorQuery:IRequest<IEnumerable<AppointmentDto>>
    {
        public int DoctorId { get; set; }
    }
}
