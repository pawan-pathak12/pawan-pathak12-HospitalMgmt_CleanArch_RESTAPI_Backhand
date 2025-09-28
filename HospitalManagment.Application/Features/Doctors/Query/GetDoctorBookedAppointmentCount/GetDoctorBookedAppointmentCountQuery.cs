using MediatR;

namespace HospitalManagment.Application.Features.Doctors.Query.GetDoctorBookedAppointmentCount;

public class GetDoctorBookedAppointmentCountQuery : IRequest<int>
{
    public int DoctorId { get; set; }
}