using HospitalManagment.Application.Features.Patients.DTOs;
using MediatR;

namespace HospitalManagment.Application.Features.Patients.Query.SearchPatients
{
    public class SearchPatientByNameQuery :IRequest<IEnumerable<PatientDto>>
    {
        public string Name { get; set; }
    }
}
