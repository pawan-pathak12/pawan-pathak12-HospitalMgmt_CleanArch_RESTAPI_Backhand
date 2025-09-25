using HospitalManagment.Domain.Entity;

namespace HospitalManagment.Application.Interfaces;

public interface IAppointmentRepository
{
    Task<IEnumerable<int>> GetPastScheduledAppointmentsAsync();
    Task<bool> MarkAppointmentsAsNotShownAsync();


    Task<int> GetAppointmentCountByDateAsync(int? year = null, int? month = null, int? day = null,
        DateTime? date = null);

    #region curd operations

    Task<Appointment> AddAsync(Appointment appointment);
    Task<IEnumerable<Appointment>> GetAllAsync();
    Task<Appointment> GetByIdAsync(int id);

    Task<bool> UpdateAsync(Appointment appointment);

    // Cancel appointment
    Task<bool> UpdateStatusAsync(int id);

    Task<IEnumerable<Appointment>> GetAppointmentsByDateAsync(string type, DateTime date);
    Task<int> CountAppoitmentBetweenDateAsync(int? doctorId, DateTime startDate, DateTime endDate);

    #endregion

    #region BookingLogic

    // Core availability checks
    Task<bool> CheckAvailability(int doctorId, DateTime appointmentDate, TimeSpan startTime, TimeSpan endTime);
    Task<bool> IsTimeSlotSpacedAsync(int doctorId, DateTime appointmentDate, TimeSpan startTime);
    Task<int> CountBookingsAsync(int doctorId, DateTime appointmentDate);

    // Pre-validation rules
    Task<bool> BlockBookingOnSundayAsync(DateTime appointmentDate);
    Task<int> CheckNumberOfBookingOfPatient(int patientId, DateTime appointmentDate);
    Task<bool> BlockBookingOutOfDate(DateTime appointmentDate);
    Task<bool> BookingDateValidationAsync(DateTime appointemntDate);

    #endregion

    #region DataAccess

    Task<IEnumerable<Appointment>> GetAppointmentsForDoctorAsync(int doctorId);
    Task<bool> CheckPatientExisting(int patientId);

    #endregion
}