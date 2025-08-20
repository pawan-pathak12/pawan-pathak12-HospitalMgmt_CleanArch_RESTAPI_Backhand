using HospitalManagment.Application.Features.Doctors.DTOs;
using MediatR;

namespace HospitalManagment.Application.Features.LogicLab.Query.GetDoctorWorkingHour
{
    public class GetDoctorWorkingHourQuery : IRequest<DoctorWorkingHourDto>
    {
        public int Id { get; set; }
    }
}
