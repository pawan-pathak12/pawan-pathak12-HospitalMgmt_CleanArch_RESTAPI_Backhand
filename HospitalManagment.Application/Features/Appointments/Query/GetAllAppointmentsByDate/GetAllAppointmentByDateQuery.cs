using HospitalManagment.Application.Features.Appointments.DTOs;
using MediatR;

namespace HospitalManagment.Application.Features.Appointments.Query.GetAllAppointmentsByDate;

public class GetAllAppointmentByDateQuery : IRequest<IEnumerable<AppointmentDto>>
{
    public string Type { get; set; }
    public DateTime Date { get; set; }
}