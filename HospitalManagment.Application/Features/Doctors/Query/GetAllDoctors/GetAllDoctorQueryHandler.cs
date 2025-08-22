using AutoMapper;
using HospitalManagment.Application.Features.Doctors.DTOs;
using HospitalManagment.Application.Interfaces;
using MediatR;

namespace HospitalManagment.Application.Features.Doctors.Query.GetAllDoctors
{
    public class GetAllDoctorQueryHandler : IRequestHandler<GetAllDoctorQuery, IEnumerable<DoctorDto>>
    {
        private readonly IDoctorRepository _repository;
        private readonly IMapper _mapper;

        public GetAllDoctorQueryHandler(IDoctorRepository repository ,IMapper mapper)
        {
            this._repository = repository;
            this._mapper = mapper;
        }
        public async Task<IEnumerable<DoctorDto>> Handle(GetAllDoctorQuery request, CancellationToken cancellationToken)
        {
            var doctors = await _repository.GetAllAsync();
            if (doctors== null || !doctors.Any())
            {
                throw new Exception("Doctors data is blank");
            }
            var doctorEntity = _mapper.Map<IEnumerable<DoctorDto>>(doctors);
            return doctorEntity;
        }
    }
}
