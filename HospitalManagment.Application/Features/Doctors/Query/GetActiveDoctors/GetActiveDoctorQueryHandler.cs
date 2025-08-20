using HospitalManagment.Application.Features.Doctors.DTOs;
using HospitalManagment.Application.Interfaces;
using MediatR;

namespace HospitalManagment.Application.Features.Doctors.Query.GetActiveDoctors
{
    public class GetActiveDoctorQueryHandler : IRequestHandler<GetActiveDoctorQuery, IEnumerable<DoctorDto>>
    {
        private readonly IDoctorRepository _doctorRepository;

        public GetActiveDoctorQueryHandler(IDoctorRepository doctorRepository)
        {
            this._doctorRepository = doctorRepository;
        }
        public async Task<IEnumerable<DoctorDto>> Handle(GetActiveDoctorQuery request, CancellationToken cancellationToken)
        {
            var result = await _doctorRepository.GetActiveDoctorAsync();
            var doctors = result.Select(s => new DoctorDto
            {
                AvailableEndTime = s.AvailableEndTime,
                AvailableStartTime = s.AvailableStartTime,
                Email = s.Email,
                FullName = s.FullName,
                Id = s.Id,
                Phone = s.Phone,
                Specialization = s.Specialization,
                IsActive = s.IsActive
            });
            return doctors;
        }
    }
}
