using AutoMapper;
using HospitalManagment.Application.Features.Appointments.DTOs;
using HospitalManagment.Application.Interfaces;
using MediatR;

namespace HospitalManagment.Application.Features.Appointments.Query.GetAllAppointments
{
    public class GetAllAppointmentQueryHandler : IRequestHandler<GetAllAppointmentQuery, IEnumerable<AppointmentDto>>
    {
        private readonly IAppointmentRepository _repository;
        private readonly IMapper _mapper;

        public GetAllAppointmentQueryHandler(IAppointmentRepository repository ,IMapper mapper)
        {
            this._repository = repository;
            this._mapper = mapper;
        }

        public async Task<IEnumerable<AppointmentDto>> Handle(GetAllAppointmentQuery request, CancellationToken cancellationToken)
        {
            var appointments = await _repository.GetAllAsync();
            if (appointments == null || !appointments.Any())
            {
                throw new Exception("Their is no appointments");
            }
            var appointmentEntity = _mapper.Map<IEnumerable<AppointmentDto>>(appointments);
           
            return appointmentEntity;
        }
    }
}
