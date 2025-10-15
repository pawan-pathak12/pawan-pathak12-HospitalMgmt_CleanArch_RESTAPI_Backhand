using AutoMapper;
using HospitalManagment.Application.Features.Patients.DTOs;
using HospitalManagment.Application.Interfaces;
using MediatR;
using Microsoft.Extensions.Logging;

namespace HospitalManagment.Application.Features.Patients.Query.SearchPatients;

public class SearchPatientByNameQueryHandler : IRequestHandler<SearchPatientByNameQuery, IEnumerable<PatientDto>>
{
    private readonly ILogger<SearchPatientByNameQueryHandler> _logger;
    private readonly IMapper _mapper;
    private readonly IPatientRepository _patientRepository;

    public SearchPatientByNameQueryHandler(IPatientRepository patientRepository, IMapper mapper,
        ILogger<SearchPatientByNameQueryHandler> logger)
    {
        _patientRepository = patientRepository;
        _mapper = mapper;
        _logger = logger;
    }

    public async Task<IEnumerable<PatientDto>> Handle(SearchPatientByNameQuery request,
        CancellationToken cancellationToken)
    {
        var result = await _patientRepository.GetByNameAsync(request.Name);
        if (!result.Any())
            _logger.LogWarning("No matching Patient Found.");
        var patient = _mapper.Map<IEnumerable<PatientDto>>(result);
        return patient;
    }
}