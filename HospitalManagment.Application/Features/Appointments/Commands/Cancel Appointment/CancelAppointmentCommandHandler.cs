using HospitalManagment.Application.Interfaces;
using MediatR;
using Microsoft.AspNetCore.Builder;

namespace HospitalManagment.Application.Features.Appointments.Commands.Cancel_Appointment
{
    public class CancelAppointmentCommandHandler : IRequestHandler<CancelAppointmentCommand>
    {
        private readonly IAppointmentRepository _appointmentRepository;
        private readonly IPatientRepository _patientRepository;

        public CancelAppointmentCommandHandler(IAppointmentRepository appointmentRepository , IPatientRepository patientRepository)
        {
            this._appointmentRepository = appointmentRepository;
            this._patientRepository = patientRepository;
        }

        public async Task Handle(CancelAppointmentCommand request, CancellationToken cancellationToken)
        {
            //1. check patient exists or not 
            var patient = await _patientRepository.GetByIdAsync(request.PatientId);
            if (patient==null)
            {
                throw new Exception($" Patient with Id {request.PatientId} doenst Exists");
            }
            // check their is appointment or not 
            var appointment = await _appointmentRepository.GetByIdAsync(request.Id);
            if (appointment ==null)
            {
                throw new Exception($"Appointment with Id {request.Id} not found.");
            }
            var isCanceled = await _appointmentRepository.UpdateStatusAsync(request.Id);
            if (!isCanceled)
            {
                throw new Exception($"Failed to cancel appointment with Id {request.Id}.");
            }


        }
    }
}
