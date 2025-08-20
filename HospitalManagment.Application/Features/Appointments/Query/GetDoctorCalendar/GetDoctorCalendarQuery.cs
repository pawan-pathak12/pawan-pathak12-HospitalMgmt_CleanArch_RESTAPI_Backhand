using HospitalManagment.Application.Features.Appointments.DTOs;
using MediatR;

namespace HospitalManagment.Application.Features.Appointments.Query.GetDoctorCalendar
{
    public class GetDoctorCalendarQuery: IRequest<IEnumerable<AppointmentDto>>
    {
        public int DoctorId { get; set; }
    }
}
