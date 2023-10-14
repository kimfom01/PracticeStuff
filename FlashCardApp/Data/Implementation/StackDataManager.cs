using System.Data.SqlClient;
using FlashCardApp.DTO;
using FlashCardApp.Models;

namespace FlashCardApp.Data.Implementation;

public class StackDataManager : IStackDataManager
{
    private readonly string _connectionString;

    public StackDataManager(string connectionString)
    {
        _connectionString = connectionString;
    }

    public void CreateStackTable()
    {
        using var connection = new SqlConnection(_connectionString);
        using var command = connection.CreateCommand();

        connection.Open();

        command.CommandText = "IF OBJECT_ID(N'Stack', N'U') IS NULL " +
                              "CREATE TABLE Stack (Id INT PRIMARY KEY IDENTITY(1,1), " +
                              "Name NVARCHAR(50) UNIQUE)";

        command.ExecuteNonQuery();
    }

    public void AddNewStack(Stack newStack)
    {
        using var connection = new SqlConnection(_connectionString);
        using var command = connection.CreateCommand();

        connection.Open();

        command.CommandText = """
                                INSERT INTO Stack (Name)
                                VALUES(@stackName)
                            """;

        command.Parameters.Add(new SqlParameter("@stackName", newStack.Name));

        command.ExecuteNonQuery();
    }

    public void UpdateStack(Stack oldStack, Stack newStack)
    {
        using var connection = new SqlConnection(_connectionString);
        using var command = connection.CreateCommand();

        connection.Open();

        command.CommandText = """
                                UPDATE Stack 
                                SET Name = @newName 
                                WHERE Name = @oldName
                              """;

        command.Parameters.Add(new SqlParameter("@newName", newStack.Name));
        command.Parameters.Add(new SqlParameter("@oldName", oldStack.Name));

        command.ExecuteNonQuery();
    }

    public void DeleteStack(Stack stackToDelete)
    {
        using var connection = new SqlConnection(_connectionString);
        using var command = connection.CreateCommand();

        connection.Open();

        command.CommandText = """
                                DELETE FROM Stack
                                WHERE Name = @stackName
                            """;

        command.Parameters.Add(new SqlParameter("@stackName", stackToDelete.Name));

        command.ExecuteNonQuery();
    }

    public List<StackDTO> GetStacks()
    {
        List<StackDTO> stackList = new();

        using var connection = new SqlConnection(_connectionString);
        using var command = connection.CreateCommand();

        connection.Open();

        command.CommandText = "SELECT * FROM Stack";

        var reader = command.ExecuteReader();

        while (reader.Read())
        {
            stackList.Add(new StackDTO
            {
                StackName = reader.GetString(1) // The second ordinal is StackName column
            });
        }

        return stackList;
    }

    public int GetStackId(Stack stack)
    {
        int id = -1;

        using var connection = new SqlConnection(_connectionString);
        using var command = connection.CreateCommand();

        connection.Open();

        command.CommandText = """
                                SELECT Id FROM Stack 
                                WHERE Name = @stackName
                              """;

        command.Parameters.Add(new SqlParameter("@stackName", stack.Name));

        var reader = command.ExecuteReader();

        if (reader.HasRows)
        {
            while (reader.Read())
            {
                id = (int)reader["Id"];
            }
        }

        return id;
    }
}
