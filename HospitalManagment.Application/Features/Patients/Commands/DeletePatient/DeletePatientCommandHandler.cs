using HospitalManagment.Application.Interfaces;
using MediatR;
using Microsoft.Extensions.Logging;

namespace HospitalManagment.Application.Features.Patients.Commands.DeletePatient;

public class DeletePatientCommandHandler : IRequestHandler<DeletePatientCommad, Unit>
{
    private readonly ILogger<DeletePatientCommandHandler> _logger;
    private readonly IPatientRepository _repository;

    public DeletePatientCommandHandler(IPatientRepository repository, ILogger<DeletePatientCommandHandler> logger)
    {
        _repository = repository;
        _logger = logger;
    }

    public async Task<Unit> Handle(DeletePatientCommad request, CancellationToken cancellationToken)
    {
        var patient = await _repository.GetByIdAsync(request.Id);
        if (patient == null)
            _logger.LogWarning($"Their is no Patient with Id {request.Id}");
        await _repository.DeleteAsync(request.Id);
        return Unit.Value;
    }
}