using HospitalManagment.Application.Features.Patients.DTOs;
using HospitalManagment.Application.Interfaces;
using MediatR;

namespace HospitalManagment.Application.Features.Patients.Query.GetPatientById
{
    public class GetPatientByIdQueryHandler : IRequestHandler<GetPatientByIdQuery, PatientDto>
    {
        private readonly IPatientRepository _repository;

        public GetPatientByIdQueryHandler(IPatientRepository repository)
        {
            this._repository = repository;
        }
        public async Task<PatientDto> Handle(GetPatientByIdQuery request, CancellationToken cancellationToken)
        {
            var patient = await _repository.GetByIdAsync(request.Id);
            if (patient == null)
            {
                throw new Exception($"Patient with id {request.Id} not found");
            }
            var result = new PatientDto
            {
                Id = patient.Id,
                Address = patient.Address,
                Age = patient.Age,
                Email = patient.Email,
                FullName = patient.FullName,
                Gender = patient.Gender,
                PhoneNumber = patient.PhoneNumber 
            };
            return result;
        }
    }
}
