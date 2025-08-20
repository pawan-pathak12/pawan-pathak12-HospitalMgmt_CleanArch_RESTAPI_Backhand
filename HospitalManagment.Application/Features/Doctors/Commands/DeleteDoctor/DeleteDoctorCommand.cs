using MediatR;

namespace HospitalManagment.Application.Features.Doctors.Commands.DeleteDoctor
{
    public class DeleteDoctorCommand:IRequest <Unit>
    {
        public int Id { get; set; }
    }
}
