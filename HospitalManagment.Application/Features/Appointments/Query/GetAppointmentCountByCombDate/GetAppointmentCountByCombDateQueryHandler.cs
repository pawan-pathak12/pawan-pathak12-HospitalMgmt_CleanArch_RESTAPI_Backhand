using HospitalManagment.Application.Interfaces;
using MediatR;

namespace HospitalManagment.Application.Features.Appointments.Query.GetAppointmentCountByCombDate;

public class GetAppointmentCountByCombDateQueryHandler : IRequestHandler<GetAppointmentCountByCombDateQuery, int>
{
    private readonly IAppointmentRepository _appointmentRepository;

    public GetAppointmentCountByCombDateQueryHandler(IAppointmentRepository appointmentRepository)
    {
        _appointmentRepository = appointmentRepository;
    }

    public async Task<int> Handle(GetAppointmentCountByCombDateQuery request, CancellationToken cancellationToken)
    {
        var result =
            await _appointmentRepository.GetAppointmentCountByDateAsync(request.Year, request.Month, request.Day,
                request.DateTime);
        if (result == -1) throw new Exception("PLease select Valid combination");
        if (result == 0) throw new Exception($" their is {result} Appointments ");

        return result;
    }
}