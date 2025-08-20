using HospitalManagment.Application.Interfaces;
using MediatR;

namespace HospitalManagment.Application.Features.Appointments.Commands.AutoMarkStatusNoShow
{
    public class AutoMarkStatusNotShownCommandHandler : IRequestHandler<AutoMarkStatusNotShownCommand>
    {
        private readonly IAppointmentRepository _appointmentRepository;

        public AutoMarkStatusNotShownCommandHandler(IAppointmentRepository appointmentRepository)
        {
            this._appointmentRepository = appointmentRepository;
        }

        public async Task Handle(AutoMarkStatusNotShownCommand request, CancellationToken cancellationToken)
        {
            var shownAppointment = await _appointmentRepository.GetPastScheduledAppointmentsAsync();
            if (!shownAppointment.Any())
            {
                throw new Exception("There are no past scheduled appointments.");
            }
            await _appointmentRepository.MarkAppointmentsAsNotShownAsync();
        }
    }
}
