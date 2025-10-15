using HospitalManagment.Application.Interfaces;
using MediatR;
using Microsoft.Extensions.Logging;

namespace HospitalManagment.Application.Features.LogicLab.Query;

public class GetPatientDoctorVisitCountQueryHandler : IRequestHandler<GetPatientDoctorVisitCountQuery, int>
{
    private readonly IAppointmentLogicTester _appointmentLogicTester;
    private readonly IDoctorRepository _doctorRepository;
    private readonly ILogger<GetPatientDoctorVisitCountQueryHandler> _logger;
    private readonly IPatientRepository _patientRepository;

    public GetPatientDoctorVisitCountQueryHandler(IAppointmentLogicTester appointmentLogicTester,
        IDoctorRepository doctorRepository, IPatientRepository patientRepository,
        ILogger<GetPatientDoctorVisitCountQueryHandler> logger)
    {
        _appointmentLogicTester = appointmentLogicTester;
        _doctorRepository = doctorRepository;
        _patientRepository = patientRepository;
        _logger = logger;
    }

    public async Task<int> Handle(GetPatientDoctorVisitCountQuery request, CancellationToken cancellationToken)
    {
        var doctor = await _doctorRepository.GetByIdAsync(request.DoctorId);
        if (doctor == null)
            _logger.LogWarning($"Their is no Doctor with Id {request.DoctorId}");
        var patient = await _patientRepository.GetByIdAsync(request.PatientId);
        if (patient == null)
            _logger.LogWarning($"Their is no Patient with Id {request.PatientId}");
        var count = await _appointmentLogicTester.GetPatientDoctorVisitCountAsync(request.PatientId, request.DoctorId,
            request.Year, request.Month);
        if (count == 0)
            _logger.LogWarning("Their is no Appointment");
        return count;
    }
}