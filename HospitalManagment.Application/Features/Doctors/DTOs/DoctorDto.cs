using System.ComponentModel.DataAnnotations;

namespace HospitalManagment.Application.Features.Doctors.DTOs
{
    public class DoctorDto
    {
        public int Id { get; set; }
        [Required]
        public string FullName { get; set; }
        public string Specialization { get; set; }
        public int Phone { get; set; }
        [EmailAddress]
        public string Email { get; set; }
        [Required]

        public TimeSpan AvailableStartTime { get; set; }
        [Required]
        public TimeSpan AvailableEndTime { get; set; }
        public bool IsActive { get; set; }
    }
}
