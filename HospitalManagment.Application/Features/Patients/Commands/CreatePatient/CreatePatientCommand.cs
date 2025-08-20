using HospitalManagment.Application.Features.Patients.DTOs;
using HospitalManagment.Domain.Entity;
using MediatR;

namespace HospitalManagment.Application.Features.Patients.Commands.CreatePatient
{
    public class CreatePatientCommand:IRequest<Patient>
    {
        public CreatePatientDto Patient { get; set; }
    }
}
