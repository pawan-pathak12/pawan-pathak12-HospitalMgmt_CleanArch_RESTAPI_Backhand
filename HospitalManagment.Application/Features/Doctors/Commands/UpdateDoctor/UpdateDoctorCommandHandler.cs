using AutoMapper;
using HospitalManagment.Application.Interfaces;
using HospitalManagment.Domain.Entity;
using MediatR;

namespace HospitalManagment.Application.Features.Doctors.Commands.UpdateDoctor
{
    internal class UpdateDoctorCommandHandler : IRequestHandler<UpdateDoctorCommand, Unit>
    {
        private readonly IDoctorRepository _repository;
        private readonly IMapper _mapper;

        public UpdateDoctorCommandHandler(IDoctorRepository repository ,IMapper mapper)
        {
            this._repository = repository;
            this._mapper = mapper;
        }
        public async Task<Unit> Handle(UpdateDoctorCommand request, CancellationToken cancellationToken)
        {
            var result = await _repository.GetByIdAsync(request.Doctor.Id);
            if (result == null)
            {
                throw new Exception("Error or Update Failed");
            }
            var docotorEntity = _mapper.Map<Doctor>(request.Doctor);           
            await _repository.UpdateAsync(request.Doctor.Id, docotorEntity);
            return Unit.Value;
        }
    }
}
