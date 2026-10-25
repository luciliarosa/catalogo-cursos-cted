using Microsoft.AspNetCore.Mvc;
using SesiCursos.Api.Data;

namespace SesiCursos.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
[Produces("application/json")]
public class HealthController : ControllerBase
{
    private readonly AppDbContext _context;

    public HealthController(AppDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<IActionResult> Check()
    {
        var canConnect = await _context.Database.CanConnectAsync();
        return Ok(new
        {
            status = canConnect ? "Healthy" : "Degraded",
            database = canConnect ? "Connected" : "Disconnected",
            provider = _context.Database.ProviderName,
            timestamp = DateTime.UtcNow
        });
    }
}
