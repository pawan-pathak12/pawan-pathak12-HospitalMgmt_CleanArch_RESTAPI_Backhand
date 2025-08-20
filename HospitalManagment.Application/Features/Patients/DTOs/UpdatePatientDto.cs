using System.ComponentModel.DataAnnotations;

namespace HospitalManagment.Application.Features.Patients.DTOs
{
    public class UpdatePatientDto
    {
        public int Id { get; set; }
        [Required]
        public string FullName { get; set; }
        [Required]
        public int Age { get; set; }
        [Required]
        public string Gender { get; set; }
        public string Address { get; set; }
        [EmailAddress]
        public String Email { get; set; }
        [Required]
        public int PhoneNumber { get; set; }
    }
}
