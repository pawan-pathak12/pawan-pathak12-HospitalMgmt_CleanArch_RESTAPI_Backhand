using HospitalManagment.Application.Interfaces;
using HospitalManagment.Domain.Entity;
using MediatR;

namespace HospitalManagment.Application.Features.Patients.Commands.UpdatePatient
{
    public class UpdatePatientCommandHandler : IRequestHandler<UpdatePatientCommand, Unit>
    {
        private readonly IPatientRepository _repository;

        public UpdatePatientCommandHandler(IPatientRepository repository)
        {
            this._repository = repository;
        }
        public async Task<Unit> Handle(UpdatePatientCommand request, CancellationToken cancellationToken)
        {
            var result = await _repository.GetByIdAsync(request.Patient.Id);
            if (result == null)
            {
                throw new Exception("Error or update failed.");
            }
            var patient = new Patient
            {
                Id = request.Patient.Id,
                FullName = request.Patient.FullName,
                Address = request.Patient.Address,
                Age = request.Patient.Age,
                Email = request.Patient.Email,
                Gender = request.Patient.Gender,
                PhoneNumber = request.Patient.PhoneNumber
            };

            await _repository.UpdateAsync(request.Patient.Id, patient);
            return Unit.Value;
        }
    }
}
