using AutoMapper;
using HospitalManagment.Application.Features.Doctors.DTOs;
using HospitalManagment.Application.Interfaces;
using MediatR;
using Microsoft.Extensions.Logging;

namespace HospitalManagment.Application.Features.Doctors.Query.GetAllDoctors;

public class GetAllDoctorQueryHandler : IRequestHandler<GetAllDoctorQuery, IEnumerable<DoctorDto>>
{
    private readonly ILogger<GetAllDoctorQueryHandler> _logger;
    private readonly IMapper _mapper;
    private readonly IDoctorRepository _repository;

    public GetAllDoctorQueryHandler(IDoctorRepository repository, IMapper mapper,
        ILogger<GetAllDoctorQueryHandler> logger)
    {
        _repository = repository;
        _logger = logger;
        _mapper = mapper;
    }

    public async Task<IEnumerable<DoctorDto>> Handle(GetAllDoctorQuery request, CancellationToken cancellationToken)
    {
        var doctors = await _repository.GetAllAsync();
        if (doctors == null || !doctors.Any())
            _logger.LogWarning("Doctors data is blank");
        var doctorEntity = _mapper.Map<IEnumerable<DoctorDto>>(doctors);
        return doctorEntity;
    }
}