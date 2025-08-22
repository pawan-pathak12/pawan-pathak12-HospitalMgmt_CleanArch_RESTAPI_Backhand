using Dapper;
using HospitalManagment.Application.Features.Doctors.DTOs;
using HospitalManagment.Application.Interfaces;
using HospitalManagment.Domain.Entity;
using HospitalManagment.Infrastucture.Data;

namespace HospitalManagment.Infrastucture.Repository
{
    public class DoctorRepository : IDoctorRepository
    {
        private readonly DapperDbContext _dapperDbContext;

        public DoctorRepository(DapperDbContext dapperDbContext)
        {
            this._dapperDbContext = dapperDbContext;
        }
        #region CURD Operations of Doctors 
        async Task<Doctor> IDoctorRepository.AddAsync(Doctor doctor)
        {
            using var connection = _dapperDbContext.Connection();
            var sql = "INSERT INTO Doctors  (FullName, Specialization, Phone, Email, AvailableStartTime, AvailableEndTime,IsActive) values   (@FullName, @Specialization, @Phone, @Email, @AvailableStartTime, @AvailableEndTime,@IsActive)";
            await connection.ExecuteAsync(sql, doctor);
            return doctor;
        }

        async Task<bool> IDoctorRepository.DeleteAsync(int id)
        {
            using var connection = _dapperDbContext.Connection();
            var sql = "Delete from Doctors where Id=@Id";
            var result = await connection.ExecuteAsync(sql, new { Id = id });
            return result > 0;
        }

        async Task<IEnumerable<Doctor>> IDoctorRepository.GetAllAsync()
        {
            using var connection = _dapperDbContext.Connection();
            var sql = "Select * from Doctors";
            var result = await connection.QueryAsync<Doctor>(sql);
            return result;
        }

        async Task<Doctor> IDoctorRepository.GetByIdAsync(int id)
        {
            using var connection = _dapperDbContext.Connection();
            var sql = "Select * from Doctors where Id=@Id";
            var result = await connection.QueryFirstOrDefaultAsync<Doctor>(sql, new { Id = id });
            return result;
        }
        async Task<bool> IDoctorRepository.UpdateAsync(int id, Doctor doctor)
        {
            using var connection = _dapperDbContext.Connection();
            var sql = "UPDATE Doctors SET FullName = @FullName,Specialization = @Specialization,   Phone = @Phone,    Email = @Email,  AvailableStartTime = @AvailableStartTime,  IsActive=@IsActive , AvailableEndTime = @AvailableEndTime WHERE Id = @Id";
            var result = await connection.ExecuteAsync(sql, new { Id = id, doctor.AvailableEndTime, doctor.AvailableStartTime, doctor.FullName, doctor.Phone, doctor.Specialization, doctor.Email ,doctor.IsActive });
            return result > 0;

        }
        #endregion 

        
        async Task<bool> IDoctorRepository.IsDoctorActiveAsync(int doctorId)
        {
            using var connection = _dapperDbContext.Connection();
            var query = "SELECT IsActive FROM Doctors WHERE Id = @DoctorId";
            var result = await connection.QuerySingleOrDefaultAsync<bool>(query, new { DoctorId = doctorId });
            return result;
        }

        async Task<IEnumerable<Doctor>> IDoctorRepository.GetActiveDoctorAsync()
        {
            // hard coded 
            using var connection = _dapperDbContext.Connection();
            var query = "select * from Doctors where IsActive=1";
            var result = await connection.QueryAsync<Doctor>(query);
            return result;

        }

        async Task<IEnumerable<Doctor>> IDoctorRepository.GetInActiveDoctor()
        {// hardcoded , later can be changed by creating enum :doctorStatus : 1,0 ...
            using var connection = _dapperDbContext.Connection();
            var query = "select * from Doctors where IsActive=0";
            var result = await connection.QueryAsync<Doctor>(query);
            return result;
        }

        async Task<DoctorWorkingHourDto> IDoctorRepository.GetDoctorWorkingHourAsync(int doctorId)
        {
            using var connection = _dapperDbContext.Connection();
            var query = "Select AvailableStartTime , AvailableEndTime from Doctors where Id=@DoctorId";
            var result = await connection.QuerySingleOrDefaultAsync<DoctorWorkingHourDto>(query, new { DoctorId = doctorId });
            return result;
        }
    }
}
