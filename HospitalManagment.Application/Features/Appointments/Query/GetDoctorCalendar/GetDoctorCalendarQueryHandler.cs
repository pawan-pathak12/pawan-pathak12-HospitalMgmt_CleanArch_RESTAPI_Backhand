using AutoMapper;
using HospitalManagment.Application.Features.Appointments.DTOs;
using HospitalManagment.Application.Features.Doctors.DTOs;
using HospitalManagment.Application.Interfaces;
using MediatR;

namespace HospitalManagment.Application.Features.Appointments.Query.GetDoctorCalendar
{
    public class GetDoctorCalendarQueryHandler : IRequestHandler<GetDoctorCalendarQuery, IEnumerable<AppointmentDto>>
    {
        private readonly IAppointmentRepository _appointmentRepository;
        private readonly IDoctorRepository _doctorRepository;
        private readonly IMapper _mapper;

        public GetDoctorCalendarQueryHandler(IAppointmentRepository appointmentRepository, IDoctorRepository doctorRepository ,IMapper mapper)
        {
            this._appointmentRepository = appointmentRepository;
            this._doctorRepository = doctorRepository;
            this._mapper = mapper;
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
            var doctorCalender = _mapper.Map<IEnumerable<AppointmentDto>>(result);
          
            return doctorCalender;
        }
    }
}
