using MediatR;

namespace HospitalManagment.Application.Features.Appointments.Query.GetAppointmentCountByCombDate;

public class GetAppointmentCountByCombDateQuery : IRequest<int>
{
    public int? Year { get; set; }
    public int? Month { get; set; }
    public int? Day { get; set; }
    public DateTime? DateTime { get; set; }
}