using MediatR;

namespace HospitalManagment.Application.Features.Doctors.Query.GetDoctorRemainingAppointmentSlots;

public class GetDoctorRemainingAppointmentSlotsQuery : IRequest<int>
{
    public int DoctorId { get; set; }
}