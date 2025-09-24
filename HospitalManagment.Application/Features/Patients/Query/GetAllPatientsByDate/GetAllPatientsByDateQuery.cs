using HospitalManagment.Application.Features.Patients.DTOs;
using MediatR;

namespace HospitalManagment.Application.Features.Patients.Query.GetAllPatientsByDate;

public class GetAllPatientsByDateQuery : IRequest<IEnumerable<PatientDto>>
{
    public string Type { get; set; }
    public DateTime Date { get; set; }
}