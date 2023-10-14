using System.Data.SqlClient;
using FlashCardApp.DTO;
using FlashCardApp.Models;

namespace FlashCardApp.Data.Implementation;

public class StudyAreaDataManager : IStudyAreaDataManager
{
    private readonly string _connectionString;
    private readonly IStackDataManager _stackDataManager;
    public StudyAreaDataManager(
        string connectionString,
    IStackDataManager stackDataManager)
    {
        _stackDataManager = stackDataManager;
        _connectionString = connectionString;
    }

    public void CreateStudyAreaTable()
    {
        using var connection = new SqlConnection(_connectionString);
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
        using (var connection = new SqlConnection(_connectionString))
        {
            using (var command = connection.CreateCommand())
            {
                connection.Open();

                command.CommandText = "INSERT INTO StudyArea(StackId, Score) " +
                                      $"VALUES ({stackId}, {studyArea.Score})";

                command.ExecuteNonQuery();
            }
        }
    }

    public List<StudyAreaDto> GetScoresHistory()
    {
        List<StudyAreaDto> history = new();
        using (var connection = new SqlConnection(_connectionString))
        {
            using (var command = connection.CreateCommand())
            {
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
            }
        }

        return history;
    }
}
