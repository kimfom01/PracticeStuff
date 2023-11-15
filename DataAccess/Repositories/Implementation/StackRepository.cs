using System.Data;
using System.Data.SqlClient;
using Dapper;
using DataAccess.Config;
using DataAccess.Dtos;
using DataAccess.Models;

namespace DataAccess.Repositories.Implementation;

public class StackRepository : IStackRepository
{
    private readonly Configuration _configuration;

    public StackRepository(Configuration configuration)
    {
        _configuration = configuration;
    }

    public async Task CreateStackTable()
    {
        using IDbConnection connection = new SqlConnection(_configuration.ConnectionString);

        var sql = """
                    IF OBJECT_ID(N'Stack', N'U') IS NULL
                    CREATE TABLE Stack (
                    Id INT PRIMARY KEY IDENTITY(1,1),
                    Name NVARCHAR(50) UNIQUE)
                  """;

        await connection.ExecuteAsync(sql);
    }

    public async Task<int> AddNewStack(Stack newStack)
    {
        using IDbConnection connection = new SqlConnection(_configuration.ConnectionString);

        var sql = """
                      INSERT INTO Stack (Name)
                      VALUES(@stackName)
                  """;

        object[] parameters =
        {
            new { stackName = newStack.Name }
        };

        return await connection.ExecuteAsync(sql, parameters);
    }

    public async Task<int> UpdateStack(Stack oldStack, Stack newStack)
    {
        using IDbConnection connection = new SqlConnection(_configuration.ConnectionString);

        var sql = """
                    UPDATE Stack
                    SET Name = @newName
                    WHERE Name = @oldName
                  """;

        object[] parameters =
        {
            new { newName = newStack.Name, oldName = oldStack.Name }
        };

        return await connection.ExecuteAsync(sql, parameters);
    }

    public async Task<int> DeleteStack(Stack stackToDelete)
    {
        using IDbConnection connection = new SqlConnection(_configuration.ConnectionString);

        var sql = """
                      DELETE FROM Stack
                      WHERE Name = @stackName
                  """;

        object[] parameters =
        {
            new { stackName = stackToDelete.Name }
        };

        return await connection.ExecuteAsync(sql, parameters);
    }

    public async Task<IEnumerable<StackDto>> GetStacks()
    {
        using IDbConnection connection = new SqlConnection(_configuration.ConnectionString);

        var sql = "SELECT * FROM Stack";

        var result = await connection.QueryAsync<StackDto>(sql);

        return result;
    }

    public async Task<int> GetStackId(Stack stack)
    {
        using IDbConnection connection = new SqlConnection(_configuration.ConnectionString);

        var sql = """
                    SELECT Id FROM Stack
                    WHERE Name = @stackName
                  """;

        var parameters = new DynamicParameters(new Dictionary<string, object?>
        {
            { "@stackName", stack.Name }
        });

        try
        {
            var foundStack = await connection.QueryFirstAsync<Stack>(sql, parameters);
            return foundStack.Id;
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
            throw;
        }
    }
}