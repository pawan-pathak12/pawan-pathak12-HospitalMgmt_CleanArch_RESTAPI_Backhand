using AutoMapper;
using HospitalManagment.Application.Features.Appointments.DTOs;
using HospitalManagment.Application.Interfaces;
using MediatR;
using Microsoft.Extensions.Logging;

namespace HospitalManagment.Application.Features.Appointments.Query.GetAllAppointmentsByDate;

public class
    GetAllAppointmentByDateQueryHandler : IRequestHandler<GetAllAppointmentByDateQuery, IEnumerable<AppointmentDto>>
{
    private readonly IAppointmentRepository _appointmentRepository;
    private readonly ILogger<GetAllAppointmentByDateQueryHandler> _logger;
    private readonly IMapper _mapper;

    public GetAllAppointmentByDateQueryHandler(IAppointmentRepository appointmentRepository, IMapper mapper,
        ILogger<GetAllAppointmentByDateQueryHandler> logger)
    {
        _appointmentRepository = appointmentRepository;
        _mapper = mapper;
        _logger = logger;
    }

    public async Task<IEnumerable<AppointmentDto>> Handle(GetAllAppointmentByDateQuery request,
        CancellationToken cancellationToken)
    {
        var result = await _appointmentRepository.GetAppointmentsByDateAsync(request.Type, request.Date);
        if (!result.Any())
            _logger.LogWarning($"Their is no Appointment on {request.Date}");
        var appointments = _mapper.Map<IEnumerable<AppointmentDto>>(result);
        return appointments;
    }
}