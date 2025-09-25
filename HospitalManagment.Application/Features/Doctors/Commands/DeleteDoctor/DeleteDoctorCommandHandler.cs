using HospitalManagment.Application.Common;
using HospitalManagment.Application.Interfaces;
using MediatR;

namespace HospitalManagment.Application.Features.Doctors.Commands.DeleteDoctor;

public class DeleteDoctorCommandHandler : IRequestHandler<DeleteDoctorCommand, Unit>
{
    private readonly IDoctorRepository _repository;

    public DeleteDoctorCommandHandler(IDoctorRepository repository)
    {
        _repository = repository;
    }

    public async Task<Unit> Handle(DeleteDoctorCommand request, CancellationToken cancellationToken)
    {
        var result = await _repository.GetByIdAsync(request.Id);
        if (result == null) throw new NotFoundException($"Delete Fail : Their is no Doctor with Id {request.Id}");
        await _repository.DeleteAsync(request.Id);
        return Unit.Value;
    }
}