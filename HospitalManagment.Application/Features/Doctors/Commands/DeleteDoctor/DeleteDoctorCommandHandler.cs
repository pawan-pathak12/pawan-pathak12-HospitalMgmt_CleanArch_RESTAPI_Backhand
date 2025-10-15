using HospitalManagment.Application.Interfaces;
using MediatR;
using Microsoft.Extensions.Logging;

namespace HospitalManagment.Application.Features.Doctors.Commands.DeleteDoctor;

public class DeleteDoctorCommandHandler : IRequestHandler<DeleteDoctorCommand, Unit>
{
    private readonly ILogger<DeleteDoctorCommandHandler> _logger;
    private readonly IDoctorRepository _repository;

    public DeleteDoctorCommandHandler(IDoctorRepository repository, ILogger<DeleteDoctorCommandHandler> logger)
    {
        _repository = repository;
        _logger = logger;
    }

    public async Task<Unit> Handle(DeleteDoctorCommand request, CancellationToken cancellationToken)
    {
        var result = await _repository.GetByIdAsync(request.Id);
        if (result == null)
            _logger.LogWarning($"Delete Fail : Their is no Doctor with Id {request.Id}");
        await _repository.DeleteAsync(request.Id);
        return Unit.Value;
    }
}