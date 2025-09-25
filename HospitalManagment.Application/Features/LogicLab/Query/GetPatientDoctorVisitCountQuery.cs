using MediatR;

namespace HospitalManagment.Application.Features.LogicLab.Query;

public class GetPatientDoctorVisitCountQuery : IRequest<int>
{
    public int DoctorId { get; set; }
    public int PatientId { get; set; }
    public int? Year { get; set; }
    public int? Month { get; set; }
}