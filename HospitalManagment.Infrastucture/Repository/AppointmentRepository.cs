using Dapper;
using HospitalManagment.Application.Interfaces;
using HospitalManagment.Domain.Entity;
using HospitalManagment.Domain.Enums;
using HospitalManagment.Infrastucture.Data;

namespace HospitalManagment.Infrastucture.Repository
{
    public class AppointmentRepository : IAppointmentRepository
    {
        private readonly DapperDbContext _dbContext;

        public AppointmentRepository(DapperDbContext dbContext)
        {
            this._dbContext = dbContext;
        }
        #region Curd Operations
        async Task<Appointment> IAppointmentRepository.AddAsync(Appointment appointment)
        {
            using var connection = _dbContext.Connection();
            var sql = "INSERT INTO Appointments (PatientId, DoctorId, AppointmentDate, Status, StartTime, EndTime) VALUES (@PatientId, @DoctorId, @AppointmentDate, @Status, @StartTime, @EndTime)";
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
            var sql = "UPDATE Appointments SET  PatientId = @PatientId,   DoctorId = @DoctorId,  AppointmentDate = @AppointmentDate, Status = @Status, StartTime = @StartTime,  EndTime = @EndTime WHERE Id = @Id";
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


            var result = await connection.QueryAsync<Appointment>(sql, new { DoctorId = doctorId, AppointmentTime=DateTime.UtcNow ,AppointmentDate = DateTime.UtcNow, Status = AppointmentStatus.Scheduled});
            return result;
        }

        #endregion

        #region Availability Checks
        async Task<bool> IAppointmentRepository.CheckAvailability(int doctorId, DateTime appointmentDate, TimeSpan startTime, TimeSpan endTime)
        {
            using var connection = _dbContext.Connection();
            var query = @"SELECT COUNT(*) 
                      FROM Appointments 
                      WHERE DoctorId = @DoctorId 
                        AND AppointmentDate = @AppointmentDate
                        AND StartTime < @EndTime
                        AND EndTime > @StartTime";

            var result = await connection.ExecuteScalarAsync<int>(query, new { doctorId, startTime, endTime, appointmentDate });
            return result == 0;
        }

        async Task<bool> IAppointmentRepository.IsTimeSlotSpacedAsync(int doctorId, DateTime appointmentDate, TimeSpan startTime)
        {
            using var connection = _dbContext.Connection();
            var query = "SELECT COUNT(*) FROM Appointments WHERE DoctorId = @DoctorId AND AppointmentDate = @AppointmentDate AND ABS(DATEDIFF(MINUTE, StartTime, @StartTime)) < 30";
            var result = await connection.ExecuteScalarAsync<int>(query, new { doctorId, appointmentDate, startTime });
            return result == 0;
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
            var query = "SELECT COUNT(*) FROM Appointments WHERE PatientId = @PatientId AND CAST(AppointmentDate AS DATE) = CAST(@AppointmentDate AS DATE)";
            var result = await connection.ExecuteScalarAsync<int>(query, new { PatientId = patientId, AppointmentDate = appointmentDate });
            return result;
        }
        async Task<bool> IAppointmentRepository.BlockBookingOutOfDate(DateTime appointmentDate)
        {
            using var connection = _dbContext.Connection();
            var query = "select " +
                "CASE WHEN DATEDIFF(DAY, GETDATE(), @AppointmentDate) > 30 THEN 1 ELSE 0 END ";

            var result = await connection.ExecuteScalarAsync<int>(query, new { AppointmentDate = appointmentDate });
            return result == 1;   // return if condition is true 
        }
        async Task<bool> IAppointmentRepository.BookingDateValidationAsync(DateTime appointemntDate)
        {
            using var connection = _dbContext.Connection();
            var query = "SELECT CASE  WHEN DATEDIFF(HOUR, GETDATE(), @AppointmentDate) <3 THEN 1  ELSE 0 END";
            var result = await connection.ExecuteScalarAsync<int>(query, new { AppointmentDate = appointemntDate });
            return result == 1;
        }

        #endregion
        async Task<IEnumerable<int>> IAppointmentRepository.GetPastScheduledAppointmentsAsync()
        {
            using var connection = _dbContext.Connection();
            var query = "select Id from Appointments where Status= @Status AND AppointmentDate <= @CurrentDate ";
            var result = await connection.QueryAsync<int>(query, new { Status = AppointmentStatus.Scheduled, CurrentDate = DateTime.UtcNow });
            return result;
        }
        async Task<bool> IAppointmentRepository.MarkAppointmentsAsNotShownAsync()
        {

            using var connection = _dbContext.Connection();
            var query = "Update Appointments Set Status=@NewStatus where Status=@OldStatus and AppointmentDate <= @CurrentDate";
            var result = await connection.ExecuteAsync(query, new { OldStatus = AppointmentStatus.Scheduled, NewStatus = AppointmentStatus.NotShown, CurrentDate = DateTime.UtcNow });
            return result > 0;
        }
    }
}