using System.Configuration;
using System.Data.SqlClient;
using FlashCardApp.DTO;

namespace FlashCardApp.Data;

public sealed class DBManager
{
    private static string connectionString = ConfigurationManager.AppSettings.Get("connectionString");

    public static void CreateStackTable()
    {
        using (var connection = new SqlConnection(connectionString))
        {
            using (var command = connection.CreateCommand())
            {
                connection.Open();

                command.CommandText = "IF OBJECT_ID(N'Stack', N'U') IS NULL " +
                                      "CREATE TABLE Stack (Id INT PRIMARY KEY IDENTITY(1,1), " +
                                      "StackName NVARCHAR(50) UNIQUE)";

                command.ExecuteNonQuery();
            }
        }
    }

    public static void CreateFlashCardTable()
    {
        using (var connection = new SqlConnection(connectionString))
        {
            using (var command = connection.CreateCommand())
            {
                connection.Open();

                command.CommandText = "IF OBJECT_ID(N'FlashCard', N'U') IS NULL " +
                                      "CREATE TABLE FlashCard (Id INT PRIMARY KEY IDENTITY(1,1), " +
                                      "StackId INT FOREIGN KEY REFERENCES Stack(Id), " +
                                      "FlashCardName NVARCHAR(50), " +
                                      "FrontContent NVARCHAR(500), " +
                                      "BackContent NVARCHAR(500))";

                command.ExecuteNonQuery();
            }
        }
    }

    public static void CreateStudyAreaTable()
    {
        using (var connection = new SqlConnection(connectionString))
        {
            using (var command = connection.CreateCommand())
            {
                connection.Open();

                command.CommandText = "IF OBJECT_ID(N'StudyArea', N'U') IS NULL " +
                                      "CREATE TABLE StudyArea (Id INT PRIMARY KEY IDENTITY(1,1), " +
                                      "StackId INT FOREIGN KEY REFERENCES Stack(Id), " +
                                      "Date DATE DEFAULT GETDATE(), " +
                                      "Score INT)";

                command.ExecuteNonQuery();
            }
        }
    }

    
    // Stack Operations
    public static void AddNewStack(string stackName)
    {
        using (var connection = new SqlConnection(connectionString))
        {
            using (var command = connection.CreateCommand())
            {
                connection.Open();

                command.CommandText = "INSERT INTO Stack (StackName) " +
                                      $"VALUES ('{stackName}')";

                command.ExecuteNonQuery();
            }
        }
    }

    public static void UpdateStack(string oldStackName, string newStackName)
    {
        using (var connection = new SqlConnection(connectionString))
        {
            using (var command = connection.CreateCommand())
            {
                connection.Open();

                command.CommandText = "UPDATE Stack " +
                                      $"SET StackName = '{newStackName}' " +
                                      $"WHERE StackName = '{oldStackName}'";

                command.ExecuteNonQuery();
            }
        }
    }

    public static void DeleteStack(string stackName)
    {
        using (var connection = new SqlConnection(connectionString))
        {
            using (var command = connection.CreateCommand())
            {
                connection.Open();

                command.CommandText = "DELETE FROM Stack " +
                                      $"WHERE StackName = '{stackName}'";

                command.ExecuteNonQuery();
            }
        }
    }

    public static List<StackDTO> GetStacks()
    {
        List<StackDTO> stackList = new List<StackDTO>();
        using (var connection = new SqlConnection(connectionString))
        {
            using (var command = connection.CreateCommand())
            {
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
            }
        }

        return stackList;
    }
}