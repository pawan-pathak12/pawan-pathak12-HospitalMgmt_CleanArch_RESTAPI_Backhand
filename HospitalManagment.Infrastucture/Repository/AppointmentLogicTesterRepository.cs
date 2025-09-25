using Dapper;
using HospitalManagment.Application.Interfaces;
using HospitalManagment.Domain.Entity;
using HospitalManagment.Domain.Enums;
using HospitalManagment.Infrastucture.Data;

namespace HospitalManagment.Infrastucture.Repository;

public class AppointmentLogicTesterRepository : IAppointmentLogicTester
{
    private readonly DapperDbContext _dapperDbContext;

    public AppointmentLogicTesterRepository(DapperDbContext dapperDbContext)
    {
        _dapperDbContext = dapperDbContext;
    }

    /// Retrieves all upcoming appointments for a specific doctor within the next 7 days.
    async Task<IEnumerable<Appointment>> IAppointmentLogicTester.GetFutureAppointmentsByDoctorAsync(int doctorId)
    {
        using var connection = _dapperDbContext.Connection();
        var query = "select * from Appointments " +
                    " where DoctorId =@DoctorId AND AppointmentDate >= CAST(GETDATE() AS date)" +
                    "  AND AppointmentDate <= DATEADD(DAY, 7, CAST(GETDATE() AS date))";
        var result = await connection.QueryAsync<Appointment>(query, new { DoctorId = doctorId });
        return result;
    }

    // Counts how many times a patient has either cancelled or missed appointments.
    async Task<int> IAppointmentLogicTester.CheckAndBlockPatientIfNeededAsync(int patientId)
    {
        using var connection = _dapperDbContext.Connection();
        var query =
            "select count(1) from Appointments where PatientId =@PatientId and (Status=@Status1 or Status=@Status2)";
        var result = await connection.ExecuteAsync(query,
            new { PatientId = patientId, Status1 = AppointmentStatus.Cancelled, Status2 = AppointmentStatus.NotShown });
        return result;
    }

    #region Get Count of ...

    // Returns the total number of appointments scheduled for a doctor on the current day.
    async Task<int> IAppointmentLogicTester.GetDoctorDailyAppointmentCountAsync(int doctorId)
    {
        using var connection = _dapperDbContext.Connection();
        var query = "select count(*) from Appointments where DoctorId=@DoctorId and " +
                    " AppointmentDate= @AppointmentDate";
        var result =
            await connection.QuerySingleAsync<int>(query, new { DoctorId = doctorId, AppointmentDate = DateTime.Now });
        return result;
    }

    //get total number of appointment doctor have till now 
    public async Task<int> GetDoctorAppointmentCountAsync(int doctorId)
    {
        using var connection = _dapperDbContext.Connection();
        var query = "Select count (1) from Appointments where DoctorId=@DoctorId";
        var result = await connection.ExecuteScalarAsync<int>(query, new { DoctorId = doctorId });
        return result;
    }

    public async Task<int> GetPatientDoctorVisitCountAsync(int patientId, int doctorId, int? year, int? month)
    {
        using var connection = _dapperDbContext.Connection();
        string sql;
        object parameters;

        if (!year.HasValue && !month.HasValue)
        {
            sql = @"SELECT COUNT(*) FROM Appointments WHERE PatientId = @PatientId AND DoctorId = @DoctorId";
            parameters = new { DoctorId = doctorId, PatientId = patientId };
        }
        else if (year.HasValue && !month.HasValue)
        {
            sql =
                @"SELECT COUNT(*) FROM Appointments  WHERE PatientId = @PatientId AND DoctorId = @DoctorId AND DATEPART(YEAR, AppointmentDate) = @Year";
            parameters = new { DoctorId = doctorId, PatientId = patientId, Year = year };
        }
        else if (year.HasValue && month.HasValue)
        {
            sql = @"SELECT COUNT(*)  FROM Appointments  WHERE PatientId = @PatientId AND DoctorId = @DoctorId
                  AND DATEPART(YEAR, AppointmentDate) = @Year
                  AND DATEPART(MONTH, AppointmentDate) = @Month";
            parameters = new { DoctorId = doctorId, PatientId = patientId, Year = year, Month = month };
        }
        else
        {
            throw new ArgumentException("Month cannot be provided without year.");
        }

        return await connection.ExecuteScalarAsync<int>(sql, parameters);
    }

    #endregion
}