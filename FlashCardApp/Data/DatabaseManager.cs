using System.Configuration;
using System.Data.SqlClient;
using FlashCardApp.DTO;
using FlashCardApp.Models;

namespace FlashCardApp.Data;

public class DatabaseManager
{
    private string _connectionString = ConfigurationManager.AppSettings.Get("connectionString")!;

    public void CreateStackTable()
    {
        using (var connection = new SqlConnection(_connectionString))
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

    public void CreateFlashCardTable()
    {
        using (var connection = new SqlConnection(_connectionString))
        {
            using (var command = connection.CreateCommand())
            {
                connection.Open();

                command.CommandText = "IF OBJECT_ID(N'FlashCard', N'U') IS NULL " +
                                      "CREATE TABLE FlashCard (Id INT PRIMARY KEY IDENTITY(1,1), " +
                                      "StackId INT FOREIGN KEY REFERENCES Stack(Id) ON DELETE CASCADE, " +
                                      "FlashCardName NVARCHAR(50), " +
                                      "FrontContent NVARCHAR(500), " +
                                      "BackContent NVARCHAR(500))";

                command.ExecuteNonQuery();
            }
        }
    }

    public void CreateStudyAreaTable()
    {
        using (var connection = new SqlConnection(_connectionString))
        {
            using (var command = connection.CreateCommand())
            {
                connection.Open();

                command.CommandText = "IF OBJECT_ID(N'StudyArea', N'U') IS NULL " +
                                      "CREATE TABLE StudyArea (Id INT PRIMARY KEY IDENTITY(1,1), " +
                                      "StackId INT FOREIGN KEY REFERENCES Stack(Id) ON DELETE CASCADE, " +
                                      "Date DATE DEFAULT GETDATE(), " +
                                      "Score INT)";

                command.ExecuteNonQuery();
            }
        }
    }


    // Stack Operations
    public void AddNewStack(Stack newStack)
    {
        using (var connection = new SqlConnection(_connectionString))
        {
            using (var command = connection.CreateCommand())
            {
                connection.Open();

                command.CommandText = "INSERT INTO Stack (StackName) " +
                                      $"VALUES ('{newStack.StackName}')";

                command.ExecuteNonQuery();
            }
        }
    }

    public void UpdateStack(Stack oldStack, Stack newStack)
    {
        using (var connection = new SqlConnection(_connectionString))
        {
            using (var command = connection.CreateCommand())
            {
                connection.Open();

                command.CommandText = "UPDATE Stack " +
                                      $"SET StackName = '{newStack.StackName}' " +
                                      $"WHERE StackName = '{oldStack.StackName}'";

                command.ExecuteNonQuery();
            }
        }
    }

    public void DeleteStack(Stack stackToDelete)
    {
        using (var connection = new SqlConnection(_connectionString))
        {
            using (var command = connection.CreateCommand())
            {
                connection.Open();

                command.CommandText = "DELETE FROM Stack " +
                                      $"WHERE StackName = '{stackToDelete.StackName}'";

                command.ExecuteNonQuery();
            }
        }
    }

    public List<StackDTO> GetStacks()
    {
        List<StackDTO> stackList = new();
        using (var connection = new SqlConnection(_connectionString))
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

    private int GetStackId(Stack stack)
    {
        int id = -1;
        using (var connection = new SqlConnection(_connectionString))
        {
            using (var command = connection.CreateCommand())
            {
                connection.Open();

                command.CommandText = "SELECT Id FROM Stack " +
                                      $"WHERE StackName = {stack.StackName}";

                var reader = command.ExecuteReader();

                if (reader.HasRows)
                {
                    id = reader.GetInt32(0);
                }
            }
        }

        return id;
    }

    // FlashCard Operations
    public void AddNewFlashCard(FlashCard flashCard, Stack stack)
    {
        var stackId = GetStackId(stack);
        using (var connection = new SqlConnection(_connectionString))
        {
            using (var command = connection.CreateCommand())
            {
                connection.Open();

                command.CommandText = "INSERT INTO FlashCard (StackId, FlashCardName, FrontContent, BackContent) " +
                                      $"VALUES ({stackId}, '{flashCard.FlashCardName}','{flashCard.FrontContent}','{flashCard.BackContent}') ";

                command.ExecuteNonQuery();
            }
        }
    }

    public void UpdateFlashCard(FlashCard oldFlashCard, FlashCard newFlashCard)
    {
        using (var connection = new SqlConnection(_connectionString))
        {
            using (var command = connection.CreateCommand())
            {
                connection.Open();

                command.CommandText = "UPDATE FlashCard " +
                                      $"SET FlashCardName = '{newFlashCard.FlashCardName}', " +
                                      $"FrontContent = '{newFlashCard.FrontContent}' " +
                                      $"BackContent = '{newFlashCard.BackContent}' " +
                                      $"WHERE FlashCardName = '{oldFlashCard.FlashCardName}'";

                command.ExecuteNonQuery();
            }
        }
    }

    public void UpdateFlashCardName(FlashCard oldFlashCard, FlashCard newFlashCard)
    {
        using (var connection = new SqlConnection(_connectionString))
        {
            using (var command = connection.CreateCommand())
            {
                connection.Open();

                command.CommandText = "UPDATE FlashCard " +
                                      $"SET FlashCardName = '{newFlashCard.FlashCardName}', " +
                                      $"WHERE FlashCardName = '{oldFlashCard.FlashCardName}'";

                command.ExecuteNonQuery();
            }
        }
    }

    public void UpdateFlashCardFront(FlashCard oldFlashCard, FlashCard newFlashCard)
    {
        using (var connection = new SqlConnection(_connectionString))
        {
            using (var command = connection.CreateCommand())
            {
                connection.Open();

                command.CommandText = "UPDATE FlashCard " +
                                      $"SET FrontContent = '{newFlashCard.FrontContent}' " +
                                      $"WHERE FlashCardName = '{oldFlashCard.FlashCardName}'";

                command.ExecuteNonQuery();
            }
        }
    }

    public void UpdateFlashCardBack(FlashCard oldFlashCard, FlashCard newFlashCard)
    {
        using (var connection = new SqlConnection(_connectionString))
        {
            using (var command = connection.CreateCommand())
            {
                connection.Open();

                command.CommandText = "UPDATE FlashCard " +
                                      $"SET BackContent = '{newFlashCard.BackContent}' " +
                                      $"WHERE FlashCardName = '{oldFlashCard.FlashCardName}'";

                command.ExecuteNonQuery();
            }
        }
    }

    public void DeleteFlashCard(FlashCard flashCardToDelete)
    {
        using (var connection = new SqlConnection(_connectionString))
        {
            using (var command = connection.CreateCommand())
            {
                connection.Open();

                command.CommandText = "DELETE FROM FlashCard " +
                                      $"WHERE FlashCardName = '{flashCardToDelete.FlashCardName}'";

                command.ExecuteNonQuery();
            }
        }
    }

    public List<FlashCardDTO> GetFlashCards()
    {
        List<FlashCardDTO> flashCardList = new();

        using (var connection = new SqlConnection(_connectionString))
        {
            using (var command = connection.CreateCommand())
            {
                connection.Open();

                command.CommandText = "SELECT * FROM FlashCard";

                var reader = command.ExecuteReader();

                while (reader.Read())
                {
                    flashCardList.Add(new FlashCardDTO
                    {
                        Name = reader.GetString(2), // The second ordinal is FlashCardName column
                        FrontContent = reader.GetString(3), // The third ordinal is FrontContent column
                        BackContent = reader.GetString(4) // The fourth ordinal is BackContent column
                    });
                }
            }
        }

        return flashCardList;
    }
}