
using System.Threading.Tasks;
using System.Collections.Generic;
using Dapper;
using ConfigurationLibrary.Models;
using Microsoft.Data.SqlClient;

namespace ConfigurationLibrary.Repositories
{
    //public class SqlConfigurationRepository : IConfigurationRepository
    //{
    //    private readonly string _connectionString;

    //    public SqlConfigurationRepository(string connectionString)
    //    {
    //        _connectionString = connectionString;
    //    }

    //    public async Task<IEnumerable<ConfigurationItem>> GetConfigurations(string applicationName)
    //    {
    //        using var connection = new SqlConnection(_connectionString);

    //        var query = @"SELECT * FROM Configurations 
    //                      WHERE ApplicationName = @ApplicationName 
    //                      AND IsActive = 1";

    //        return await connection.QueryAsync<ConfigurationItem>(query, new { ApplicationName = applicationName });
    //    }
    //}
}
