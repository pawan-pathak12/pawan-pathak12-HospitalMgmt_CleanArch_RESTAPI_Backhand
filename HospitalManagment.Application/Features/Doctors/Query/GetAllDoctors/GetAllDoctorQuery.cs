using HospitalManagment.Application.Features.Doctors.DTOs;
using MediatR;

namespace HospitalManagment.Application.Features.Doctors.Query.GetAllDoctors
{
    public class GetAllDoctorQuery : IRequest<IEnumerable<DoctorDto>>
    {
    }
}
