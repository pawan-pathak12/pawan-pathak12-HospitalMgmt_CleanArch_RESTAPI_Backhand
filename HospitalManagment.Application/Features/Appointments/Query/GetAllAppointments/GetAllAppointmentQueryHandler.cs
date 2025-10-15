using AutoMapper;
using HospitalManagment.Application.Features.Appointments.DTOs;
using HospitalManagment.Application.Interfaces;
using MediatR;
using Microsoft.Extensions.Logging;

namespace HospitalManagment.Application.Features.Appointments.Query.GetAllAppointments;

public class GetAllAppointmentQueryHandler : IRequestHandler<GetAllAppointmentQuery, IEnumerable<AppointmentDto>>
{
    private readonly ILogger<GetAllAppointmentQueryHandler> _logger;
    private readonly IMapper _mapper;
    private readonly IAppointmentRepository _repository;

    public GetAllAppointmentQueryHandler(IAppointmentRepository repository, IMapper mapper,
        ILogger<GetAllAppointmentQueryHandler> logger)
    {
        _repository = repository;
        _mapper = mapper;
        _logger = logger;
    }

    public async Task<IEnumerable<AppointmentDto>> Handle(GetAllAppointmentQuery request,
        CancellationToken cancellationToken)
    {
        var appointments = await _repository.GetAllAsync();
        if (appointments == null || !appointments.Any())
            _logger.LogWarning("Their is no appointments");
        var appointmentEntity = _mapper.Map<IEnumerable<AppointmentDto>>(appointments);

        return appointmentEntity;
    }
}