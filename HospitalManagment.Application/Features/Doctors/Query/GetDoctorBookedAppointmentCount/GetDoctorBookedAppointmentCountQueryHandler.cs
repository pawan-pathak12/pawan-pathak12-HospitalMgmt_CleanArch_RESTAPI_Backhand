using HospitalManagment.Application.Interfaces;
using MediatR;
using Microsoft.Extensions.Logging;

namespace HospitalManagment.Application.Features.Doctors.Query.GetDoctorBookedAppointmentCount;

public class GetDoctorBookedAppointmentCountQueryHandler : IRequestHandler<GetDoctorBookedAppointmentCountQuery, int>
{
    private readonly IDoctorRepository _doctorRepository;
    private readonly ILogger<GetDoctorBookedAppointmentCountQueryHandler> _logger;

    public GetDoctorBookedAppointmentCountQueryHandler(IDoctorRepository doctorRepository,
        ILogger<GetDoctorBookedAppointmentCountQueryHandler> logger)
    {
        _doctorRepository = doctorRepository;
        _logger = logger;
    }

    public async Task<int> Handle(GetDoctorBookedAppointmentCountQuery request, CancellationToken cancellationToken)
    {
        var doctor = await _doctorRepository.GetByIdAsync(request.DoctorId);
        if (doctor == null)
            _logger.LogWarning($"Doctor with Id {request.DoctorId} not found.");

        var count = await _doctorRepository.GetDoctorBookedAppointmentCountAsync(request.DoctorId);
        if (count == 0)
            _logger.LogWarning($"Their is no booking for doctor Id {request.DoctorId} till now");
        return count;
    }
}