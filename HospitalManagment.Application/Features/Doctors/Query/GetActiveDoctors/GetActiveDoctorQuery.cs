using HospitalManagment.Application.Features.Doctors.DTOs;
using MediatR;

namespace HospitalManagment.Application.Features.Doctors.Query.GetActiveDoctors
{
    public class GetActiveDoctorQuery:IRequest<IEnumerable<DoctorDto>>
    {
    }
}
