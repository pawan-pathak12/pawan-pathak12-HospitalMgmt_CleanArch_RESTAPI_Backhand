using HospitalManagment.Application.Features.Appointments.DTOs;
using MediatR;

namespace HospitalManagment.Application.Features.Appointments.Query.GetAllAppointments
{
    public class GetAllAppointmentQuery : IRequest<IEnumerable<AppointmentDto>>
    {

    }
}
