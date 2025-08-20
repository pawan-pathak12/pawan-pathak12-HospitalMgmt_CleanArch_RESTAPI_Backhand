using HospitalManagment.Application.Features.Patients.DTOs;
using MediatR;

namespace HospitalManagment.Application.Features.Patients.Query.GetAllPatients
{
    public class GetAllPatientQuery : IRequest<IEnumerable<PatientDto>>
    {

    }
}
