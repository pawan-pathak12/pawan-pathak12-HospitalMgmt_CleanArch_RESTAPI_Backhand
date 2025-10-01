using AutoMapper;
using HospitalManagment.Application.Interfaces;
using HospitalManagment.Domain.Entity;
using MediatR;
using Microsoft.Extensions.Logging;

namespace HospitalManagment.Application.Features.Patients.Commands.UpdatePatient;

public class UpdatePatientCommandHandler : IRequestHandler<UpdatePatientCommand, Unit>
{
    private readonly ILogger<UpdatePatientCommandHandler> _logger;
    private readonly IMapper _mapper;
    private readonly IPatientRepository _repository;

    public UpdatePatientCommandHandler(IPatientRepository repository, IMapper mapper,
        ILogger<UpdatePatientCommandHandler> logger)
    {
        _repository = repository;
        _mapper = mapper;
        _logger = logger;
    }

    public async Task<Unit> Handle(UpdatePatientCommand request, CancellationToken cancellationToken)
    {
        var result = await _repository.GetByIdAsync(request.Patient.Id);
        if (result == null)
            _logger.LogWarning($"Their is no Patient with Id {request.Patient.Id}");
        var patient = _mapper.Map<Patient>(request.Patient);
        await _repository.UpdateAsync(request.Patient.Id, patient);
        return Unit.Value;
    }
}