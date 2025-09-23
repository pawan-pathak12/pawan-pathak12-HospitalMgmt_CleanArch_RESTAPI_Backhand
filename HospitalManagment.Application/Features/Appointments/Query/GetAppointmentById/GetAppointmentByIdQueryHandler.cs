using AutoMapper;
using HospitalManagment.Application.Features.Appointments.DTOs;
using HospitalManagment.Application.Interfaces;
using MediatR;

namespace HospitalManagment.Application.Features.Appointments.Query.GetAppointmentById
{
    public class GetAppointmentByIdQueryHandler : IRequestHandler<GetAppointmentByIdQuery, AppointmentDto>
    {
        private readonly IAppointmentRepository _repository;
        private readonly IMapper _mapper;

        public GetAppointmentByIdQueryHandler(IAppointmentRepository repository ,IMapper mapper)
        {
            this._repository = repository;
            this._mapper = mapper;
        }
        public async Task<AppointmentDto> Handle(GetAppointmentByIdQuery request, CancellationToken cancellation)
        {
            var appointment = await _repository.GetByIdAsync(request.Id);
            if (appointment == null )
            {
                throw new Exception($"Their is no appointment with id {request.Id}.");
            }
            var appointmentEntity = _mapper.Map<AppointmentDto>(appointment);
         
            return appointmentEntity;
        }
    }
}
