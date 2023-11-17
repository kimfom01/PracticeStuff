using BusinessLogic.Services;
using DataAccess.Dtos.Stack;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class StackController : ControllerBase
{
    private readonly IStackService _stackService;

    public StackController(IStackService stackService)
    {
        _stackService = stackService;
    }

    [HttpGet]
    [ProducesResponseType(200)]
    [ProducesResponseType(404)]
    public async Task<IActionResult> GetStacks()
    {
        var stacks = await _stackService.GetStacks();

        if (!stacks.Any())
        {
            return NotFound();
        }

        return Ok(stacks);
    }

    [HttpGet("{id:int}")]
    [ProducesResponseType(200)]
    [ProducesResponseType(404)]
    public async Task<IActionResult> GetStack(int id)
    {
        var stack = await _stackService.GetStack(id);
        
        if (stack is null)
        {
            return NotFound();
        }

        return Ok(stack);
    }

    [HttpPost]
    [ProducesResponseType(201)]
    [ProducesResponseType(400)]
    public async Task<IActionResult> PostStack(CreateStackDto stack)
    {
        var added = await _stackService.AddStack(stack);
        
        if (added is null)
        {
            return BadRequest();
        }

        return CreatedAtAction(nameof(GetStack), new { id = added.Id }, added);
    }

    [HttpPut]
    [ProducesResponseType(204)]
    [ProducesResponseType(400)]
    public async Task<IActionResult> UpdateStack(UpdateStackDto stack)
    {
        try
        {
            await _stackService.UpdateStack(stack);
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
    public async Task<IActionResult> DeleteStack(int id)
    {
        try
        {
            await _stackService.DeleteStack(id);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }

        return NoContent();
    }
}