namespace TodoApi;

using Microsoft.AspNetCore.Mvc;
using System.Collections.Concurrent;

[ApiController]
[Route("api/[controller]")]
public class WeatherForecastController : ControllerBase
{
    private static readonly string[] Summaries = new[]
    {
        "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
    };

    private static readonly ConcurrentDictionary<Guid, WeatherForecastModel> _forecasts = new();

    // CREATE
    [HttpPost]
    public ActionResult<WeatherForecastModel> Create(WeatherForecastModel forecast)
    {
        forecast.Id = Guid.NewGuid();
        if (_forecasts.TryAdd(forecast.Id, forecast))
        {
            return CreatedAtAction(nameof(GetById), new { id = forecast.Id }, forecast);
        }
        return BadRequest("Failed to add the forecast.");
    }

    // READ (All)
    [HttpGet]
    public ActionResult<IEnumerable<WeatherForecastModel>> Get()
    {
        return Ok(_forecasts.Values);
    }

    // READ (By Id)
    [HttpGet("{id}")]
    public ActionResult<WeatherForecastModel> GetById(Guid id)
    {
        if (_forecasts.TryGetValue(id, out var forecast))
        {
            return Ok(forecast);
        }
        return NotFound();
    }

    [HttpGet("{id}/dto")]
    public ActionResult<WeatherForecastModelDto> GetDtoById(Guid id)
    {
        if (_forecasts.TryGetValue(id, out var forecast))
        {
            return Ok(forecast.ToDto());
        }
        return NotFound();
    }

    // UPDATE
    [HttpPut("{id}")]
    public IActionResult Update(Guid id, WeatherForecastModel forecast)
    {
        if (id != forecast.Id)
        {
            return BadRequest("ID mismatch");
        }

        if (_forecasts.TryGetValue(id, out var existingForecast))
        {
            if (_forecasts.TryUpdate(id, forecast, existingForecast))
            {
                return NoContent();
            }
            return BadRequest("Update failed.");
        }
        return NotFound();
    }

    // DELETE
    [HttpDelete("{id}")]
    public IActionResult Delete(Guid id)
    {
        if (_forecasts.TryRemove(id, out _))
        {
            return NoContent();
        }
        return NotFound();
    }

    // Generate random forecasts
    [HttpPost("generate")]
    public ActionResult<IEnumerable<WeatherForecastModel>> Generate(int count = 5)
    {
        var forecasts = Enumerable.Range(1, count).Select(index => new WeatherForecastModel
        {
            Id = Guid.NewGuid(),
            Date = DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
            TemperatureC = Random.Shared.Next(-20, 55),
            Summary = Summaries[Random.Shared.Next(Summaries.Length)]
        }).ToArray();

        foreach (var forecast in forecasts)
        {
            _forecasts.TryAdd(forecast.Id, forecast);
        }

        return CreatedAtAction(nameof(Get), forecasts);
    }
}
