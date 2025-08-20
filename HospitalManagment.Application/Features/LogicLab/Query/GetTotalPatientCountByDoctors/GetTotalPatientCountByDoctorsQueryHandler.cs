using HospitalManagment.Application.Interfaces;
using MediatR;
using System.Diagnostics.CodeAnalysis;

namespace HospitalManagment.Application.Features.LogicLab.Query.GetTotalPatientCountByDoctors
{
    public class GetTotalPatientCountByDoctorsQueryHandler:IRequestHandler<GetTotalPatientCountByDoctorsQuery , int>
    {
        private readonly IAppointmentLogicTester _appointmentLogicTester;
        private readonly IDoctorRepository _doctorRepository;

        public GetTotalPatientCountByDoctorsQueryHandler(IAppointmentLogicTester appointmentLogicTester , IDoctorRepository doctorRepository)
        {
            _appointmentLogicTester = appointmentLogicTester;
            _doctorRepository = doctorRepository;
        }

        public async Task<int> Handle(GetTotalPatientCountByDoctorsQuery request , CancellationToken cancellationToken)
        {
            var doctor = await _doctorRepository.GetByIdAsync(request.DoctorId);
            if (doctor==null)
            {
                throw new Exception($"Their is no Doctor with Id {request.DoctorId}");
            }
            var result = await _appointmentLogicTester.GetDoctorDailyAppointmentCountAsync(request.DoctorId);
           
            return result;
        }
    }
}
