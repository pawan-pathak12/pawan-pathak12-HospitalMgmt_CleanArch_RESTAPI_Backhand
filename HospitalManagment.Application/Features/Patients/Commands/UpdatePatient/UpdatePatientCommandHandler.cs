using AutoMapper;
using HospitalManagment.Application.Common;
using HospitalManagment.Application.Interfaces;
using HospitalManagment.Domain.Entity;
using MediatR;

namespace HospitalManagment.Application.Features.Patients.Commands.UpdatePatient;

public class UpdatePatientCommandHandler : IRequestHandler<UpdatePatientCommand, Unit>
{
    private readonly IMapper _mapper;
    private readonly IPatientRepository _repository;

    public UpdatePatientCommandHandler(IPatientRepository repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public async Task<Unit> Handle(UpdatePatientCommand request, CancellationToken cancellationToken)
    {
        var result = await _repository.GetByIdAsync(request.Patient.Id);
        if (result == null) throw new NotFoundException($"Their is no Patient with Id {request.Patient.Id}");

        var patient = _mapper.Map<Patient>(request.Patient);
        await _repository.UpdateAsync(request.Patient.Id, patient);
        return Unit.Value;
    }
}