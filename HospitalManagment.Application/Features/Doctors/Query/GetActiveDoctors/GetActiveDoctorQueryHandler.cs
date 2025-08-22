using AutoMapper;
using HospitalManagment.Application.Features.Appointments.DTOs;
using HospitalManagment.Application.Features.Doctors.DTOs;
using HospitalManagment.Application.Interfaces;
using MediatR;

namespace HospitalManagment.Application.Features.Doctors.Query.GetActiveDoctors
{
    public class GetActiveDoctorQueryHandler : IRequestHandler<GetActiveDoctorQuery, IEnumerable<DoctorDto>>
    {
        private readonly IDoctorRepository _doctorRepository;
        private readonly IMapper _mapper;

        public GetActiveDoctorQueryHandler(IDoctorRepository doctorRepository ,IMapper mapper)
        {
            this._doctorRepository = doctorRepository;
            this._mapper = mapper;
        }
        public async Task<IEnumerable<DoctorDto>> Handle(GetActiveDoctorQuery request, CancellationToken cancellationToken)
        {
            var result = await _doctorRepository.GetActiveDoctorAsync();
            var doctorEntity = _mapper.Map<IEnumerable<DoctorDto>>(result);
            return doctorEntity;
        }
    }
}
