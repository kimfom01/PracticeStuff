using System.Data.SqlClient;
using FlashCardApp.Config;
using FlashCardApp.DTO;
using FlashCardApp.Models;

namespace FlashCardApp.Data.Implementation;

public class StudyAreaDataManager : IStudyAreaDataManager
{
    private readonly Configuration _configuration;
    private readonly IStackDataManager _stackDataManager;
    public StudyAreaDataManager(
        Configuration configuration,
    IStackDataManager stackDataManager)
    {
        _configuration = configuration;
        _stackDataManager = stackDataManager;
    }

    public void CreateStudyAreaTable()
    {
        using var connection = new SqlConnection(_configuration.ConnectionString);
        using var command = connection.CreateCommand();

        connection.Open();

        command.CommandText = "IF OBJECT_ID(N'StudyArea', N'U') IS NULL " +
                              "CREATE TABLE StudyArea (Id INT PRIMARY KEY IDENTITY(1,1), " +
                              "StackId INT FOREIGN KEY REFERENCES Stack(Id) ON DELETE CASCADE, " +
                              "Date DATE DEFAULT GETDATE(), " +
                              "Score INT)";

        command.ExecuteNonQuery();
    }

    public void SaveScore(StudyArea studyArea, Stack stack)
    {
        var stackId = _stackDataManager.GetStackId(stack);

        using var connection = new SqlConnection(_configuration.ConnectionString);
        using var command = connection.CreateCommand();
        connection.Open();

        command.CommandText = "INSERT INTO StudyArea(StackId, Score) " +
                              $"VALUES (@stackId, @studyAreaScore)";

        command.Parameters.Add(new SqlParameter("@stackId", stackId));
        command.Parameters.Add(new SqlParameter("@studyAreaScore", studyArea.Score));

        command.ExecuteNonQuery();
    }

    public List<StudyAreaDto> GetScoresHistory()
    {
        List<StudyAreaDto> history = new();

        using var connection = new SqlConnection(_configuration.ConnectionString);
        using var command = connection.CreateCommand();
        connection.Open();

        command.CommandText = "SELECT SA.Date, SA.Score, St.Name " +
                              "FROM StudyArea AS SA " +
                              "LEFT JOIN Stack AS St " +
                              "ON SA.StackId = St.Id";

        var reader = command.ExecuteReader();

        while (reader.Read())
        {
            history.Add(new StudyAreaDto
            {
                Date = (DateTime)reader["Date"],
                Score = (int)reader["Score"],
                Stack = (string)reader["Name"]
            });
        }

        return history;
    }
}
