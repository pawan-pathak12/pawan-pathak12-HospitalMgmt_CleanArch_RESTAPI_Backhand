using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System.Data;

namespace HospitalManagment.Infrastucture.Data;

public class DapperDbContext
{
    private readonly IConfiguration _configuration;
    public readonly string _connectionString;

    public DapperDbContext(IConfiguration configuration)
    {
        _configuration = configuration;
        _connectionString = _configuration.GetConnectionString("DefaultConnection");
    }

    public IDbConnection Connection()
    {
        return new SqlConnection(_configuration.GetConnectionString("DefaultConnection"));
    }
}
