using HospitalManagment.Domain.Entity;

namespace HospitalManagment.Application.Interfaces
{
    public interface IPatientRepository
    {
        #region CURD Operations 
        Task<IEnumerable<Patient>> GetAllAsync();
        Task<Patient> GetByIdAsync(int id);
        Task<Patient> AddAsync(Patient patient);
        Task<bool> UpdateAsync(int id, Patient patient);
        Task<bool> DeleteAsync(int id);
        #endregion

        #region Business Logic Practice 
        Task<IEnumerable<Patient>> GetByNameAsync(string name);
        #endregion
        Task SetPatientBlockUntilAsync(DateTime blockedDate ,int patientId);
    }
}
