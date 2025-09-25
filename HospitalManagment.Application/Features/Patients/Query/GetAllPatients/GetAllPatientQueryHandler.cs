using AutoMapper;
using HospitalManagment.Application.Common;
using HospitalManagment.Application.Features.Patients.DTOs;
using HospitalManagment.Application.Interfaces;
using MediatR;

namespace HospitalManagment.Application.Features.Patients.Query.GetAllPatients;

public class GetAllPatientQueryHandler : IRequestHandler<GetAllPatientQuery, IEnumerable<PatientDto>>
{
    private readonly IMapper _mapper;
    private readonly IPatientRepository _repository;

    public GetAllPatientQueryHandler(IPatientRepository repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public async Task<IEnumerable<PatientDto>> Handle(GetAllPatientQuery request, CancellationToken cancellationToken)
    {
        var patients = await _repository.GetAllAsync();
        if (patients == null || !patients.Any()) throw new NotFoundException("Patients data not found ");
        var patientData = _mapper.Map<IEnumerable<PatientDto>>(patients);
        return patientData;
    }
}