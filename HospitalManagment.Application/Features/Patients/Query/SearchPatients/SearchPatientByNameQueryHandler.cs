using AutoMapper;
using HospitalManagment.Application.Features.Patients.DTOs;
using HospitalManagment.Application.Interfaces;
using MediatR;

namespace HospitalManagment.Application.Features.Patients.Query.SearchPatients
{
    public class SearchPatientByNameQueryHandler : IRequestHandler<SearchPatientByNameQuery, IEnumerable<PatientDto>>
    {
        private readonly IPatientRepository _patientRepository;
        private readonly IMapper _mapper;

        public SearchPatientByNameQueryHandler(IPatientRepository patientRepository, IMapper mapper)
        {
            this._patientRepository = patientRepository;
            this._mapper = mapper;
        }

        public async Task<IEnumerable<PatientDto>> Handle(SearchPatientByNameQuery request, CancellationToken cancellationToken)
        {
            var result = await _patientRepository.GetByNameAsync(request.Name);
            if (!result.Any())
            {
                throw new Exception("No matching Patient Found.");
            }
            var patient = _mapper.Map<IEnumerable<PatientDto>>(result);
            return patient;

        }
    }
}
