using AutoMapper;
using HospitalManagment.Application.Features.Doctors.DTOs;
using HospitalManagment.Application.Interfaces;
using MediatR;

namespace HospitalManagment.Application.Features.Doctors.Query.GetInActiveDoctors
{
    public class GetInActiveDoctorQueryHandler : IRequestHandler<GetInActiveDoctorQuery, IEnumerable<DoctorDto>>
    {
        private readonly IDoctorRepository _doctorRepository;
        private readonly IMapper _mapper;

        public GetInActiveDoctorQueryHandler(IDoctorRepository doctorRepository ,IMapper mapper )
        {
            this._doctorRepository = doctorRepository;
            this._mapper = mapper;
        }
        public async Task<IEnumerable<DoctorDto>> Handle(GetInActiveDoctorQuery request, CancellationToken cancellationToken)
        {
            var result = await _doctorRepository.GetInActiveDoctor();
            var ActiveDoctors = _mapper.Map<IEnumerable<DoctorDto>>(result);       
            return ActiveDoctors;
        }

    }
}
