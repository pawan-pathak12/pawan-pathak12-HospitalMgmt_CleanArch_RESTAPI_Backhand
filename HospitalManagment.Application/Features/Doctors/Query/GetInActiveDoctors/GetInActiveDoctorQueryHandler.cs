using AutoMapper;
using HospitalManagment.Application.Features.Doctors.DTOs;
using HospitalManagment.Application.Interfaces;
using MediatR;
using Microsoft.Extensions.Logging;

namespace HospitalManagment.Application.Features.Doctors.Query.GetInActiveDoctors;

public class GetInActiveDoctorQueryHandler : IRequestHandler<GetInActiveDoctorQuery, IEnumerable<DoctorDto>>
{
    private readonly IDoctorRepository _doctorRepository;
    private readonly ILogger<GetInActiveDoctorQueryHandler> _logger;
    private readonly IMapper _mapper;

    public GetInActiveDoctorQueryHandler(IDoctorRepository doctorRepository, IMapper mapper,
        ILogger<GetInActiveDoctorQueryHandler> logger)
    {
        _doctorRepository = doctorRepository;
        _mapper = mapper;
        _logger = logger;
    }

    public async Task<IEnumerable<DoctorDto>> Handle(GetInActiveDoctorQuery request,
        CancellationToken cancellationToken)
    {
        var result = await _doctorRepository.GetInActiveDoctor();
        if (!result.Any()) _logger.LogWarning("Their is no InActive Doctor");
        var ActiveDoctors = _mapper.Map<IEnumerable<DoctorDto>>(result);
        return ActiveDoctors;
    }
}