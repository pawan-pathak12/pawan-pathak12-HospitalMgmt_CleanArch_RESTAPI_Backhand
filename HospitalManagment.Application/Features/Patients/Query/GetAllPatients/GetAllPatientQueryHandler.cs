using AutoMapper;
using HospitalManagment.Application.Features.Patients.DTOs;
using HospitalManagment.Application.Interfaces;
using MediatR;

namespace HospitalManagment.Application.Features.Patients.Query.GetAllPatients
{
    public class GetAllPatientQueryHandler : IRequestHandler<GetAllPatientQuery, IEnumerable<PatientDto>>
    {
        private readonly IPatientRepository _repository;
        private readonly IMapper _mapper;

        public GetAllPatientQueryHandler(IPatientRepository repository , IMapper mapper)
        {
            this._repository = repository;
            this._mapper = mapper;
        }
        public async Task<IEnumerable<PatientDto>> Handle(GetAllPatientQuery request, CancellationToken cancellationToken)
        {
            var patients = await _repository.GetAllAsync();
            if (patients== null || !patients.Any())
            {
                throw new Exception("Patients data not found ");
            }
            var patientData = _mapper.Map<IEnumerable<PatientDto>>(patients);            
            return patientData;
        }
    }
}
