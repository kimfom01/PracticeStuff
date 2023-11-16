using BusinessLogic.Services;
using DataAccess.Models;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class FlashCardController : ControllerBase
{
    private readonly IFlashCardService _flashCardService;

    public FlashCardController(IFlashCardService flashCardService)
    {
        _flashCardService = flashCardService;
    }

    [HttpGet]
    [ProducesResponseType(200)]
    [ProducesResponseType(404)]
    public async Task<IActionResult> GetFlashCards()
    {
        var flashCards = await _flashCardService.GetFlashCards();

        if (!flashCards.Any())
        {
            return NotFound();
        }

        return Ok(flashCards);
    }

    [HttpGet("{id:int}")]
    [ProducesResponseType(200)]
    [ProducesResponseType(404)]
    public async Task<IActionResult> GetFlashCard(int id)
    {
        var flashCard = await _flashCardService.GetFlashCard(id);

        if (flashCard is null)
        {
            return NotFound();
        }

        return Ok(flashCard);
    }

    [HttpPost]
    [ProducesResponseType(201)]
    [ProducesResponseType(400)]
    public async Task<IActionResult> PostFlashCard(FlashCard flashCard)
    {
        var added = await _flashCardService.AddFlashCard(flashCard);

        if (added is null)
        {
            return BadRequest();
        }

        return CreatedAtAction(nameof(GetFlashCard), new { id = added.Id }, added);
    }

    [HttpPut]
    [ProducesResponseType(204)]
    [ProducesResponseType(400)]
    public async Task<IActionResult> UpdateFlashCard(FlashCard flashCard)
    {
        try
        {
            await _flashCardService.UpdateFlashCard(flashCard);
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
    public async Task<IActionResult> DeleteFlashCard(int id)
    {
        try
        {
            await _flashCardService.DeleteFlashCard(id);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }

        return NoContent();
    }
}