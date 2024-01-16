using PracticeStuff.Application.Dtos.FlashCard;
using PracticeStuff.Application.Services;
using Microsoft.AspNetCore.Mvc;

namespace PracticeStuff.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class FlashCardController : ControllerBase
{
    private readonly IFlashCardService _flashCardService;

    public FlashCardController(IFlashCardService flashCardService)
    {
        _flashCardService = flashCardService;
    }

    [HttpGet("{stackId:int}")]
    [ProducesResponseType(200)]
    [ProducesResponseType(404)]
    public async Task<ActionResult<GetFlashCardListDto>> GetFlashCards(int stackId)
    {
        var flashCards = await _flashCardService.GetFlashCards(stackId);

        if (!flashCards.Any())
        {
            return NotFound();
        }

        return Ok(flashCards);
    }

    [HttpGet("{stackId:int}/{id:int}")]
    [ProducesResponseType(200)]
    [ProducesResponseType(404)]
    public async Task<ActionResult<GetFlashCardDetailDto>> GetFlashCard(int stackId, int id)
    {
        var flashCard = await _flashCardService.GetFlashCard(stackId, id);

        if (flashCard is null)
        {
            return NotFound();
        }

        return Ok(flashCard);
    }

    [HttpPost]
    [ProducesResponseType(201)]
    [ProducesResponseType(400)]
    public async Task<ActionResult<CreateFlashCardDto>> PostFlashCard(CreateFlashCardDto createFlashCardDto)
    {
        var added = await _flashCardService.AddFlashCard(createFlashCardDto);

        if (added is null)
        {
            return BadRequest();
        }

        return CreatedAtAction(nameof(GetFlashCard), new { id = added.Id }, added);
    }

    [HttpPut]
    [ProducesResponseType(204)]
    [ProducesResponseType(400)]
    public async Task<IActionResult> UpdateFlashCard(UpdateFlashCardDto updateFlashCardDto)
    {
        try
        {
            await _flashCardService.UpdateFlashCard(updateFlashCardDto);
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