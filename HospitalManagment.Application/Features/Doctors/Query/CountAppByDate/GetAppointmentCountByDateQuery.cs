using MediatR;

namespace HospitalManagment.Application.Features.Doctors.Query.CountAppByDate;

public class GetAppointmentCountByDateQuery : IRequest<int>
{
    public int DoctorId { get; set; }
    public string Type { get; set; }
}