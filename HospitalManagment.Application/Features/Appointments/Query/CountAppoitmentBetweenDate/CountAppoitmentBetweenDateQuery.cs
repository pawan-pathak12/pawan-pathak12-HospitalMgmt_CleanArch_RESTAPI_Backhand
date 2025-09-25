using MediatR;

namespace HospitalManagment.Application.Features.Appointments.Query.CountAppoitmentBetweenDate;

public class CountAppoitmentBetweenDateQuery : IRequest<int>
{
    public int? DoctorId { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndTime { get; set; }
}