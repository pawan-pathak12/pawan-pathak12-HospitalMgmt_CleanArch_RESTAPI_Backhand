using HospitalManagment.Domain.Enums;

namespace HospitalManagment.Application.Features.Appointments.DTOs
{
    public class SlotStatusDto
    {
        public TimeSpan StartTime { get; set; }
        public TimeSpan EndTime { get; set; }
        public AppointmentStatus Status { get; set; } // Enum: Available, Booked
    }
}
