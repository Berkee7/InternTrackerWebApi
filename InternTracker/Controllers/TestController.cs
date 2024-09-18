using Microsoft.AspNetCore.Mvc;
using InternTracker.Services;
using System.Collections.Generic;
using System.Threading.Tasks;

[ApiController]
[Route("api/[controller]")]
public class HolidaysController : ControllerBase
{
    private readonly HolidayService _holidayService;

    public HolidaysController(HolidayService holidayService)
    {
        _holidayService = holidayService;
    }

    [HttpGet]
    public async Task<ActionResult<List<DateTime>>> GetHolidays()
    {
        try
        {
            var holidays = await _holidayService.GetPublicHolidaysAsync();
            return Ok(holidays);
        }
        catch (HttpRequestException ex)
        {
            return BadRequest($"Error: {ex.Message}");
        }
        catch (Exception ex)
        {
            return BadRequest($"Error: {ex.Message}");
        }
    }
}
