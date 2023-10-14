using System.Data.SqlClient;
using FlashCardApp.DTO;
using FlashCardApp.Models;

namespace FlashCardApp.Data.Implementation;

public class FlashCardDataManager : IFlashCardDataManager
{
    private readonly string _connectionString;
    private readonly IStackDataManager _stackDataManager;

    public FlashCardDataManager(string connectionString, IStackDataManager stackDataManager)
    {
        _connectionString = connectionString;
        _stackDataManager = stackDataManager;
    }

    public void CreateFlashCardTable()
    {
        using var connection = new SqlConnection(_connectionString);
        using var command = connection.CreateCommand();

        connection.Open();

        command.CommandText = "IF OBJECT_ID(N'FlashCard', N'U') IS NULL " +
                              "CREATE TABLE FlashCard (Id INT PRIMARY KEY IDENTITY(1,1), " +
                              "StackId INT FOREIGN KEY REFERENCES Stack(Id) ON DELETE CASCADE, " +
                              "Name NVARCHAR(50), " +
                              "Content NVARCHAR(500))";

        command.ExecuteNonQuery();
    }


    public void AddNewFlashCard(FlashCard flashCard, Stack stack)
    {
        var stackId = _stackDataManager.GetStackId(stack);
        using (var connection = new SqlConnection(_connectionString))
        {
            using (var command = connection.CreateCommand())
            {
                connection.Open();

                command.CommandText = "INSERT INTO FlashCard (StackId, Name, Content) " +
                                      $"VALUES ({stackId}, '{flashCard.Name}','{flashCard.Content}') ";

                command.ExecuteNonQuery();
            }
        }
    }

    public void UpdateFlashCard(FlashCard oldFlashCard, FlashCard newFlashCard, Stack stack)
    {
        var stackId = _stackDataManager.GetStackId(stack);
        using (var connection = new SqlConnection(_connectionString))
        {
            using (var command = connection.CreateCommand())
            {
                connection.Open();

                command.CommandText = "UPDATE FlashCard " +
                                      $"SET Name = '{newFlashCard.Name}', " +
                                      $"Content = '{newFlashCard.Content}' " +
                                      $"WHERE Name = '{oldFlashCard.Name}' " +
                                      $"AND StackId = {stackId}";

                command.ExecuteNonQuery();
            }
        }
    }

    public void UpdateFlashCardName(FlashCard oldFlashCard, FlashCard newFlashCard, Stack stack)
    {
        var stackId = _stackDataManager.GetStackId(stack);
        using (var connection = new SqlConnection(_connectionString))
        {
            using (var command = connection.CreateCommand())
            {
                connection.Open();

                command.CommandText = "UPDATE FlashCard " +
                                      $"SET Name = '{newFlashCard.Name}' " +
                                      $"WHERE Name = '{oldFlashCard.Name}' " +
                                      $"AND StackId = {stackId}";

                command.ExecuteNonQuery();
            }
        }
    }

    public void UpdateFlashCardContent(FlashCard oldFlashCard, FlashCard newFlashCard, Stack stack)
    {
        var stackId = _stackDataManager.GetStackId(stack);
        using (var connection = new SqlConnection(_connectionString))
        {
            using (var command = connection.CreateCommand())
            {
                connection.Open();

                command.CommandText = "UPDATE FlashCard " +
                                      $"SET Content = '{newFlashCard.Content}' " +
                                      $"WHERE Name = '{oldFlashCard.Name}' " +
                                      $"AND StackId = {stackId}";

                command.ExecuteNonQuery();
            }
        }
    }

    public void DeleteFlashCard(FlashCard flashCardToDelete, Stack stack)
    {
        var stackId = _stackDataManager.GetStackId(stack);
        using (var connection = new SqlConnection(_connectionString))
        {
            using (var command = connection.CreateCommand())
            {
                connection.Open();

                command.CommandText = "DELETE FROM FlashCard " +
                                      $"WHERE Name = '{flashCardToDelete.Name}' " +
                                      $"AND StackId = {stackId}";

                command.ExecuteNonQuery();
            }
        }
    }

    public List<FlashCardDTO> GetFlashCardsOfStack(Stack stack)
    {
        var stackId = _stackDataManager.GetStackId(stack);
        var id = 0;
        List<FlashCardDTO> flashCardList = new();

        using (var connection = new SqlConnection(_connectionString))
        {
            using (var command = connection.CreateCommand())
            {
                connection.Open();

                command.CommandText = "SELECT * FROM FlashCard " +
                                      $"WHERE StackId = {stackId}";

                var reader = command.ExecuteReader();

                while (reader.Read())
                {
                    flashCardList.Add(new FlashCardDTO
                    {
                        Id = ++id,
                        Name = reader.GetString(2), // The second ordinal is Name column
                        Content = reader.GetString(3), // The third ordinal is Content column
                    });
                }
            }
        }

        return flashCardList;
    }

}