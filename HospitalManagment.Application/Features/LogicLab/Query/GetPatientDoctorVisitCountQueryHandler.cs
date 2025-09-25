using HospitalManagment.Application.Common;
using HospitalManagment.Application.Interfaces;
using MediatR;

namespace HospitalManagment.Application.Features.LogicLab.Query;

public class GetPatientDoctorVisitCountQueryHandler : IRequestHandler<GetPatientDoctorVisitCountQuery, int>
{
    private readonly IAppointmentLogicTester _appointmentLogicTester;
    private readonly IDoctorRepository _doctorRepository;
    private readonly IPatientRepository _patientRepository;

    public GetPatientDoctorVisitCountQueryHandler(IAppointmentLogicTester appointmentLogicTester,
        IDoctorRepository doctorRepository, IPatientRepository patientRepository)
    {
        _appointmentLogicTester = appointmentLogicTester;
        _doctorRepository = doctorRepository;
        _patientRepository = patientRepository;
    }

    public async Task<int> Handle(GetPatientDoctorVisitCountQuery request, CancellationToken cancellationToken)
    {
        var doctor = await _doctorRepository.GetByIdAsync(request.DoctorId);
        if (doctor == null) throw new Exception("");
        var patient = await _patientRepository.GetByIdAsync(request.PatientId);
        if (patient == null) throw new Exception("");
        var count = await _appointmentLogicTester.GetPatientDoctorVisitCountAsync(request.PatientId, request.DoctorId,
            request.Year, request.Month);
        if (count == 0) throw new NotFoundException("Their is no Appointment");

        return count;
    }
}