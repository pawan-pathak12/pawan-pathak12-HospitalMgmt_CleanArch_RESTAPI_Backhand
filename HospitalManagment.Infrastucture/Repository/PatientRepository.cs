using Dapper;
using HospitalManagment.Application.Interfaces;
using HospitalManagment.Domain.Entity;
using HospitalManagment.Infrastucture.Data;

namespace HospitalManagment.Infrastucture.Repository;

public class PatientRepository : IPatientRepository
{
    private readonly DapperDbContext _dapperDbContext;

    public PatientRepository(DapperDbContext dapperDbContext)
    {
        _dapperDbContext = dapperDbContext;
    }

    #region CURD Operations

    async Task<Patient> IPatientRepository.AddAsync(Patient patient)
    {
        using var connection = _dapperDbContext.Connection();
        var sql =
            "INSERT INTO Patients (FullName, Age, Gender, Address, Email, PhoneNumber) VALUES  (@FullName, @Age, @Gender, @Address, @Email, @PhoneNumber)";
        await connection.ExecuteAsync(sql, patient);
        return patient;
    }

    async Task<bool> IPatientRepository.DeleteAsync(int id)
    {
        using var connection = _dapperDbContext.Connection();
        var sql = "Delete from Patients where Id=@Id";
        var result = await connection.ExecuteAsync(sql, new { Id = id });
        return result > 0;
    }

    public async Task<IEnumerable<Patient>> GetAppointmentsByDateAsync(string type, DateTime date)
    {
        using var connection = _dapperDbContext.Connection();
        var sql = string.Empty;
        var today = DateTime.Now;
        switch (type.ToLower())
        {
            case "past":
                sql =
                    "SELECT Patients.* , cast ( Appointments.AppointmentDate as Date) FROM Patients JOIN Appointments ON Patients.Id = Appointments.PatientId WHERE Appointments.AppointmentDate < CAST(@Date AS DATE)";
                return await connection.QueryAsync<Patient>(sql, new { Date = date });

            case "future":
                sql =
                    "SELECT Patients.* , cast ( Appointments.AppointmentDate as Date) FROM Patients JOIN Appointments ON Patients.Id = Appointments.PatientId WHERE Appointments.AppointmentDate > CAST(@Date AS DATE)";
                return await connection.QueryAsync<Patient>(sql, new { Date = date });

            case "today":
            default:
                sql =
                    "SELECT Patients.*  , cast ( Appointments.AppointmentDate as Date)FROM Patients JOIN Appointments ON Patients.Id = Appointments.PatientId WHERE Appointments.AppointmentDate = CAST(@Date AS DATE)";
                return await connection.QueryAsync<Patient>(sql, new { Date = date });
        }
    }

    async Task<IEnumerable<Patient>> IPatientRepository.GetAllAsync()
    {
        using var connection = _dapperDbContext.Connection();
        var sql = "Select * from Patients";
        var result = await connection.QueryAsync<Patient>(sql);
        return result;
    }

    async Task<Patient> IPatientRepository.GetByIdAsync(int id)
    {
        using var connection = _dapperDbContext.Connection();
        var sql = "Select * from Patients where Id=@Id";
        var result = await connection.QueryFirstOrDefaultAsync<Patient>(sql, new { Id = id });
        return result;
    }

    async Task<bool> IPatientRepository.UpdateAsync(int id, Patient patient)
    {
        using var connection = _dapperDbContext.Connection();
        var sql =
            " UPDATE Patients SET FullName = @FullName,Age = @Age,  Gender = @Gender, Address = @Address,Email = @Email, PhoneNumber = @PhoneNumber WHERE Id = @Id";
        var result = await connection.ExecuteAsync(sql,
            new
            {
                Id = id, patient.Address, patient.Age, patient.Email, patient.FullName, patient.Gender,
                patient.PhoneNumber
            });
        return result > 0;
    }

    #endregion

    #region Extra logic

    async Task<IEnumerable<Patient>> IPatientRepository.GetByNameAsync(string name)
    {
        using var connection = _dapperDbContext.Connection();
        var query = "Select * from Patients where Lower(FullName) like Lower(@FullName)";

        return await connection.QueryAsync<Patient>(query, new { FullName = $"%{name}%" });
    }

    async Task IPatientRepository.SetPatientBlockUntilAsync(DateTime blockedDate, int patientId)
    {
        using var connection = _dapperDbContext.Connection();
        var query = "update Patients Set BlockedDate=@BlockedDate where Id=@PatientId ";
        await connection.ExecuteAsync(query, new { blockedDate, Id = patientId });
    }

    #endregion
}