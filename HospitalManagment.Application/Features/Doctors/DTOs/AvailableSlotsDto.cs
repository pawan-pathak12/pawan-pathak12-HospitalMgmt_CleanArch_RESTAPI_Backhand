namespace HospitalManagment.Application.Features.Doctors.DTOs;

public class AvailableSlotsDto
{
    public int TotalSlots { get; set; }
    public List<TimeSpan> AvailableStartTime { get; set; }
}