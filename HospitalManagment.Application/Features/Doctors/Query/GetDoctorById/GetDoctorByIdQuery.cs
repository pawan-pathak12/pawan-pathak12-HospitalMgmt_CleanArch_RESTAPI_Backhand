using HospitalManagment.Application.Features.Doctors.DTOs;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HospitalManagment.Application.Features.Doctors.Query.GetDoctorById
{
    public class GetDoctorByIdQuery:IRequest<DoctorDto>
    {
        public int Id { get; set; }
    }
}
