using Dapper;
using HospitalManagment.Application.Features.Doctors.DTOs;
using HospitalManagment.Application.Interfaces;
using HospitalManagment.Domain.Entity;
using HospitalManagment.Domain.Enums;
using HospitalManagment.Infrastucture.Data;

namespace HospitalManagment.Infrastucture.Repository;

public class DoctorRepository : IDoctorRepository
{
    private readonly DapperDbContext _dapperDbContext;

    public DoctorRepository(DapperDbContext dapperDbContext)
    {
        _dapperDbContext = dapperDbContext;
    }

    #region Extra

    public async Task<int> GetDoctorDailyWorkingHoursAsync(int doctorId)
    {
        using var connection = _dapperDbContext.Connection();
        var sql = "select  DATEDIFF(hour , AvailableStartTime , AvailableEndTime) from Doctors where Id=@Id";
        var result = await connection.ExecuteScalarAsync<int>(sql, new { Id = doctorId });
        return result;
    }

    public async Task<int> GetDoctorBookedAppointmentCountAsync(int doctorId)
    {
        using var connection = _dapperDbContext.Connection();
        var parms = new
        {
            DoctorId = doctorId,
            Status1 = AppointmentStatus.Scheduled,
            Status2 = AppointmentStatus.OnGoing
        };
        var sql =
            " select COUNT(*) from Appointments where Appointments.DoctorId= @DoctorId and Status between @Status1 and @status2 ";
        var result = await connection.ExecuteScalarAsync<int>(sql, parms);
        return result;
    }

    public async Task<int> GetDoctorRemainingAppointmentSlotsAsync(int doctorId)
    {
        using var connection = _dapperDbContext.Connection();

        // Each appointment = 30 minutes, so total slots = working hours * 2
        var totalSlots = 2 * await GetDoctorDailyWorkingHoursAsync(doctorId);

        var bookedSlots = await GetDoctorBookedAppointmentCountAsync(doctorId);

        var remainingSlots = totalSlots - bookedSlots;

        return Math.Max(0, remainingSlots); // never return negative
    }

    #endregion

    #region CURD Operations of Doctors

    async Task<Doctor> IDoctorRepository.AddAsync(Doctor doctor)
    {
        using var connection = _dapperDbContext.Connection();
        var sql =
            "INSERT INTO Doctors  (FullName, Specialization, Phone, Email, AvailableStartTime, AvailableEndTime,IsActive) values   (@FullName, @Specialization, @Phone, @Email, @AvailableStartTime, @AvailableEndTime,@IsActive)";
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


    public async Task<IEnumerable<Doctor>> GetAllAsync()
    {
        using var connection = _dapperDbContext.Connection();
        var sql = "Select * from Doctors";
        var result = await connection.QueryAsync<Doctor>(sql);
        return result;
    }

    async Task<Doctor> IDoctorRepository.GetByIdAsync(int? id)
    {
        using var connection = _dapperDbContext.Connection();
        var sql = "Select * from Doctors where Id=@Id";
        var result = await connection.QueryFirstOrDefaultAsync<Doctor>(sql, new { Id = id });
        return result;
    }

    async Task<bool> IDoctorRepository.UpdateAsync(int id, Doctor doctor)
    {
        using var connection = _dapperDbContext.Connection();
        var sql =
            "UPDATE Doctors SET FullName = @FullName,Specialization = @Specialization,   Phone = @Phone,    Email = @Email,  AvailableStartTime = @AvailableStartTime,  IsActive=@IsActive , AvailableEndTime = @AvailableEndTime WHERE Id = @Id";
        var result = await connection.ExecuteAsync(sql, doctor);

        return result > 0;
    }

    #endregion

    #region Doctor Validation and Logic

    async Task<bool> IDoctorRepository.IsDoctorActiveAsync(int doctorId)
    {
        using var connection = _dapperDbContext.Connection();
        var query = "SELECT IsActive FROM Doctors WHERE Id = @DoctorId";
        var result = await connection.QuerySingleOrDefaultAsync<bool>(query, new { DoctorId = doctorId });
        return result;
    }

    #region Get Active/InActive

    async Task<IEnumerable<Doctor>> IDoctorRepository.GetActiveDoctorAsync()
    {
        // hard coded 
        using var connection = _dapperDbContext.Connection();
        var query = "select * from Doctors where IsActive=1";
        var result = await connection.QueryAsync<Doctor>(query);
        return result;
    }

    async Task<IEnumerable<Doctor>> IDoctorRepository.GetInActiveDoctor()
    {
        // hardcoded , later can be changed by creating enum :doctorStatus : 1,0 ...
        using var connection = _dapperDbContext.Connection();
        var query = "select * from Doctors where IsActive=0";
        var result = await connection.QueryAsync<Doctor>(query);
        return result;
    }

    #endregion

    #region GetCount

    async Task<DoctorWorkingHourDto> IDoctorRepository.GetDoctorWorkingHourAsync(int doctorId)
    {
        using var connection = _dapperDbContext.Connection();
        var query = "Select AvailableStartTime , AvailableEndTime from Doctors where Id=@DoctorId";
        var result =
            await connection.QuerySingleOrDefaultAsync<DoctorWorkingHourDto>(query, new { DoctorId = doctorId });
        return result;
    }

    public async Task<int> GetDoctorAppointmentCountByDateAsync(string type, int doctorId)
    {
        using var connection = _dapperDbContext.Connection();
        var sql = string.Empty;
        var date = DateTime.Today;
        switch (type.ToLower())
        {
            case "past":
                sql =
                    "Select count(1) from Appointments where DoctorId=@DoctorId and cast(AppointmentDate as Date) <cast(@Date as DATE)";
                return await connection.ExecuteScalarAsync<int>(sql, new { DoctorId = doctorId, Date = date });

            case "future":
                sql =
                    "Select count(1) from Appointments where DoctorId=@DoctorId and cast(AppointmentDate as Date) >cast(@Date as DATE)";
                return await connection.ExecuteScalarAsync<int>(sql, new { DoctorId = doctorId, Date = date });

            case "today":
            default:
                sql =
                    "Select count(1) from Appointments where DoctorId=@DoctorId and cast(AppointmentDate as Date) = cast(@Date as DATE)";
                return await connection.ExecuteScalarAsync<int>(sql, new { DoctorId = doctorId, Date = date });
        }
    }

    #endregion`

    #endregion
}