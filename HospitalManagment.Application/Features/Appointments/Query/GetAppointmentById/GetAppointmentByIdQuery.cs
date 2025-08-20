using HospitalManagment.Application.Features.Appointments.DTOs;
using MediatR;

namespace HospitalManagment.Application.Features.Appointments.Query.GetAppointmentById
{
    public class GetAppointmentByIdQuery:IRequest<AppointmentDto>
    {
        public int Id { get; set; }
    }
}
