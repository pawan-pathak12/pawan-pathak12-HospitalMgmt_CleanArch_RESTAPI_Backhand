using HospitalManagment.Application.Common;
using HospitalManagment.Application.Interfaces;
using MediatR;

namespace HospitalManagment.Application.Features.Doctors.Query.GetDoctorRemainingAppointmentSlots;

public class
    GetDoctorRemainingAppointmentSlotsQueryHandler : IRequestHandler<GetDoctorRemainingAppointmentSlotsQuery, int>
{
    private readonly IDoctorRepository _doctorRepository;

    public GetDoctorRemainingAppointmentSlotsQueryHandler(IDoctorRepository doctorRepository)
    {
        _doctorRepository = doctorRepository;
    }

    public async Task<int> Handle(GetDoctorRemainingAppointmentSlotsQuery request, CancellationToken cancellationToken)
    {
        var doctor = await _doctorRepository.GetByIdAsync(request.DoctorId);
        if (doctor == null)
            throw new NotFoundException($"Doctor with Id {request.DoctorId} not found.");

        var remaningAppoitmentSlots = await _doctorRepository.GetDoctorRemainingAppointmentSlotsAsync(request.DoctorId);

        if (remaningAppoitmentSlots <= 0)
            throw new BusinessRuleException(
                $"There are no available booking slots for doctor ID {request.DoctorId} today.");

        return remaningAppoitmentSlots;
    }
}