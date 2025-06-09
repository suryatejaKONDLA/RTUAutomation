namespace RTUAutomation.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class PasController(IPasService pasService) : ControllerBase
{
    [HttpGet("GetAllLocations")]
    [Produces(MediaTypeNames.Application.Json)]
    [Consumes(MediaTypeNames.Application.Json)]
    [ProducesResponseType(typeof(IEnumerable<PasLocationsModel>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetAllLocations()
    {
        var locations = await pasService.GetAllLocationsAsync();
        return Ok(locations);
    }

    [HttpGet("GetAnalogTable/{rtuLocation}")]
    [Produces(MediaTypeNames.Application.Json)]
    [Consumes(MediaTypeNames.Application.Json)]
    [ProducesResponseType(typeof(IEnumerable<PasAnalogTableModel>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetAnalogTable(string rtuLocation)
    {
        var locations = await pasService.GetAnalogTableAsync(rtuLocation);
        return Ok(locations);
    }

    [HttpGet("GetDigitalTable/{rtuLocation}")]
    [Produces(MediaTypeNames.Application.Json)]
    [Consumes(MediaTypeNames.Application.Json)]
    [ProducesResponseType(typeof(IEnumerable<PasDigitalTableModel>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetDigitalTable(string rtuLocation)
    {
        var locations = await pasService.GetDigitalTableAsync(rtuLocation);
        return Ok(locations);
    }

    [HttpGet("GetControlTable/{rtuLocation}")]
    [Produces(MediaTypeNames.Application.Json)]
    [Consumes(MediaTypeNames.Application.Json)]
    [ProducesResponseType(typeof(IEnumerable<PasControlTableModel>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetControlTable(string rtuLocation)
    {
        var locations = await pasService.GetControlTableAsync(rtuLocation);
        return Ok(locations);
    }
}