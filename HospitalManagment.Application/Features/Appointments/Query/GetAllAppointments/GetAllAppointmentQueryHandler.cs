using HospitalManagment.Application.Features.Appointments.DTOs;
using HospitalManagment.Application.Interfaces;
using MediatR;

namespace HospitalManagment.Application.Features.Appointments.Query.GetAllAppointments
{
    public class GetAllAppointmentQueryHandler : IRequestHandler<GetAllAppointmentQuery, IEnumerable<AppointmentDto>>
    {
        private readonly IAppointmentRepository _repository;

        public GetAllAppointmentQueryHandler(IAppointmentRepository repository)
        {
            this._repository = repository;
        }

        public async Task<IEnumerable<AppointmentDto>> Handle(GetAllAppointmentQuery request, CancellationToken cancellationToken)
        {
            var appointments = await _repository.GetAllAsync();
            if (appointments == null || !appointments.Any())
            {
                throw new Exception("Their is no appointments");
            }
            var result = appointments.Select(s => new AppointmentDto
            {
                AppointmentDate = s.AppointmentDate,
                DoctorId = s.DoctorId,
                EndTime = s.EndTime,
                Id = s.Id,
                PatientId = s.PatientId,
                StartTime = s.StartTime,
                Status = s.Status
            });
            return result;
        }
    }
}
