using HospitalManagment.Application.Interfaces;
using MediatR;
using Microsoft.Extensions.Logging;

namespace HospitalManagment.Application.Features.Doctors.Query.GetDoctorWorkingHOurPerDay;

public class GetDoctorDailyWorkingHoursQueryHandler : IRequestHandler<GetDoctorDailyWorkingHoursQuery, int>
{
    private readonly IDoctorRepository _doctorRepository;
    private readonly ILogger<GetDoctorDailyWorkingHoursQueryHandler> _logger;

    public GetDoctorDailyWorkingHoursQueryHandler(IDoctorRepository doctorRepository,
        ILogger<GetDoctorDailyWorkingHoursQueryHandler> logger)
    {
        _doctorRepository = doctorRepository;
        _logger = logger;
    }

    public async Task<int> Handle(GetDoctorDailyWorkingHoursQuery request, CancellationToken cancellationToken)
    {
        var doctor = await _doctorRepository.GetByIdAsync(request.DoctorId);
        if (doctor == null)
            _logger.LogWarning($"Doctor with Id {request.DoctorId} not found.");
        var result = await _doctorRepository.GetDoctorDailyWorkingHoursAsync(request.DoctorId);
        return result;
    }
}