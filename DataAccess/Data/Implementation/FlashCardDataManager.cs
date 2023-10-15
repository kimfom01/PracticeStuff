using System.Data.SqlClient;
using DataAccess.Config;
using DataAccess.DTO;
using DataAccess.Models;

namespace DataAccess.Data.Implementation;

public class FlashCardDataManager : IFlashCardDataManager
{
    private readonly Configuration _configuration;
    private readonly IStackDataManager _stackDataManager;

    public FlashCardDataManager(
    Configuration configuration,
    IStackDataManager stackDataManager
    )
    {
        _stackDataManager = stackDataManager;
        _configuration = configuration;
    }

    public void CreateFlashCardTable()
    {
        using var connection = new SqlConnection(_configuration.ConnectionString);
        using var command = connection.CreateCommand();

        connection.Open();

        command.CommandText = """
                                IF OBJECT_ID(N'FlashCard', N'U') IS NULL
                                CREATE TABLE FlashCard (Id INT PRIMARY KEY IDENTITY(1,1),
                                StackId INT FOREIGN KEY REFERENCES Stack(Id) ON DELETE CASCADE,
                                Name NVARCHAR(50),
                                Content NVARCHAR(500))
                              """;

        command.ExecuteNonQuery();
    }


    public void AddNewFlashCard(FlashCard flashCard, Stack stack)
    {
        var stackId = _stackDataManager.GetStackId(stack);
        using var connection = new SqlConnection(_configuration.ConnectionString);
        using var command = connection.CreateCommand();
        connection.Open();

        command.CommandText = """
                                INSERT INTO FlashCard (StackId, Name, Content)
                                VALUES (@stackId, @flashCardName, @flashCardContent)
                              """;

        command.Parameters.Add(new SqlParameter("@stackId", stackId));
        command.Parameters.Add(new SqlParameter("@flashCardName", flashCard.Name));
        command.Parameters.Add(new SqlParameter("@flashCardContent", flashCard.Content));

        command.ExecuteNonQuery();
    }

    public void UpdateFlashCard(FlashCard oldFlashCard, FlashCard newFlashCard, Stack stack)
    {
        var stackId = _stackDataManager.GetStackId(stack);
        using var connection = new SqlConnection(_configuration.ConnectionString);
        using var command = connection.CreateCommand();
        connection.Open();

        command.CommandText = """
                                UPDATE FlashCard 
                                SET Name = @newFlashCardName, Content = @newFlashCardContent
                                WHERE Name = @oldFlashCardName AND StackId = @stackId
                              """;

        command.Parameters.Add(new SqlParameter("@newFlashCardName", newFlashCard.Name));
        command.Parameters.Add(new SqlParameter("@newFlashCardContent", newFlashCard.Content));
        command.Parameters.Add(new SqlParameter("@oldFlashCardName", oldFlashCard.Name));
        command.Parameters.Add(new SqlParameter("@stackId", stackId));

        command.ExecuteNonQuery();
    }

    public void UpdateFlashCardName(FlashCard oldFlashCard, FlashCard newFlashCard, Stack stack)
    {
        var stackId = _stackDataManager.GetStackId(stack);
        using var connection = new SqlConnection(_configuration.ConnectionString);
        using var command = connection.CreateCommand();
        connection.Open();

        command.CommandText = """
                                UPDATE FlashCard
                                SET Name = @newFlashCardName
                                WHERE Name = @oldFlashCardName
                                AND StackId = @stackId
                              """;

        command.Parameters.Add(new SqlParameter("@newFlashCardName", newFlashCard.Name));
        command.Parameters.Add(new SqlParameter("@oldFlashCardName", oldFlashCard.Name));
        command.Parameters.Add(new SqlParameter("@stackId", stackId));

        command.ExecuteNonQuery();
    }

    public void UpdateFlashCardContent(FlashCard oldFlashCard, FlashCard newFlashCard, Stack stack)
    {
        var stackId = _stackDataManager.GetStackId(stack);
        using var connection = new SqlConnection(_configuration.ConnectionString);
        using var command = connection.CreateCommand();
        connection.Open();

        command.CommandText = """
                                UPDATE FlashCard
                                SET Content = @newFlashCardContent
                                WHERE Name = @oldFlashCardName
                                AND StackId = @stackId
                              """;

        command.Parameters.Add(new SqlParameter("@newFlashCardContent", newFlashCard.Content));
        command.Parameters.Add(new SqlParameter("@oldFlashCardName", oldFlashCard.Name));
        command.Parameters.Add(new SqlParameter("@stackId", stackId));

        command.ExecuteNonQuery();
    }

    public void DeleteFlashCard(FlashCard flashCardToDelete, Stack stack)
    {
        var stackId = _stackDataManager.GetStackId(stack);
        using var connection = new SqlConnection(_configuration.ConnectionString);
        using var command = connection.CreateCommand();
        connection.Open();

        command.CommandText = """
                                DELETE FROM FlashCard
                                WHERE Name = @flashCardToDeleteName
                                AND StackId = @stackId
                              """;

        command.Parameters.Add(new SqlParameter("@flashCardToDeleteName", flashCardToDelete.Name));
        command.Parameters.Add(new SqlParameter("@stackId", stackId));

        command.ExecuteNonQuery();
    }

    public List<FlashCardDTO> GetFlashCardsOfStack(Stack stack)
    {
        var stackId = _stackDataManager.GetStackId(stack);
        var id = 0;
        List<FlashCardDTO> flashCardList = new();

        using (var connection = new SqlConnection(_configuration.ConnectionString))
        {
            using var command = connection.CreateCommand();
            connection.Open();

            command.CommandText = """
                                    SELECT * FROM FlashCard 
                                    WHERE StackId = @stackId
                                  """;

            command.Parameters.Add(new SqlParameter("@stackId", stackId));

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

        return flashCardList;
    }

}
