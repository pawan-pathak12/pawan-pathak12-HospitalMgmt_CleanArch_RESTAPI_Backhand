using HospitalManagment.Application.Common;
using HospitalManagment.Application.Features.Appointments.DTOs;
using HospitalManagment.Application.Interfaces;
using MediatR;

namespace HospitalManagment.Application.Features.LogicLab.Query.GetFutureAppointment;

public class
    GetFutureAppointmentsByDoctorQueryHandler : IRequestHandler<GetFutureAppointmentsByDoctorQuery,
    IEnumerable<AppointmentDto>>
{
    private readonly IAppointmentLogicTester _appointmentLogicTester;
    private readonly IDoctorRepository _doctorRepository;

    public GetFutureAppointmentsByDoctorQueryHandler(IAppointmentLogicTester appointmentLogicTester,
        IDoctorRepository doctorRepository)
    {
        _appointmentLogicTester = appointmentLogicTester;
        _doctorRepository = doctorRepository;
    }

    public async Task<IEnumerable<AppointmentDto>> Handle(GetFutureAppointmentsByDoctorQuery request,
        CancellationToken cancellationToken)
    {
        var doctor = await _doctorRepository.GetByIdAsync(request.DoctorId);
        if (doctor == null) throw new NotFoundException($"Their is no record of Doctor with Id {request.DoctorId}");
        var appointment = await _appointmentLogicTester.GetFutureAppointmentsByDoctorAsync(request.DoctorId);
        var result = appointment.Select(s => new AppointmentDto
        {
            DoctorId = s.DoctorId, AppointmentDate = s.AppointmentDate, EndTime = s.EndTime, Id = s.Id,
            PatientId = s.PatientId, StartTime = s.StartTime, Status = s.Status
        });
        return result;
    }
}