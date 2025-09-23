using HospitalManagment.Application.Features.Doctors.DTOs;
using HospitalManagment.Domain.Entity;

namespace HospitalManagment.Application.Interfaces;

public interface IDoctorRepository
{
    Task<bool> IsDoctorActiveAsync(int doctorId);
    Task<IEnumerable<Doctor>> GetActiveDoctorAsync();
    Task<IEnumerable<Doctor>> GetInActiveDoctor();
    Task<DoctorWorkingHourDto> GetDoctorWorkingHourAsync(int doctorId);

    #region CURD Operations

    Task<IEnumerable<Doctor>> GetAllAsync();

    Task<Doctor> GetByIdAsync(int id);

    Task<Doctor> AddAsync(Doctor doctor);

    Task<bool> UpdateAsync(int id, Doctor doctor);

    Task<bool> DeleteAsync(int id);

    #endregion
}