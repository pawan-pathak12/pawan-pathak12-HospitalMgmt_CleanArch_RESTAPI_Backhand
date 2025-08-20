using HospitalManagment.Application.Features.Patients.DTOs;
using MediatR;

namespace HospitalManagment.Application.Features.Patients.Commands.UpdatePatient
{
    public class UpdatePatientCommand : IRequest<Unit>
    {
        public UpdatePatientDto Patient { get; set; }
    }
}
