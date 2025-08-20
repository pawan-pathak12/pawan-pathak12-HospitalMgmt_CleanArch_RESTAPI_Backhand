using HospitalManagment.Application.Features.Doctors.DTOs;
using HospitalManagment.Application.Interfaces;
using HospitalManagment.Domain.Entity;
using MediatR;

namespace HospitalManagment.Application.Features.Doctors.Commands.CreateDoctor
{
    public class CreateDoctorCommandHandler : IRequestHandler<CreateDoctorCommand, Doctor>
    {
        private readonly IDoctorRepository _repository;

        public CreateDoctorCommandHandler(IDoctorRepository repository)
        {
            _repository = repository;
        }
        public async Task<Doctor> Handle(CreateDoctorCommand request, CancellationToken cancellationToken)
        {
            var doctor = new Doctor
            {
                AvailableEndTime = request.Doctor.AvailableEndTime,
                AvailableStartTime = request.Doctor.AvailableStartTime,
                Email = request.Doctor.Email,
                FullName = request.Doctor.FullName,
                Phone = request.Doctor.Phone,
                Specialization = request.Doctor.Specialization,

            };
            return await _repository.AddAsync(doctor);
        }
    }
}
