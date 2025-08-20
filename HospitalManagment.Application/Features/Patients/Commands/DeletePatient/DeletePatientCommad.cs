using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HospitalManagment.Application.Features.Patients.Commands.DeletePatient
{
    public class DeletePatientCommad:IRequest<Unit>
    {
        public int Id { get; set; }
    }
}
