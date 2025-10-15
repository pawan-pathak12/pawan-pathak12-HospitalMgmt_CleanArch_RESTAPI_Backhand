using HospitalManagment.Application.Features.Doctors.DTOs;
using HospitalManagment.Application.Interfaces;
using MediatR;
using Microsoft.Extensions.Logging;

namespace HospitalManagment.Application.Features.LogicLab.Query.GetDoctorWorkingHour;

public class GetDoctorWorkingHourQueryHandler : IRequestHandler<GetDoctorWorkingHourQuery, DoctorWorkingHourDto>
{
    private readonly IDoctorRepository _doctorRepository;
    private readonly ILogger<GetDoctorWorkingHourQueryHandler> _logger;

    public GetDoctorWorkingHourQueryHandler(IDoctorRepository doctorRepository,
        ILogger<GetDoctorWorkingHourQueryHandler> logger)
    {
        _doctorRepository = doctorRepository;
        _logger = logger;
    }

    public async Task<DoctorWorkingHourDto> Handle(GetDoctorWorkingHourQuery request, CancellationToken cancellation)
    {
        var doctor = await _doctorRepository.GetByIdAsync(request.Id);
        if (doctor == null)
            _logger.LogWarning($"Their is no doctor with id  {request.Id}.");

        var workingHourOfDoctor = await _doctorRepository.GetDoctorWorkingHourAsync(request.Id);
        if (workingHourOfDoctor == null)
            _logger.LogWarning("Doctor's working hours not found.");
        return workingHourOfDoctor;
    }
}