using HospitalManagment.Application.Common;
using HospitalManagment.Application.Interfaces;
using MediatR;

namespace HospitalManagment.Application.Features.Doctors.Query.GetDoctorBookedAppointmentCount;

public class GetDoctorBookedAppointmentCountQueryHandler : IRequestHandler<GetDoctorBookedAppointmentCountQuery, int>
{
    private readonly IDoctorRepository _doctorRepository;

    public GetDoctorBookedAppointmentCountQueryHandler(IDoctorRepository doctorRepository)
    {
        _doctorRepository = doctorRepository;
    }

    public async Task<int> Handle(GetDoctorBookedAppointmentCountQuery request, CancellationToken cancellationToken)
    {
        var doctor = await _doctorRepository.GetByIdAsync(request.DoctorId);
        if (doctor == null) throw new Exception($"Doctor with Id {request.DoctorId} not found.");

        var count = await _doctorRepository.GetDoctorBookedAppointmentCountAsync(request.DoctorId);
        if (count == 0) throw new NotFoundException($"Their is no booking for doctor Id {request.DoctorId} till now");

        return count;
    }
}