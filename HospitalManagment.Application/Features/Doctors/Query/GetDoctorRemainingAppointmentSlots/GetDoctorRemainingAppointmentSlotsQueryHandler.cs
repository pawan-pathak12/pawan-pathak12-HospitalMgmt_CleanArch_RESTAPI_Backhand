using HospitalManagment.Application.Interfaces;
using MediatR;
using Microsoft.Extensions.Logging;

namespace HospitalManagment.Application.Features.Doctors.Query.GetDoctorRemainingAppointmentSlots;

public class
    GetDoctorRemainingAppointmentSlotsQueryHandler : IRequestHandler<GetDoctorRemainingAppointmentSlotsQuery, int>
{
    private readonly IDoctorRepository _doctorRepository;
    private readonly ILogger<GetDoctorRemainingAppointmentSlotsQueryHandler> _logger;

    public GetDoctorRemainingAppointmentSlotsQueryHandler(IDoctorRepository doctorRepository,
        ILogger<GetDoctorRemainingAppointmentSlotsQueryHandler> logger)
    {
        _doctorRepository = doctorRepository;
        _logger = logger;
    }

    public async Task<int> Handle(GetDoctorRemainingAppointmentSlotsQuery request, CancellationToken cancellationToken)
    {
        var doctor = await _doctorRepository.GetByIdAsync(request.DoctorId);
        if (doctor == null)
            _logger.LogWarning($"Doctor with Id {request.DoctorId} not found.");
        var remaningAppoitmentSlots = await _doctorRepository.GetDoctorRemainingAppointmentSlotsAsync(request.DoctorId);

        if (remaningAppoitmentSlots <= 0)
            _logger.LogWarning($"There are no available booking slots for doctor ID {request.DoctorId} today.");
        return remaningAppoitmentSlots;
    }
}