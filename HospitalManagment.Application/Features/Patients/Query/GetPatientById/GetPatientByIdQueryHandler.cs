using AutoMapper;
using HospitalManagment.Application.Features.Patients.DTOs;
using HospitalManagment.Application.Interfaces;
using MediatR;
using Microsoft.Extensions.Logging;

namespace HospitalManagment.Application.Features.Patients.Query.GetPatientById;

public class GetPatientByIdQueryHandler : IRequestHandler<GetPatientByIdQuery, PatientDto>
{
    private readonly ILogger<GetPatientByIdQueryHandler> _logger;
    private readonly IMapper _mapper;
    private readonly IPatientRepository _repository;

    public GetPatientByIdQueryHandler(IPatientRepository repository, IMapper mapper,
        ILogger<GetPatientByIdQueryHandler> logger)
    {
        _repository = repository;
        _mapper = mapper;
        _logger = logger;
    }

    public async Task<PatientDto> Handle(GetPatientByIdQuery request, CancellationToken cancellationToken)
    {
        var patient = await _repository.GetByIdAsync(request.Id);
        if (patient == null)
            _logger.LogWarning($"Patient with id {request.Id} not found");
        var patientData = _mapper.Map<PatientDto>(patient);
        return patientData;
    }
}