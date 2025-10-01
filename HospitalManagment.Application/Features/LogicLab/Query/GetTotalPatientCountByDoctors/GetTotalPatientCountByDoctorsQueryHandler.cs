using HospitalManagment.Application.Interfaces;
using MediatR;
using Microsoft.Extensions.Logging;

namespace HospitalManagment.Application.Features.LogicLab.Query.GetTotalPatientCountByDoctors;

public class GetTotalPatientCountByDoctorsQueryHandler : IRequestHandler<GetTotalPatientCountByDoctorsQuery, int>
{
    private readonly IAppointmentLogicTester _appointmentLogicTester;
    private readonly IDoctorRepository _doctorRepository;
    private readonly ILogger<GetTotalPatientCountByDoctorsQueryHandler> _logger;

    public GetTotalPatientCountByDoctorsQueryHandler(IAppointmentLogicTester appointmentLogicTester,
        IDoctorRepository doctorRepository, ILogger<GetTotalPatientCountByDoctorsQueryHandler> logger)
    {
        _appointmentLogicTester = appointmentLogicTester;
        _doctorRepository = doctorRepository;
        _logger = logger;
    }

    public async Task<int> Handle(GetTotalPatientCountByDoctorsQuery request, CancellationToken cancellationToken)
    {
        var doctor = await _doctorRepository.GetByIdAsync(request.DoctorId);
        if (doctor == null)
            _logger.LogWarning($"Their is no Doctor with Id {request.DoctorId}");
        var result = await _appointmentLogicTester.GetDoctorDailyAppointmentCountAsync(request.DoctorId);
        return result;
    }
}