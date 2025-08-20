using HospitalManagment.Application.Features.Appointments.DTOs;
using HospitalManagment.Application.Interfaces;
using MediatR;

namespace HospitalManagment.Application.Features.Appointments.Query.GetAppointmentById
{
    public class GetAppointmentByIdQueryHandler : IRequestHandler<GetAppointmentByIdQuery, AppointmentDto>
    {
        private readonly IAppointmentRepository _repository;

        public GetAppointmentByIdQueryHandler(IAppointmentRepository repository)
        {
            this._repository = repository;
        }
        public async Task<AppointmentDto> Handle(GetAppointmentByIdQuery request, CancellationToken cancellation)
        {
            var appointment = await _repository.GetByIdAsync(request.Id);
            if (appointment == null )
            {
                throw new Exception($"Their is no appointment with id {request.Id}.");
            }
            var result = new AppointmentDto
            {
                Id = appointment.Id,
                AppointmentDate = appointment.AppointmentDate,
                DoctorId = appointment.DoctorId,
                EndTime = appointment.EndTime,
                PatientId = appointment.PatientId,
                StartTime = appointment.StartTime,
                Status = appointment.Status
            };
            return result;
        }
    }
}
