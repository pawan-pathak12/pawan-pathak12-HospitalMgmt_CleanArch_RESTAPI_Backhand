using HospitalManagment.Application.Features.Doctors.DTOs;
using HospitalManagment.Domain.Entity;
using MediatR;

namespace HospitalManagment.Application.Features.Doctors.Commands.CreateDoctor
{
    public class CreateDoctorCommand : IRequest<Doctor>
    {
        public CreateDoctorDto Doctor { get; set; }
    }

}
