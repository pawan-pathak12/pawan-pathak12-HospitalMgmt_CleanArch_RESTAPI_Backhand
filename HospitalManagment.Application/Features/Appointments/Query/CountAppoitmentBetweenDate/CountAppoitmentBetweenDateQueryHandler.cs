using HospitalManagment.Application.Interfaces;
using MediatR;

namespace HospitalManagment.Application.Features.Appointments.Query.CountAppoitmentBetweenDate;

public class CountAppoitmentBetweenDateQueryHandler : IRequestHandler<CountAppoitmentBetweenDateQuery, int>
{
    private readonly IAppointmentRepository _appointmentRepository;
    private readonly IDoctorRepository _doctorRepository;

    public CountAppoitmentBetweenDateQueryHandler(IAppointmentRepository appointmentRepository,
        IDoctorRepository doctorRepository)
    {
        _appointmentRepository = appointmentRepository;
        _doctorRepository = doctorRepository;
    }

    public async Task<int> Handle(CountAppoitmentBetweenDateQuery request, CancellationToken cancellationToken)
    {
        if (request.DoctorId != null)
        {
            var doctor = await _doctorRepository.GetByIdAsync(request.DoctorId);
            if (doctor == null) throw new InvalidOperationException($"doctor woth id {request.DoctorId} not found");
        }

        var count = await _appointmentRepository.CountAppoitmentBetweenDateAsync(request.DoctorId, request.StartDate,
            request.EndTime);
        if (count == 0)
            throw new Exception($"Their is no appointments between Date {request.StartDate} and {request.EndTime}");
        return count;
    }
}