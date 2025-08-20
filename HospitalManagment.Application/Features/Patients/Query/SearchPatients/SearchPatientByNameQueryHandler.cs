using HospitalManagment.Application.Features.Patients.DTOs;
using HospitalManagment.Application.Interfaces;
using MediatR;

namespace HospitalManagment.Application.Features.Patients.Query.SearchPatients
{
    public class SearchPatientByNameQueryHandler : IRequestHandler<SearchPatientByNameQuery, IEnumerable<PatientDto>>
    {
        private readonly IPatientRepository _patientRepository;

        public SearchPatientByNameQueryHandler(IPatientRepository patientRepository)
        {
            this._patientRepository = patientRepository;
        }

        public async Task<IEnumerable<PatientDto>> Handle(SearchPatientByNameQuery request, CancellationToken cancellationToken)
        {
            var result = await _patientRepository.GetByNameAsync(request.Name);
            if (!result.Any())
            {
                throw new Exception("No matching Patient Found.");  
            }
            var patient = result.Select(s => new PatientDto
            {
                Address = s.Address,
                Age = s.Age,
                Email = s.Email,
                FullName = s.FullName,
                Gender = s.Gender,
                Id = s.Id,
                PhoneNumber = s.PhoneNumber
            });
            return patient;

        }
    }
}
