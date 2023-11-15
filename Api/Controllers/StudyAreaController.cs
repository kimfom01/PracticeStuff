using BusinessLogic.Services;
using DataAccess.Models;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers;

[ApiController]
[Route("[controller]")]
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
    public async Task<IActionResult> GetStudyArea(int id)
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
    public async Task<IActionResult> GetStudyAreas()
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
    public async Task<IActionResult> PostStudyArea(StudyArea studyArea)
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
    public async Task<IActionResult> UpdateStudyArea(StudyArea studyArea)
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