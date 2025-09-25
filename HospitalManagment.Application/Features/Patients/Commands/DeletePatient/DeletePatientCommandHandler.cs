using HospitalManagment.Application.Common;
using HospitalManagment.Application.Interfaces;
using MediatR;

namespace HospitalManagment.Application.Features.Patients.Commands.DeletePatient;

public class DeletePatientCommandHandler : IRequestHandler<DeletePatientCommad, Unit>
{
    private readonly IPatientRepository _repository;

    public DeletePatientCommandHandler(IPatientRepository repository)
    {
        _repository = repository;
    }

    public async Task<Unit> Handle(DeletePatientCommad request, CancellationToken cancellationToken)
    {
        var patient = await _repository.GetByIdAsync(request.Id);
        if (patient == null) throw new NotFoundException($"Their is no Patient with Id {request.Id}");
        await _repository.DeleteAsync(request.Id);
        return Unit.Value;
    }
}