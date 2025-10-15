using AutoMapper;
using HospitalManagment.Application.Features.Appointments.DTOs;
using HospitalManagment.Application.Interfaces;
using MediatR;
using Microsoft.Extensions.Logging;

namespace HospitalManagment.Application.Features.LogicLab.Query.GetFutureAppointment;

public class
    GetFutureAppointmentsByDoctorQueryHandler : IRequestHandler<GetFutureAppointmentsByDoctorQuery,
    IEnumerable<AppointmentDto>>
{
    private readonly IAppointmentLogicTester _appointmentLogicTester;
    private readonly IDoctorRepository _doctorRepository;
    private readonly ILogger<GetFutureAppointmentsByDoctorQueryHandler> _logger;
    private readonly IMapper _mapper;

    public GetFutureAppointmentsByDoctorQueryHandler(IAppointmentLogicTester appointmentLogicTester,
        IDoctorRepository doctorRepository, ILogger<GetFutureAppointmentsByDoctorQueryHandler> logger, IMapper mapper)
    {
        _appointmentLogicTester = appointmentLogicTester;
        _doctorRepository = doctorRepository;
        _logger = logger;
        _mapper = mapper;
    }

    public async Task<IEnumerable<AppointmentDto>> Handle(GetFutureAppointmentsByDoctorQuery request,
        CancellationToken cancellationToken)
    {
        var doctor = await _doctorRepository.GetByIdAsync(request.DoctorId);
        if (doctor == null)
            _logger.LogWarning($"Their is no record of Doctor with Id {request.DoctorId}");
        var appointment = await _appointmentLogicTester.GetFutureAppointmentsByDoctorAsync(request.DoctorId);
        var doctorEntity = _mapper.Map<IEnumerable<AppointmentDto>>(appointment);
        return doctorEntity;
    }
}