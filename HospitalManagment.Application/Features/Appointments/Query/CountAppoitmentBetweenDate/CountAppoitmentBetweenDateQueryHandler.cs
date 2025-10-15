using HospitalManagment.Application.Interfaces;
using MediatR;
using Microsoft.Extensions.Logging;

namespace HospitalManagment.Application.Features.Appointments.Query.CountAppoitmentBetweenDate;

public class CountAppoitmentBetweenDateQueryHandler : IRequestHandler<CountAppoitmentBetweenDateQuery, int>
{
    private readonly IAppointmentRepository _appointmentRepository;
    private readonly IDoctorRepository _doctorRepository;
    private readonly ILogger<CountAppoitmentBetweenDateQueryHandler> _logger;

    public CountAppoitmentBetweenDateQueryHandler(IAppointmentRepository appointmentRepository,
        IDoctorRepository doctorRepository, ILogger<CountAppoitmentBetweenDateQueryHandler> logger)
    {
        _appointmentRepository = appointmentRepository;
        _doctorRepository = doctorRepository;
        _logger = logger;
    }

    public async Task<int> Handle(CountAppoitmentBetweenDateQuery request, CancellationToken cancellationToken)

    {
        if (request.DoctorId != null)
        {
            var doctor = await _doctorRepository.GetByIdAsync(request.DoctorId);
            if (doctor == null)
                _logger.LogWarning($"doctor woth id {request.DoctorId} not found");
        }

        var count = await _appointmentRepository.CountAppoitmentBetweenDateAsync(request.DoctorId, request.StartDate,
            request.EndTime);
        if (count == 0)

            _logger.LogWarning($"Their is no appointments between Date {request.StartDate} and {request.EndTime}");
        return count;
    }
}