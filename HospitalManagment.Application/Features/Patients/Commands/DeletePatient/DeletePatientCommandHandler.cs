using HospitalManagment.Application.Interfaces;
using MediatR;

namespace HospitalManagment.Application.Features.Patients.Commands.DeletePatient
{
    public class DeletePatientCommandHandler:IRequestHandler<DeletePatientCommad , Unit>
    {
        private readonly IPatientRepository _repository;

        public DeletePatientCommandHandler(IPatientRepository repository)
        {
            this._repository = repository;
        }
        public async Task<Unit> Handle(DeletePatientCommad request , CancellationToken cancellationToken)
        {
            var patient = await _repository.GetByIdAsync(request.Id);
            if(patient==null)
            {
                throw new Exception("Error delete failed");
            }
            await _repository.DeleteAsync(request.Id);
            return Unit.Value;
        }
    }
}
