using AutoMapper;
using HospitalManagment.Application.Features.Appointments.DTOs;
using HospitalManagment.Application.Interfaces;
using MediatR;
using Microsoft.Extensions.Logging;

namespace HospitalManagment.Application.Features.Appointments.Query.GetAppointmentById;

public class GetAppointmentByIdQueryHandler : IRequestHandler<GetAppointmentByIdQuery, AppointmentDto>
{
    private readonly ILogger<GetAppointmentByIdQueryHandler> _logger;
    private readonly IMapper _mapper;
    private readonly IAppointmentRepository _repository;

    public GetAppointmentByIdQueryHandler(IAppointmentRepository repository, IMapper mapper,
        ILogger<GetAppointmentByIdQueryHandler> logger)
    {
        _repository = repository;
        _mapper = mapper;
        _logger = logger;
    }

    public async Task<AppointmentDto> Handle(GetAppointmentByIdQuery request, CancellationToken cancellation)
    {
        var appointment = await _repository.GetByIdAsync(request.Id);
        if (appointment == null)
            _logger.LogWarning($"Their is no appointment with id {request.Id}.");
        var appointmentEntity = _mapper.Map<AppointmentDto>(appointment);

        return appointmentEntity;
    }
}