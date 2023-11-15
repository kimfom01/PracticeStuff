using System.Data;
using System.Data.SqlClient;
using Dapper;
using DataAccess.Config;
using DataAccess.Dtos;
using DataAccess.Models;

namespace DataAccess.Repositories.Implementation;

public class StudyAreaRepository : IStudyAreaRepository
{
    private readonly Configuration _configuration;
    private readonly IStackRepository _stackRepository;
    public StudyAreaRepository(
        Configuration configuration,
    IStackRepository stackRepository)
    {
        _configuration = configuration;
        _stackRepository = stackRepository;
    }

    public async Task CreateStudyAreaTable()
    {
        using IDbConnection connection = new SqlConnection(_configuration.ConnectionString);

        var sql = """
                      IF OBJECT_ID(N'StudyArea', N'U') IS NULL 
                      CREATE TABLE StudyArea (Id INT PRIMARY KEY IDENTITY(1,1), 
                      StackId INT FOREIGN KEY REFERENCES Stack(Id) ON DELETE CASCADE, 
                      Date DATE DEFAULT GETDATE(), Score INT)
                  """;

        await connection.ExecuteAsync(sql);
    }

    public async Task SaveScore(StudyArea studyArea, Stack stack)
    {
        var stackId = await _stackRepository.GetStackId(stack);

        using IDbConnection connection = new SqlConnection(_configuration.ConnectionString);

        var sql = """
                      INSERT INTO StudyArea(StackId, Score) 
                      VALUES (@stackId, @studyAreaScore)
                  """;
        
        object[] parameters =
        {
            new { stackId, studyAreaScore= studyArea.Score}
        };

        await connection.ExecuteAsync(sql, parameters);
    }

    public async Task<IEnumerable<StudyAreaDto>> GetScoresHistory()
    {
        using IDbConnection connection = new SqlConnection(_configuration.ConnectionString);

        var sql = """
                  SELECT SA.Date, SA.Score, St.Name As Stack
                  FROM StudyArea AS SA 
                      LEFT JOIN Stack AS St 
                          ON SA.StackId = St.Id
                  """;

        return await connection.QueryAsync<StudyAreaDto>(sql);
    }
}
