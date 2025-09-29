using MediatR;

namespace HospitalManagment.Application.Features.Appointments.Query.GetDoctorAvailableAppointmentTimes;

public class GetDoctorAvailableAppointmentTimesQuery : IRequest
{
    public int DoctorId { get; set; }
}