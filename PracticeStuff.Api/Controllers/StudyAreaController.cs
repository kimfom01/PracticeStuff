using PracticeStuff.Application.Dtos.StudyArea;
using PracticeStuff.Application.Services;
using Microsoft.AspNetCore.Mvc;

namespace PracticeStuff.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class StudyAreaController : ControllerBase
{
    private readonly IStudyAreaService _studyAreaService;

    public StudyAreaController(IStudyAreaService studyAreaService)
    {
        _studyAreaService = studyAreaService;
    }

    [HttpGet("{id:int}")]
    [ProducesResponseType(200)]
    [ProducesResponseType(400)]
    public async Task<ActionResult<GetStudyAreaDetailDto>> GetStudyArea(int id)
    {
        var studyArea = await _studyAreaService.GetStudyArea(id);

        if (studyArea is null)
        {
            return NotFound();
        }

        return Ok(studyArea);
    }

    [HttpGet]
    [ProducesResponseType(200)]
    [ProducesResponseType(400)]
    public async Task<ActionResult<GetStudyAreaListDto>> GetStudyAreas()
    {
        var studyAreas = await _studyAreaService.GetStudyAreas();

        if (!studyAreas.Any())
        {
            return NotFound();
        }

        return Ok(studyAreas);
    }

    [HttpPost]
    [ProducesResponseType(201)]
    [ProducesResponseType(400)]
    public async Task<ActionResult<CreateStudyAreaDto>> PostStudyArea(CreateStudyAreaDto studyArea)
    {
        var added = await _studyAreaService.AddStudyArea(studyArea);

        if (added is null)
        {
            return BadRequest();
        }

        return CreatedAtAction(nameof(GetStudyArea), new { id = added.Id }, added);
    }

    [HttpPut]
    [ProducesResponseType(204)]
    [ProducesResponseType(400)]
    public async Task<IActionResult> UpdateStudyArea(UpdateStudyAreaDto studyArea)
    {
        try
        {
            await _studyAreaService.UpdateStudyArea(studyArea);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }

        return NoContent();
    }

    [HttpDelete("{id:int}")]
    [ProducesResponseType(204)]
    [ProducesResponseType(400)]
    public async Task<IActionResult> DeleteStudyArea(int id)
    {
        try
        {
            await _studyAreaService.DeleteStudyArea(id);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }

        return NoContent();
    }
}