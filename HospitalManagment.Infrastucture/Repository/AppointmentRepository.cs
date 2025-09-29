using Dapper;
using HospitalManagment.Application.Features.Appointments.DTOs;
using HospitalManagment.Application.Interfaces;
using HospitalManagment.Domain.Entity;
using HospitalManagment.Domain.Enums;
using HospitalManagment.Infrastucture.Data;

namespace HospitalManagment.Infrastucture.Repository;

public class AppointmentRepository : IAppointmentRepository
{
    private readonly DapperDbContext _dbContext;
    private readonly IDoctorRepository _doctorRepository;

    public AppointmentRepository(DapperDbContext dbContext, IDoctorRepository doctorRepository)
    {
        _dbContext = dbContext;
        _doctorRepository = doctorRepository;
    }

    #region Appointment Retrieval

    async Task<IEnumerable<Appointment>> IAppointmentRepository.GetAppointmentsForDoctorAsync(int doctorId)
    {
        using var connection = _dbContext.Connection();
        var sql = @" SELECT Appointments.* FROM Appointments
                         JOIN Doctors ON Appointments.DoctorId = Doctors.Id
                         WHERE Appointments.DoctorId = @DoctorId
                         AND Appointments.Status = @Status
                        and Appointments.AppointmentDate=@AppointmentDate
                         AND cast(@AppointmentTime as time) BETWEEN Doctors.AvailableStartTime AND Doctors.AvailableEndTime";


        var result = await connection.QueryAsync<Appointment>(sql,
            new
            {
                DoctorId = doctorId, AppointmentTime = DateTime.UtcNow, AppointmentDate = DateTime.UtcNow,
                Status = AppointmentStatus.Scheduled
            });
        return result;
    }

    #endregion

    #region Booking Count

    async Task<int> IAppointmentRepository.CountBookingsAsync(int doctorId, DateTime appointmentDate)
    {
        using var connection = _dbContext.Connection();
        var query = "select count(*) from Appointments where DoctorId=@DoctorId and AppointmentDate=@AppointmentDate";
        var result = await connection.ExecuteScalarAsync<int>(query, new { doctorId, appointmentDate });
        return result;
    }

    #endregion

    #region Patient Checks

    async Task<bool> IAppointmentRepository.CheckPatientExisting(int patientid)
    {
        using var connection = _dbContext.Connection();
        var query = "select count(1) from Patients where Id =@Id";
        var result = await connection.ExecuteScalarAsync<int>(query, new { Id = patientid });
        return result > 0;
    }

    #endregion

    #region Appointment Count

    public async Task<int> GetAppointmentCountByDateAsync(int? year = null, int? month = null, int? day = null,
        DateTime? date = null)
    {
        // combination : month and day  , year and month , (year , month , day )  , year only 
        using var connection = _dbContext.Connection();
        var sql = string.Empty;
        switch (year.HasValue, month.HasValue, day.HasValue)
        {
            case (true, true, false):
                // year + month
                sql =
                    "Select count (*) from Appointments where DatePart(Year , AppointmentDate) =@Year and DatePart(Month , AppointmentDate) =@Month";
                return await connection.ExecuteScalarAsync<int>(sql, new { Year = year, Month = month });
            case (false, true, true):
                // month + day 
                sql =
                    "Select count (*) from Appointments where DatePart(Month , AppointmentDate) = @Month  and DatePart(Day , AppointmentDate)=@Day";
                return await connection.ExecuteScalarAsync<int>(sql, new { Month = month, Day = day });
            case (true, true, true):
                // year - month - day 
                sql = "select count (*) from Appointments where cast(AppointmentDate as Date) = cast(@Date as Date) ";
                return await connection.ExecuteScalarAsync<int>(sql, new { Date = date });
            case (true, false, false):
                sql = "Select count (*) from Appointments where  DatePart(Year , AppointmentDate) = @Year";
                return await connection.ExecuteScalarAsync<int>(sql, new { Year = year });
            default:
                return -1;
        }
    }

    #endregion

    public async Task<int> CountAppoitmentBetweenDateAsync(int? doctorId, DateTime startDate, DateTime endDate)
    {
        using var connection = _dbContext.Connection();
        var sql = string.Empty;
        if (doctorId.HasValue)
        {
            sql =
                @"SELECT COUNT(*)  FROM Appointments WHERE cast (AppointmentDate as Date) BETWEEN @StartDate AND @EndDate AND DoctorId = @DoctorId";

            return await connection.ExecuteScalarAsync<int>(sql,
                new { StartDate = startDate, EndDate = endDate, DoctorId = doctorId });
        }

        sql =
            @"SELECT COUNT(*) FROM Appointments  WHERE cast (AppointmentDate as Date)  BETWEEN @StartDate AND @EndDate";

        return await connection.ExecuteScalarAsync<int>(sql,
            new { StartDate = startDate, EndDate = endDate });
    }

    #region Doctor Appointment Availability

    // public async Task<AvailableSlotsDto> GetDoctorAvailableAppointmentTimesAsync(int doctorId)
    // {
    //     using var connection = _dbContext.Connection();
    //     var doctorWorkingHour = await _doctorRepository.GetDoctorWorkingHourAsync(doctorId);
    //     return doctorWorkingHour; 
    // }

    #endregion


    #region Extra

    async Task<IEnumerable<int>> IAppointmentRepository.GetPastScheduledAppointmentsAsync()
    {
        using var connection = _dbContext.Connection();
        var query = "select Id from Appointments where Status= @Status AND AppointmentDate <= @CurrentDate ";
        var result = await connection.QueryAsync<int>(query,
            new { Status = AppointmentStatus.Scheduled, CurrentDate = DateTime.UtcNow });
        return result;
    }

    async Task<bool> IAppointmentRepository.MarkAppointmentsAsNotShownAsync()
    {
        using var connection = _dbContext.Connection();
        var query =
            "Update Appointments Set Status=@NewStatus where Status=@OldStatus and AppointmentDate <= @CurrentDate";
        var result = await connection.ExecuteAsync(query,
            new
            {
                OldStatus = AppointmentStatus.Scheduled, NewStatus = AppointmentStatus.NotShown,
                CurrentDate = DateTime.UtcNow
            });
        return result > 0;
    }

    public async Task<IEnumerable<Appointment>> GetAppointmentsByDateAsync(string type, DateTime date)
    {
        using var connection = _dbContext.Connection();
        var sql = string.Empty;
        var today = DateTime.Now;
        switch (type.ToLower())
        {
            case "past":
                sql = "Select * from Appointments where AppointmentDate<@Date  ";
                return await connection.QueryAsync<Appointment>(sql, new { Date = date });
            case "future":
                sql = "Select * from Appointments where AppointmentDate>@Date";
                return await connection.QueryAsync<Appointment>(sql, new { Date = date });
            case "today":
            default:
                sql = "Select * from Appointments where CAST (AppointmentDate as DATE) =@Date";
                return await connection.QueryAsync<Appointment>(sql, new { Date = date });
        }
    }

    private Task<IEnumerable<SlotDto>> GetAvailableSlotAsync(int doctorId)
    {
        using var connection = _dbContext.Connection();
        var sql = " ";

        throw new NotImplementedException();
    }

    #endregion

    #region Curd Operations

    async Task<Appointment> IAppointmentRepository.AddAsync(Appointment appointment)
    {
        using var connection = _dbContext.Connection();
        var sql =
            "INSERT INTO Appointments (PatientId, DoctorId, AppointmentDate, Status, StartTime, EndTime) VALUES (@PatientId, @DoctorId, @AppointmentDate, @Status, @StartTime, @EndTime)";
        var result = await connection.ExecuteAsync(sql, appointment);
        return appointment;
    }

    async Task<IEnumerable<Appointment>> IAppointmentRepository.GetAllAsync()
    {
        using var connection = _dbContext.Connection();
        var sql = "Select * from Appointments";
        var result = await connection.QueryAsync<Appointment>(sql);
        return result;
    }

    async Task<Appointment> IAppointmentRepository.GetByIdAsync(int id)
    {
        using var connection = _dbContext.Connection();
        var sql = "Select * from Appointments where Id =@Id";
        var result = await connection.QueryFirstOrDefaultAsync<Appointment>(sql, new { Id = id });
        return result;
    }

    async Task<bool> IAppointmentRepository.UpdateAsync(Appointment appointment)
    {
        using var connection = _dbContext.Connection();
        var sql =
            "UPDATE Appointments SET  PatientId = @PatientId,   DoctorId = @DoctorId,  AppointmentDate = @AppointmentDate, Status = @Status, StartTime = @StartTime,  EndTime = @EndTime WHERE Id = @Id";
        var result = await connection.ExecuteAsync(sql, appointment);
        return result > 0;
    }

    async Task<bool> IAppointmentRepository.UpdateStatusAsync(int id)
    {
        using var connection = _dbContext.Connection();
        var sql = "UPDATE Appointments SET Status = @Status WHERE Id = @Id";
        var result = await connection.ExecuteAsync(sql, new { Status = AppointmentStatus.Cancelled, Id = id });
        return result > 0;
    }

    #endregion

    #region Availability Checks

    async Task<bool> IAppointmentRepository.CheckAvailability(int doctorId, DateTime appointmentDate,
        TimeSpan startTime, TimeSpan endTime)
    {
        using var connection = _dbContext.Connection();
        var query = @"SELECT COUNT(*) 
                      FROM Appointments 
                      WHERE DoctorId = @DoctorId 
                        AND AppointmentDate = @AppointmentDate
                        AND StartTime < @EndTime
                        AND EndTime > @StartTime";

        var result =
            await connection.ExecuteScalarAsync<int>(query, new { doctorId, startTime, endTime, appointmentDate });
        return result == 0;
    }

    async Task<bool> IAppointmentRepository.IsTimeSlotSpacedAsync(int doctorId, DateTime appointmentDate,
        TimeSpan startTime)
    {
        using var connection = _dbContext.Connection();
        var query =
            "SELECT COUNT(*) FROM Appointments WHERE DoctorId = @DoctorId AND AppointmentDate = @AppointmentDate AND ABS(DATEDIFF(MINUTE, StartTime, @StartTime)) < 30";
        var result = await connection.ExecuteScalarAsync<int>(query, new { doctorId, appointmentDate, startTime });
        return result == 0;
    }

    #endregion

    #region Pre -validation

    async Task<bool> IAppointmentRepository.BlockBookingOnSundayAsync(DateTime appointmentDate)
    {
        using var connection = _dbContext.Connection();
        var query = @" SELECT CASE 
           WHEN DATEPART(WEEKDAY, @AppointmentDate) = 1 THEN 1  ELSE 0  END";

        var parameters = new { AppointmentDate = appointmentDate };
        var result = await connection.ExecuteScalarAsync<int>(query, parameters);
        return result == 1;
    }

    async Task<int> IAppointmentRepository.CheckNumberOfBookingOfPatient(int patientId, DateTime appointmentDate)
    {
        using var connection = _dbContext.Connection();
        var query =
            "SELECT COUNT(*) FROM Appointments WHERE PatientId = @PatientId AND CAST(AppointmentDate AS DATE) = CAST(@AppointmentDate AS DATE)";
        var result = await connection.ExecuteScalarAsync<int>(query,
            new { PatientId = patientId, AppointmentDate = appointmentDate });
        return result;
    }

    async Task<bool> IAppointmentRepository.BlockBookingOutOfDate(DateTime appointmentDate)
    {
        using var connection = _dbContext.Connection();
        var query = "select " +
                    "CASE WHEN DATEDIFF(DAY, GETDATE(), @AppointmentDate) > 30 THEN 1 ELSE 0 END ";

        var result = await connection.ExecuteScalarAsync<int>(query, new { AppointmentDate = appointmentDate });
        return result == 1; // return if condition is true 
    }

    async Task<bool> IAppointmentRepository.BookingDateValidationAsync(DateTime appointemntDate)
    {
        using var connection = _dbContext.Connection();
        var query = "SELECT CASE  WHEN DATEDIFF(HOUR, GETDATE(), @AppointmentDate) <3 THEN 1  ELSE 0 END";
        var result = await connection.ExecuteScalarAsync<int>(query, new { AppointmentDate = appointemntDate });
        return result == 1;
    }

    #endregion
}