using HospitalManagment.Application.Interfaces;
using MediatR;

namespace HospitalManagment.Application.Features.Appointments.Query.GetDoctorAvailableAppointmentTimes;

public class GetDoctorAvailableAppointmentTimesQueryHandler : IRequestHandler<GetDoctorAvailableAppointmentTimesQuery>
{
    private readonly IDoctorRepository _doctorRepository;

    public GetDoctorAvailableAppointmentTimesQueryHandler(IDoctorRepository doctorRepository)
    {
        _doctorRepository = doctorRepository;
    }

    public Task Handle(GetDoctorAvailableAppointmentTimesQuery request, CancellationToken cancellationToken)
    {
        //step 1 : get doctor Working Hour 

        //step 2 : 

        throw new NotImplementedException();
    }
}