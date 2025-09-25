using HospitalManagment.Domain.Entity;

namespace HospitalManagment.Application.Interfaces;

public interface IAppointmentLogicTester
{
    Task<int> GetDoctorDailyAppointmentCountAsync(int doctorId);
    Task<IEnumerable<Appointment>> GetFutureAppointmentsByDoctorAsync(int doctorId);
    Task<int> CheckAndBlockPatientIfNeededAsync(int patientId);

    Task<int> GetDoctorAppointmentCountAsync(int doctorId);

    // it should give count : how many time patient have visited to particular doctor , additionally use can enter year-month , year only
    Task<int> GetPatientDoctorVisitCountAsync(int patientId, int doctorId, int? year, int? month);
}