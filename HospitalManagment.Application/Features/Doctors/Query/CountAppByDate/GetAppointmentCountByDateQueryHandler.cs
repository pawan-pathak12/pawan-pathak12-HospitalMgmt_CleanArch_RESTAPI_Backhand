using HospitalManagment.Application.Interfaces;
using MediatR;

namespace HospitalManagment.Application.Features.Doctors.Query.CountAppByDate;

public class GetAppointmentCountByDateQueryHandler : IRequestHandler<GetAppointmentCountByDateQuery, int>
{
    private readonly IDoctorRepository _doctorRepository;

    public GetAppointmentCountByDateQueryHandler(IDoctorRepository doctorRepository)
    {
        _doctorRepository = doctorRepository;
    }

    public async Task<int> Handle(GetAppointmentCountByDateQuery request, CancellationToken cancellationToken)
    {
        var doctor = await _doctorRepository.GetByIdAsync(request.DoctorId);
        if (doctor == null) throw new InvalidOperationException($"Doctor with id {request.DoctorId} not Found");
        var result = await _doctorRepository.GetDoctorAppointmentCountByDateAsync(request.Type, request.DoctorId);
        if (result == 0)
            throw new Exception(
                $"No appointments found for doctor ID {request.DoctorId} on  ({request.Type}");
        return result;
    }
}