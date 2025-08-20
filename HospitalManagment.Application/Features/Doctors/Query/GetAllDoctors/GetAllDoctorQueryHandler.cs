using HospitalManagment.Application.Features.Doctors.DTOs;
using HospitalManagment.Application.Interfaces;
using MediatR;

namespace HospitalManagment.Application.Features.Doctors.Query.GetAllDoctors
{
    public class GetAllDoctorQueryHandler : IRequestHandler<GetAllDoctorQuery, IEnumerable<DoctorDto>>
    {
        private readonly IDoctorRepository _repository;

        public GetAllDoctorQueryHandler(IDoctorRepository repository)
        {
            this._repository = repository;
        }
        public async Task<IEnumerable<DoctorDto>> Handle(GetAllDoctorQuery request, CancellationToken cancellationToken)
        {
            var doctors = await _repository.GetAllAsync();
            if (doctors== null || !doctors.Any())
            {
                throw new Exception("Doctors data is blank");
            }
            var result = doctors.Select(s => new DoctorDto
            {
                Id = s.Id,
                AvailableEndTime = s.AvailableEndTime,
                AvailableStartTime = s.AvailableStartTime,
                Email = s.Email,
                FullName = s.FullName,
                Phone = s.Phone,
                Specialization = s.Specialization

            });

            return result;
        }
    }
}
