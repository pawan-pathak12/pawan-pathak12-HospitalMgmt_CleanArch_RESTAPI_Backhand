using HospitalManagment.Application.Interfaces;
using MediatR;
using Microsoft.Extensions.Logging;

namespace HospitalManagment.Application.Features.Doctors.Query.CountAppByDate;

public class GetAppointmentCountByDateQueryHandler : IRequestHandler<GetAppointmentCountByDateQuery, int>
{
    private readonly IDoctorRepository _doctorRepository;
    private readonly ILogger<GetAppointmentCountByDateQueryHandler> _logger;

    public GetAppointmentCountByDateQueryHandler(IDoctorRepository doctorRepository,
        ILogger<GetAppointmentCountByDateQueryHandler> logger)
    {
        _doctorRepository = doctorRepository;
        _logger = logger;
    }

    public async Task<int> Handle(GetAppointmentCountByDateQuery request, CancellationToken cancellationToken)
    {
        var doctor = await _doctorRepository.GetByIdAsync(request.DoctorId);
        if (doctor == null)
            _logger.LogWarning($"Doctor with id {request.DoctorId} not Found");
        var result = await _doctorRepository.GetDoctorAppointmentCountByDateAsync(request.Type, request.DoctorId);
        if (result == 0)
            _logger.LogWarning($"No appointments found for doctor ID {request.DoctorId} on  ({request.Type}");
        return result;
    }
}