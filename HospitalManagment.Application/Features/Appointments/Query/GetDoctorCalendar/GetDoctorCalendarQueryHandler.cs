using HospitalManagment.Application.Features.Appointments.DTOs;
using HospitalManagment.Application.Interfaces;
using MediatR;

namespace HospitalManagment.Application.Features.Appointments.Query.GetDoctorCalendar
{
    public class GetDoctorCalendarQueryHandler : IRequestHandler<GetDoctorCalendarQuery, IEnumerable<AppointmentDto>>
    {
        private readonly IAppointmentRepository _appointmentRepository;
        private readonly IDoctorRepository _doctorRepository;

        public GetDoctorCalendarQueryHandler(IAppointmentRepository appointmentRepository, IDoctorRepository doctorRepository)
        {
            this._appointmentRepository = appointmentRepository;
            this._doctorRepository = doctorRepository;
        }
        public async Task<IEnumerable<AppointmentDto>> Handle(GetDoctorCalendarQuery request, CancellationToken cancellationToken)
        {
            var doctor = await _doctorRepository.GetByIdAsync(request.DoctorId);
            if (doctor == null)
            {
                throw new Exception($"Doctor with id {request.DoctorId} doesnt exist.");
            }
            var result = await _appointmentRepository.GetAppointmentsForDoctorAsync(request.DoctorId);
            if (result == null || !result.Any())
            {
                throw new Exception($"Their is no appointment for doctor Id {request.DoctorId} till now .");
            }
           
            var doctorCalender = result.Select(a => new AppointmentDto
            {
                AppointmentDate = a.AppointmentDate,
                PatientId = a.PatientId,
                StartTime = a.StartTime,
                EndTime = a.EndTime,
                Id = a.Id

            });
            return doctorCalender;
        }
    }
}
