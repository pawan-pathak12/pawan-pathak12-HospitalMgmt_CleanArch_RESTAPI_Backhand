using HospitalManagment.Application.Common;
using HospitalManagment.Application.Interfaces;
using MediatR;

namespace HospitalManagment.Application.Features.Doctors.Query.GetDoctorWorkingHOurPerDay;

public class GetDoctorDailyWorkingHoursQueryHandler : IRequestHandler<GetDoctorDailyWorkingHoursQuery, int>
{
    private readonly IDoctorRepository _doctorRepository;

    public GetDoctorDailyWorkingHoursQueryHandler(IDoctorRepository doctorRepository)
    {
        _doctorRepository = doctorRepository;
    }

    public async Task<int> Handle(GetDoctorDailyWorkingHoursQuery request, CancellationToken cancellationToken)
    {
        var doctor = await _doctorRepository.GetByIdAsync(request.DoctorId);
        if (doctor == null) throw new NotFoundException($"Doctor with Id {request.DoctorId} not found.");
        var result = await _doctorRepository.GetDoctorDailyWorkingHoursAsync(request.DoctorId);
        return result;
    }
}