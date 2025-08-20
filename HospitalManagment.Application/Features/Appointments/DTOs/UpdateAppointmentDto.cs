using HospitalManagment.Domain.Enums;
using System.ComponentModel.DataAnnotations;

namespace HospitalManagment.Application.Features.Appointments.DTOs
{
    public class UpdateAppointmentDto
    {
        public int Id { get; set; }
        [Required]
        public int PatientId { get; set; }
        [Required]
        public int DoctorId { get; set; }
        [Required]
        public DateTime AppointmentDate { get; set; }
        public AppointmentStatus Status { get; set; }
        [Required]
        [DataType(DataType.Time)]
        public TimeSpan StartTime { get; set; }
        //[Required]
        //[DataType(DataType.Time)]
        //public TimeSpan EndTime { get; set; }
    }
}
