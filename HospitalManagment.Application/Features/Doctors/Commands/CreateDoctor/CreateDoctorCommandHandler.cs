using AutoMapper;
using HospitalManagment.Application.Features.Doctors.DTOs;
using HospitalManagment.Application.Interfaces;
using HospitalManagment.Domain.Entity;
using MediatR;

namespace HospitalManagment.Application.Features.Doctors.Commands.CreateDoctor
{
    public class CreateDoctorCommandHandler : IRequestHandler<CreateDoctorCommand, Doctor>
    {
        private readonly IDoctorRepository _repository;
        private readonly IMapper _mapper;

        public CreateDoctorCommandHandler(IDoctorRepository repository ,IMapper mapper)
        {
            _repository = repository;
            this._mapper = mapper;
        }
        public async Task<Doctor> Handle(CreateDoctorCommand request, CancellationToken cancellationToken)
        {
            // Map CreateDoctorDto → Doctor entity

            var doctorEntity = _mapper.Map<Doctor>(request.Doctor);

            
            //var doctor = new Doctor
            //{
            //    AvailableEndTime = request.Doctor.AvailableEndTime,
            //    AvailableStartTime = request.Doctor.AvailableStartTime,
            //    Email = request.Doctor.Email,
            //    FullName = request.Doctor.FullName,
            //    Phone = request.Doctor.Phone,
            //    Specialization = request.Doctor.Specialization,

            //};

            return await _repository.AddAsync(doctorEntity);
        }
    }
}
