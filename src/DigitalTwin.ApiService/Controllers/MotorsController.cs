using Microsoft.AspNetCore.Mvc;
using DigitalTwin.Application.Interfaces;
using DigitalTwin.Application.DTOs;

namespace DigitalTwin.ApiService.Controllers;

[ApiController]
[Route("api/[controller]")]
public class MotorsController : ControllerBase
{
    private readonly IMotorService _motorService;

    public MotorsController(IMotorService motorService)
    {
        _motorService = motorService;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var motors = await _motorService.GetAllMotorsAsync();
        return Ok(motors);
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateMotorRequestDto request)
    {
        try
        {
            var newMotor = await _motorService.CreateMotorAsync(request);
            return CreatedAtAction(nameof(GetById), new { id = newMotor.Id }, newMotor);
        }
        catch (ArgumentException ex)
        {
            return BadRequest(new { message = ex.Message });
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Critical error: {ex}");
            return StatusCode(500, new { message = "An unexpected error occurred." });
        }
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(Guid id)
    {
        var motor = await _motorService.GetMotorByIDAsync(id);

        if (motor == null)
            return NotFound();

        return Ok(motor);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(Guid id, [FromBody] UpdateMotorRequestDto request)
    {
        if (id != request.Id)
            return BadRequest("The URl id does not match the Body id");

        try
        {
            await _motorService.UpdateMotorAsync(request);
            return NoContent();
        }
        catch (KeyNotFoundException)
        {
            return NotFound();
        }
    }
}