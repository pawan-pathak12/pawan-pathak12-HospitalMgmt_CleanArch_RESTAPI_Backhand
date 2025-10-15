using AutoMapper;
using HospitalManagment.Application.Features.Patients.DTOs;
using HospitalManagment.Application.Interfaces;
using MediatR;
using Microsoft.Extensions.Logging;

namespace HospitalManagment.Application.Features.Patients.Query.GetAllPatients;

public class GetAllPatientQueryHandler : IRequestHandler<GetAllPatientQuery, IEnumerable<PatientDto>>
{
    private readonly ILogger<GetAllPatientQueryHandler> _logger;
    private readonly IMapper _mapper;
    private readonly IPatientRepository _repository;

    public GetAllPatientQueryHandler(IPatientRepository repository, IMapper mapper,
        ILogger<GetAllPatientQueryHandler> logger)
    {
        _repository = repository;
        _mapper = mapper;
        _logger = logger;
    }

    public async Task<IEnumerable<PatientDto>> Handle(GetAllPatientQuery request, CancellationToken cancellationToken)
    {
        var patients = await _repository.GetAllAsync();
        if (patients == null || !patients.Any())
            _logger.LogWarning("Patients data not found");
        var patientData = _mapper.Map<IEnumerable<PatientDto>>(patients);
        return patientData;
    }
}