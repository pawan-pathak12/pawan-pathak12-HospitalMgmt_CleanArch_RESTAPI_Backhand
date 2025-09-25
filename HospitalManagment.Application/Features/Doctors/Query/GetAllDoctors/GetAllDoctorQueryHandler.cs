using AutoMapper;
using HospitalManagment.Application.Common;
using HospitalManagment.Application.Features.Doctors.DTOs;
using HospitalManagment.Application.Interfaces;
using MediatR;

namespace HospitalManagment.Application.Features.Doctors.Query.GetAllDoctors;

public class GetAllDoctorQueryHandler : IRequestHandler<GetAllDoctorQuery, IEnumerable<DoctorDto>>
{
    private readonly IMapper _mapper;
    private readonly IDoctorRepository _repository;

    public GetAllDoctorQueryHandler(IDoctorRepository repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public async Task<IEnumerable<DoctorDto>> Handle(GetAllDoctorQuery request, CancellationToken cancellationToken)
    {
        var doctors = await _repository.GetAllAsync();
        if (doctors == null || !doctors.Any()) throw new NotFoundException("Doctors data is blank");
        var doctorEntity = _mapper.Map<IEnumerable<DoctorDto>>(doctors);
        return doctorEntity;
    }
}