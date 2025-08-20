namespace HospitalManagment.Domain.Entity
{
    public class Patient
    {
        public int Id { get; set; }
        public string FullName { get; set; }
        public int Age { get; set; }
        public string Gender { get; set; }
        public string Address { get; set; }
        public String Email { get; set; }
        public int  PhoneNumber { get; set; }   

    }
}
