namespace HospitalManagment.Domain.Entity
{
    public class Doctor
    {
        public int Id { get; set; }
        public string FullName { get; set; }
        public string Specialization { get; set; }
        public int Phone { get; set; }
        public string Email { get; set; }
        public TimeSpan AvailableStartTime { get; set; }
        public TimeSpan AvailableEndTime { get; set; }
        public bool IsActive { get; set; }


    }
}
