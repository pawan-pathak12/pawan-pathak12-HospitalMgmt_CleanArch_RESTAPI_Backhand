using AutoMapper;
using HospitalManagment.Application.Features.Appointments.DTOs;
using HospitalManagment.Domain.Entity;

namespace HospitalManagment.Application.Mapping
{
    public class AppointmentProfile:Profile
    {
        public AppointmentProfile()
        {
            CreateMap<Appointment, AppointmentDto>().ReverseMap();
            CreateMap<Appointment, CreateAppointmentDto>().ReverseMap();
            CreateMap<Appointment, UpdateAppointmentDto>().ReverseMap();
        }
    }
}
