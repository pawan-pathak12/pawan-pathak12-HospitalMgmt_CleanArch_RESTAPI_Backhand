using AutoMapper;
using HospitalManagment.Application.Interfaces;
using HospitalManagment.Domain.Entity;
using MediatR;

namespace HospitalManagment.Application.Features.Doctors.Commands.CreateDoctor;

public class CreateDoctorCommandHandler : IRequestHandler<CreateDoctorCommand, Doctor>
{
    private readonly IMapper _mapper;
    private readonly IDoctorRepository _repository;

    public CreateDoctorCommandHandler(IDoctorRepository repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public async Task<Doctor> Handle(CreateDoctorCommand request, CancellationToken cancellationToken)
    {
        // add logic : if already same doctor entry is their with same field than block it 
        // Map CreateDoctorDto → Doctor entity

        var doctorEntity = _mapper.Map<Doctor>(request.Doctor);

        return await _repository.AddAsync(doctorEntity);
    }
}