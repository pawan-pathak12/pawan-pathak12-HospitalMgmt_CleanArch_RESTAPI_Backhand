using AutoMapper;
using HospitalManagment.Application.Features.Doctors.DTOs;
using HospitalManagment.Application.Interfaces;
using MediatR;
using Microsoft.Extensions.Logging;

namespace HospitalManagment.Application.Features.Doctors.Query.GetDoctorById;

internal class GetDoctorByIdQueryHandler : IRequestHandler<GetDoctorByIdQuery, DoctorDto>
{
    private readonly ILogger<GetDoctorByIdQueryHandler> _logger;
    private readonly IMapper _mapper;
    private readonly IDoctorRepository _repository;

    public GetDoctorByIdQueryHandler(IDoctorRepository repository, IMapper mapper,
        ILogger<GetDoctorByIdQueryHandler> logger)
    {
        _repository = repository;
        _mapper = mapper;
        _logger = logger;
    }

    public async Task<DoctorDto> Handle(GetDoctorByIdQuery request, CancellationToken cancellationToken)
    {
        var result = await _repository.GetByIdAsync(request.Id);
        if (result == null)
            _logger.LogWarning($"Doctor with id {request.Id} not found");
        var doctor = _mapper.Map<DoctorDto>(result);

        return doctor;
    }
}