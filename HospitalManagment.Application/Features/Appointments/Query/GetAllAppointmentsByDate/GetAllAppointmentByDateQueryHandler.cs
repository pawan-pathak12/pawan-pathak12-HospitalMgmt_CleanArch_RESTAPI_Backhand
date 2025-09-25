using AutoMapper;
using HospitalManagment.Application.Common;
using HospitalManagment.Application.Features.Appointments.DTOs;
using HospitalManagment.Application.Interfaces;
using MediatR;

namespace HospitalManagment.Application.Features.Appointments.Query.GetAllAppointmentsByDate;

public class
    GetAllAppointmentByDateQueryHandler : IRequestHandler<GetAllAppointmentByDateQuery, IEnumerable<AppointmentDto>>
{
    private readonly IAppointmentRepository _appointmentRepository;
    private readonly IMapper _mapper;

    public GetAllAppointmentByDateQueryHandler(IAppointmentRepository appointmentRepository, IMapper mapper)
    {
        _appointmentRepository = appointmentRepository;
        _mapper = mapper;
    }

    public async Task<IEnumerable<AppointmentDto>> Handle(GetAllAppointmentByDateQuery request,
        CancellationToken cancellationToken)
    {
        var result = await _appointmentRepository.GetAppointmentsByDateAsync(request.Type, request.Date);
        if (!result.Any()) throw new NotFoundException($"Their is no Appointment on {request.Date}");
        var appointments = _mapper.Map<IEnumerable<AppointmentDto>>(result);
        return appointments;
    }
}