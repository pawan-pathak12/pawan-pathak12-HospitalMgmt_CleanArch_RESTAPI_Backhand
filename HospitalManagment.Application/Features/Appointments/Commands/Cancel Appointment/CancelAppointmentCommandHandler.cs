using HospitalManagment.Application.Interfaces;
using MediatR;
using Microsoft.Extensions.Logging;

namespace HospitalManagment.Application.Features.Appointments.Commands.Cancel_Appointment;

public class CancelAppointmentCommandHandler : IRequestHandler<CancelAppointmentCommand>
{
    private readonly IAppointmentRepository _appointmentRepository;
    private readonly ILogger<CancelAppointmentCommandHandler> _logger;
    private readonly IPatientRepository _patientRepository;

    public CancelAppointmentCommandHandler(IAppointmentRepository appointmentRepository,
        IPatientRepository patientRepository, ILogger<CancelAppointmentCommandHandler> logger)
    {
        _appointmentRepository = appointmentRepository;
        _patientRepository = patientRepository;
        _logger = logger;
    }

    public async Task Handle(CancelAppointmentCommand request, CancellationToken cancellationToken)
    {
        //1. check patient exists or not 
        var patient = await _patientRepository.GetByIdAsync(request.PatientId);
        if (patient == null) _logger.LogWarning($" Patient with Id {request.PatientId} doenst Exists");
        // check their is appointment or not 
        var appointment = await _appointmentRepository.GetByIdAsync(request.Id);
        if (appointment == null) _logger.LogWarning($"Appointment with Id {request.Id} not found.");
        var isCanceled = await _appointmentRepository.UpdateStatusAsync(request.Id);
        if (!isCanceled) _logger.LogWarning($"Failed to cancel appointment with Id {request.Id}.");
    }
}