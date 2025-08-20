using HospitalManagment.Application.Interfaces;
using HospitalManagment.Domain.Entity;
using MediatR;

namespace HospitalManagment.Application.Features.Doctors.Commands.UpdateDoctor
{
    internal class UpdateDoctorCommandHandler : IRequestHandler<UpdateDoctorCommand, Unit>
    {
        private readonly IDoctorRepository _repository;

        public UpdateDoctorCommandHandler(IDoctorRepository repository)
        {
            this._repository = repository;
        }
        public async Task<Unit> Handle(UpdateDoctorCommand request, CancellationToken cancellationToken)
        {
            var result = await _repository.GetByIdAsync(request.Doctor.Id);
            if (result == null)
            {
                throw new Exception("Error or Update Failed");
            }
            var doctor = new Doctor
            {
                AvailableEndTime = request.Doctor.AvailableEndTime,
                AvailableStartTime = request.Doctor.AvailableStartTime,
                Email = request.Doctor.Email,
                FullName = request.Doctor.FullName,
                Phone = request.Doctor.Phone,
                Specialization = request.Doctor.Specialization

            };
            await _repository.UpdateAsync(request.Doctor.Id, doctor);
            return Unit.Value;
        }
    }
}
