using HospitalManagment.Application.Features.Patients.DTOs;
using HospitalManagment.Application.Interfaces;
using MediatR;

namespace HospitalManagment.Application.Features.Patients.Query.GetAllPatients
{
    public class GetAllPatientQueryHandler : IRequestHandler<GetAllPatientQuery, IEnumerable<PatientDto>>
    {
        private readonly IPatientRepository _repository;

        public GetAllPatientQueryHandler(IPatientRepository repository)
        {
            this._repository = repository;
        }
        public async Task<IEnumerable<PatientDto>> Handle(GetAllPatientQuery request, CancellationToken cancellationToken)
        {
            var patients = await _repository.GetAllAsync();
            if (patients== null || !patients.Any())
            {
                throw new Exception("Patients data not found ");
            }

            var result = patients.Select(s => new PatientDto
            {
                Address = s.Address,
                Age = s.Age,
                Email = s.Email,
                FullName = s.FullName,
                Gender = s.Gender,
                Id = s.Id,
                PhoneNumber = s.PhoneNumber
            });
            return result;
        }
    }
}
