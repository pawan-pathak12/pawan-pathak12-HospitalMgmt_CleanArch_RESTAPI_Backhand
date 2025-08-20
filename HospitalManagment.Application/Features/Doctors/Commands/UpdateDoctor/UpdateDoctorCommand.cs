using HospitalManagment.Application.Features.Doctors.DTOs;
using MediatR;

namespace HospitalManagment.Application.Features.Doctors.Commands.UpdateDoctor
{
    public class UpdateDoctorCommand : IRequest<Unit>
    {
        public UpdateDoctorDto Doctor { get; set; }
    }
}
