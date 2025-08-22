using AutoMapper;
using HospitalManagment.Application.Interfaces;
using HospitalManagment.Domain.Entity;
using MediatR;

namespace HospitalManagment.Application.Features.Patients.Commands.CreatePatient
{
    public class CreatePatientCommandHandler : IRequestHandler<CreatePatientCommand, Patient>
    {
        private readonly IPatientRepository _repository;
        private readonly IMapper _mapper;

        public CreatePatientCommandHandler(IPatientRepository repository,IMapper mapper)
        {
            this._repository = repository;
            this._mapper = mapper;
        }

        public async Task<Patient> Handle(CreatePatientCommand request, CancellationToken cancellationToken)
        {
            var patient = _mapper.Map<Patient>(request.Patient);

            //var patient = new Patient
            //{
            //    FullName = request.Patient.FullName,
            //    Address = request.Patient.Address,
            //    Age = request.Patient.Age,
            //    Email = request.Patient.Email,
            //    Gender = request.Patient.Gender,
            //    PhoneNumber = request.Patient.PhoneNumber
            //};
            var result = await _repository.AddAsync(patient);
            return result;
        }
    }
}
