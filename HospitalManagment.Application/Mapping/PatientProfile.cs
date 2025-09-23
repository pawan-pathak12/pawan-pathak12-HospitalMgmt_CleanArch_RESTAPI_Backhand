using AutoMapper;
using HospitalManagment.Application.Features.Patients.DTOs;
using HospitalManagment.Domain.Entity;

namespace HospitalManagment.Application.Mapping
{
    public class PatientProfile : Profile
    {
        public PatientProfile()
        {
            CreateMap<Patient, CreatePatientDto>().ReverseMap();
            CreateMap<Patient, PatientDto>().ReverseMap();
            CreateMap<Patient, UpdatePatientDto>().ReverseMap();
        }
    }
}
