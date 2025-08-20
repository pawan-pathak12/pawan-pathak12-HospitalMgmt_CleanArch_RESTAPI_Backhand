using HospitalManagment.Application.Features.Doctors.DTOs;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HospitalManagment.Application.Features.Doctors.Query.GetInActiveDoctors
{
    public class GetInActiveDoctorQuery: IRequest<IEnumerable<DoctorDto>>
    {
    }
}
