using MediatR;

namespace HospitalManagment.Application.Features.Doctors.Query.GetDoctorWorkingHOurPerDay;

public class GetDoctorDailyWorkingHoursQuery : IRequest<int>
{
    public int DoctorId { get; set; }
}