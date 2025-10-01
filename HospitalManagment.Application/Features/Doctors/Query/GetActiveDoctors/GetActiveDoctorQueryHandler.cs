using AutoMapper;
using HospitalManagment.Application.Features.Doctors.DTOs;
using HospitalManagment.Application.Interfaces;
using MediatR;
using Microsoft.Extensions.Logging;

namespace HospitalManagment.Application.Features.Doctors.Query.GetActiveDoctors;

public class GetActiveDoctorQueryHandler : IRequestHandler<GetActiveDoctorQuery, IEnumerable<DoctorDto>>
{
    private readonly IDoctorRepository _doctorRepository;
    private readonly ILogger<GetActiveDoctorQueryHandler> _logger;
    private readonly IMapper _mapper;

    public GetActiveDoctorQueryHandler(IDoctorRepository doctorRepository, IMapper mapper,
        ILogger<GetActiveDoctorQueryHandler> logger)
    {
        _doctorRepository = doctorRepository;
        _mapper = mapper;
        _logger = logger;
    }

    public async Task<IEnumerable<DoctorDto>> Handle(GetActiveDoctorQuery request, CancellationToken cancellationToken)
    {
        var result = await _doctorRepository.GetActiveDoctorAsync();
        if (result == null)
            _logger.LogWarning("Their is no Active Appointment ");
        var doctorEntity = _mapper.Map<IEnumerable<DoctorDto>>(result);
        return doctorEntity;
    }
}