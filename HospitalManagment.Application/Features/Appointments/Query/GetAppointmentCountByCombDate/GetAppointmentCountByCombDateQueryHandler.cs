using HospitalManagment.Application.Interfaces;
using MediatR;
using Microsoft.Extensions.Logging;

namespace HospitalManagment.Application.Features.Appointments.Query.GetAppointmentCountByCombDate;

public class GetAppointmentCountByCombDateQueryHandler : IRequestHandler<GetAppointmentCountByCombDateQuery, int>
{
    private readonly IAppointmentRepository _appointmentRepository;
    private readonly ILogger<GetAppointmentCountByCombDateQueryHandler> _logger;

    public GetAppointmentCountByCombDateQueryHandler(IAppointmentRepository appointmentRepository,
        ILogger<GetAppointmentCountByCombDateQueryHandler> logger)
    {
        _appointmentRepository = appointmentRepository;
        _logger = logger;
    }

    public async Task<int> Handle(GetAppointmentCountByCombDateQuery request, CancellationToken cancellationToken)
    {
        var result =
            await _appointmentRepository.GetAppointmentCountByDateAsync(request.Year, request.Month, request.Day,
                request.DateTime);
        if (result == -1)
            _logger.LogError("PLease select Valid combination");
        if (result == 0)
            _logger.LogWarning($" Their is {result} Appointments ");
        return result;
    }
}