using AutoMapper;
using HospitalManagment.Application.Features.Appointments.DTOs;
using HospitalManagment.Application.Interfaces;
using MediatR;
using Microsoft.Extensions.Logging;

namespace HospitalManagment.Application.Features.Appointments.Query.GetDoctorCalendar;

public class GetDoctorCalendarQueryHandler : IRequestHandler<GetDoctorCalendarQuery, IEnumerable<AppointmentDto>>
{
    private readonly IAppointmentRepository _appointmentRepository;
    private readonly IDoctorRepository _doctorRepository;
    private readonly ILogger<GetDoctorCalendarQueryHandler> _logger;
    private readonly IMapper _mapper;

    public GetDoctorCalendarQueryHandler(IAppointmentRepository appointmentRepository,
        IDoctorRepository doctorRepository, IMapper mapper, ILogger<GetDoctorCalendarQueryHandler> logger)
    {
        _appointmentRepository = appointmentRepository;
        _doctorRepository = doctorRepository;
        _mapper = mapper;
        _logger = logger;
    }

    public async Task<IEnumerable<AppointmentDto>> Handle(GetDoctorCalendarQuery request,
        CancellationToken cancellationToken)
    {
        var doctor = await _doctorRepository.GetByIdAsync(request.DoctorId);
        if (doctor == null)
            _logger.LogWarning($"Doctor with id {request.DoctorId} doesnt exist.");
        var result = await _appointmentRepository.GetAppointmentsForDoctorAsync(request.DoctorId);
        if (result == null || !result.Any())
            _logger.LogWarning($"Their is no appointment for doctor Id {request.DoctorId} till now .");
        var doctorCalender = _mapper.Map<IEnumerable<AppointmentDto>>(result);

        return doctorCalender;
    }
}