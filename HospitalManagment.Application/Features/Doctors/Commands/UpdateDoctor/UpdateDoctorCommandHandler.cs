using AutoMapper;
using HospitalManagment.Application.Common;
using HospitalManagment.Application.Interfaces;
using HospitalManagment.Domain.Entity;
using MediatR;

namespace HospitalManagment.Application.Features.Doctors.Commands.UpdateDoctor;

internal class UpdateDoctorCommandHandler : IRequestHandler<UpdateDoctorCommand, Unit>
{
    private readonly IMapper _mapper;
    private readonly IDoctorRepository _repository;

    public UpdateDoctorCommandHandler(IDoctorRepository repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public async Task<Unit> Handle(UpdateDoctorCommand request, CancellationToken cancellationToken)
    {
        var result = await _repository.GetByIdAsync(request.Doctor.Id);
        if (result == null)
            throw new NotFoundException($"Update Failed : Their is no Doctor with Id {request.Doctor.Id}");
        var docotorEntity = _mapper.Map<Doctor>(request.Doctor);
        await _repository.UpdateAsync(request.Doctor.Id, docotorEntity);
        return Unit.Value;
    }
}