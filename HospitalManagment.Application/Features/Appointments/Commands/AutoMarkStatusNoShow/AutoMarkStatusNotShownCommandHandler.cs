using HospitalManagment.Application.Interfaces;
using MediatR;
using Microsoft.Extensions.Logging;

namespace HospitalManagment.Application.Features.Appointments.Commands.AutoMarkStatusNoShow;

public class AutoMarkStatusNotShownCommandHandler : IRequestHandler<AutoMarkStatusNotShownCommand>
{
    private readonly IAppointmentRepository _appointmentRepository;
    private readonly ILogger<AutoMarkStatusNotShownCommandHandler> _logger;

    public AutoMarkStatusNotShownCommandHandler(IAppointmentRepository appointmentRepository,
        ILogger<AutoMarkStatusNotShownCommandHandler> logger)
    {
        _appointmentRepository = appointmentRepository;
        _logger = logger;
    }

    public async Task Handle(AutoMarkStatusNotShownCommand request, CancellationToken cancellationToken)
    {
        var shownAppointment = await _appointmentRepository.GetPastScheduledAppointmentsAsync();
        if (!shownAppointment.Any()) _logger.LogWarning("There are no past scheduled appointments.");
        await _appointmentRepository.MarkAppointmentsAsNotShownAsync();
    }
}