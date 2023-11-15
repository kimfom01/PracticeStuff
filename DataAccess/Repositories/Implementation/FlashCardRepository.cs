using System.Data;
using System.Data.SqlClient;
using Dapper;
using DataAccess.Config;
using DataAccess.Dtos;
using DataAccess.Models;

namespace DataAccess.Repositories.Implementation;

public class FlashCardRepository : IFlashCardRepository
{
    private readonly Configuration _configuration;
    private readonly IStackRepository _stackRepository;

    public FlashCardRepository(
        Configuration configuration,
        IStackRepository stackRepository
    )
    {
        _stackRepository = stackRepository;
        _configuration = configuration;
    }

    public async Task CreateFlashCardTable()
    {
        using IDbConnection connection = new SqlConnection(_configuration.ConnectionString);

        var sql = """
                    IF OBJECT_ID(N'FlashCard', N'U') IS NULL
                    CREATE TABLE FlashCard (Id INT PRIMARY KEY IDENTITY(1,1),
                    StackId INT FOREIGN KEY REFERENCES Stack(Id) ON DELETE CASCADE,
                    Name NVARCHAR(50),
                    Content NVARCHAR(500))
                  """;

        await connection.ExecuteAsync(sql);
    }


    public async Task<int> AddNewFlashCard(FlashCard flashCard, Stack stack)
    {
        var stackId = _stackRepository.GetStackId(stack);
        using IDbConnection connection = new SqlConnection(_configuration.ConnectionString);

        var sql = """
                    INSERT INTO FlashCard (StackId, Name, Content)
                    VALUES (@stackId, @flashCardName, @flashCardContent)
                  """;

        object[] parameters =
        {
            new { stackId, flashCardName = flashCard.Name, flashCardContent = flashCard.Content }
        };
        return await connection.ExecuteAsync(sql, parameters);
    }

    public async Task<int> UpdateFlashCard(FlashCard oldFlashCard, FlashCard newFlashCard, Stack stack)
    {
        var stackId = _stackRepository.GetStackId(stack);
        using IDbConnection connection = new SqlConnection(_configuration.ConnectionString);

        var sql = """
                    UPDATE FlashCard
                    SET Name = @newFlashCardName, Content = @newFlashCardContent
                    WHERE Name = @oldFlashCardName AND StackId = @stackId
                  """;

        object[] parameters =
        {
            new
            {
                stackId, newFlashCardName = newFlashCard.Name, newFlashCardContent = newFlashCard.Content,
                oldFlashCardName = oldFlashCard.Name
            }
        };

        return await connection.ExecuteAsync(sql, parameters);
    }

    public async Task<int> UpdateFlashCardName(FlashCard oldFlashCard, FlashCard newFlashCard, Stack stack)
    {
        var stackId = _stackRepository.GetStackId(stack);
        using IDbConnection connection = new SqlConnection(_configuration.ConnectionString);

        var sql = """
                    UPDATE FlashCard
                    SET Name = @newFlashCardName
                    WHERE Name = @oldFlashCardName
                    AND StackId = @stackId
                  """;

        object[] parameters =
        {
            new
            {
                stackId, newFlashCardName = newFlashCard.Name, oldFlashCardName = oldFlashCard.Name
            }
        };

        return await connection.ExecuteAsync(sql, parameters);
    }

    public async Task<int> UpdateFlashCardContent(FlashCard oldFlashCard, FlashCard newFlashCard, Stack stack)
    {
        var stackId = _stackRepository.GetStackId(stack);
        using IDbConnection connection = new SqlConnection(_configuration.ConnectionString);

        var sql = """
                    UPDATE FlashCard
                    SET Content = @newFlashCardContent
                    WHERE Name = @oldFlashCardName
                    AND StackId = @stackId
                  """;

        object[] parameters =
        {
            new
            {
                stackId, newFlashCardContent = newFlashCard.Content, oldFlashCardName = oldFlashCard.Name
            }
        };

        return await connection.ExecuteAsync(sql, parameters);
    }

    public async Task<int> DeleteFlashCard(FlashCard flashCardToDelete, Stack stack)
    {
        var stackId = _stackRepository.GetStackId(stack);
        using IDbConnection connection = new SqlConnection(_configuration.ConnectionString);

        var sql = """
                    DELETE FROM FlashCard
                    WHERE Name = @flashCardToDeleteName
                    AND StackId = @stackId
                  """;

        object[] parameters =
        {
            new
            {
                stackId, flashCardToDeleteName = flashCardToDelete.Name
            }
        };

        return await connection.ExecuteAsync(sql, parameters);
    }

    public async Task<IEnumerable<FlashCardDto>> GetFlashCardsOfStack(Stack stack)
    {
        var stackId = await _stackRepository.GetStackId(stack);

        using IDbConnection connection = new SqlConnection(_configuration.ConnectionString);

        var sql = """
                    SELECT * FROM FlashCard
                    WHERE StackId = @stackId
                  """;

        // var parameters = new DynamicParameters(new Dictionary<string, object>
        // {
        //     { "@stackId", stackId }
        // });

        // var parameters = ;

        IEnumerable<FlashCard> flashCards;

        try
        {
            flashCards = await connection.QueryAsync<FlashCard>(sql, new { stackId });
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }

        return flashCards.Select(MapFlashCardToDto).ToList();
    }

    private FlashCardDto MapFlashCardToDto(FlashCard flashCard)
    {
        return new FlashCardDto
        {
            Id = flashCard.Id,
            Name = flashCard.Name,
            Content = flashCard.Content
        };
    }
}