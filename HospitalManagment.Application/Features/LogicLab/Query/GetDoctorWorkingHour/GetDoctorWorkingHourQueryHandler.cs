using HospitalManagment.Application.Common;
using HospitalManagment.Application.Features.Doctors.DTOs;
using HospitalManagment.Application.Interfaces;
using MediatR;

namespace HospitalManagment.Application.Features.LogicLab.Query.GetDoctorWorkingHour;

public class GetDoctorWorkingHourQueryHandler : IRequestHandler<GetDoctorWorkingHourQuery, DoctorWorkingHourDto>
{
    private readonly IDoctorRepository _doctorRepository;

    public GetDoctorWorkingHourQueryHandler(IDoctorRepository doctorRepository)
    {
        _doctorRepository = doctorRepository;
    }

    public async Task<DoctorWorkingHourDto> Handle(GetDoctorWorkingHourQuery request, CancellationToken cancellation)
    {
        var doctor = await _doctorRepository.GetByIdAsync(request.Id);
        if (doctor == null) throw new NotFoundException($"Their is no doctor with id  {request.Id}.");
        var workingHourOfDoctor = await _doctorRepository.GetDoctorWorkingHourAsync(request.Id);
        if (workingHourOfDoctor == null) throw new NotFoundException("Doctor's working hours not found.");
        return workingHourOfDoctor;
    }
}