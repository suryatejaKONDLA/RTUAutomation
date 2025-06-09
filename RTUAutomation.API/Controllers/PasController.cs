namespace RTUAutomation.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class PasController(IPasService pasService) : ControllerBase
{
    [HttpGet("Locations")]
    [Produces(MediaTypeNames.Application.Json)]
    [Consumes(MediaTypeNames.Application.Json)]
    [ProducesResponseType(typeof(IEnumerable<PasLocationsModel>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetAllLocationsAsync()
    {
        var locations = await pasService.GetAllLocationsAsync();
        return Ok(locations);
    }
}