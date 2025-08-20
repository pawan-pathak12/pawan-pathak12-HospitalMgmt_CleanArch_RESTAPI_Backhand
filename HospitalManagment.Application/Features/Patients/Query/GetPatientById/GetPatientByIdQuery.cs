using HospitalManagment.Application.Features.Patients.DTOs;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HospitalManagment.Application.Features.Patients.Query.GetPatientById
{
    public class GetPatientByIdQuery :IRequest<PatientDto>
    {
        public int Id { get; set; }
    }
}
