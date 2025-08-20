using HospitalManagment.Domain.Entity;

namespace HospitalManagment.Application.Interfaces
{
    public interface IAppointmentLogicTester
    {
        Task<int> GetDoctorDailyAppointmentCountAsync(int doctorId);
        Task<IEnumerable<Appointment>> GetFutureAppointmentsByDoctorAsync(int doctorId);
        Task<int> CheckAndBlockPatientIfNeededAsync(int patientId);


    }
}