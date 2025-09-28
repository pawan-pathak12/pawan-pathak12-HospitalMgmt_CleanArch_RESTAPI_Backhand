using HospitalManagment.Application.Features.Doctors.DTOs;
using HospitalManagment.Domain.Entity;

namespace HospitalManagment.Application.Interfaces;

public interface IDoctorRepository
{
    Task<bool> IsDoctorActiveAsync(int doctorId);
    Task<IEnumerable<Doctor>> GetActiveDoctorAsync();
    Task<IEnumerable<Doctor>> GetInActiveDoctor();
    Task<DoctorWorkingHourDto> GetDoctorWorkingHourAsync(int doctorId);

    Task<int> GetDoctorAppointmentCountByDateAsync(string type, int doctorId);

    #region Extra

    Task<int> GetDoctorDailyWorkingHoursAsync(int doctorId);
    Task<int> GetDoctorBookedAppointmentCountAsync(int doctorId);

    #endregion

    #region CURD Operations

    Task<IEnumerable<Doctor>> GetAllAsync();

    Task<Doctor> GetByIdAsync(int? id);

    Task<Doctor> AddAsync(Doctor doctor);

    Task<bool> UpdateAsync(int id, Doctor doctor);

    Task<bool> DeleteAsync(int id);

    #endregion
}