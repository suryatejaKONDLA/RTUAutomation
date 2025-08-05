namespace RTUAutomation.Api.Controllers.Masters;

[ApiController]
[Tags("Unit Master")]
[Route("[controller]")]
[ApiExplorerSettings(GroupName = GroupConstants.MastersModule)]
[ProducesResponseType(typeof(ResponseResult), StatusCodes.Status401Unauthorized)]
public class UnitMasterController(IUnitMasterService unitMasterService) : ControllerBase
{
#region PUT & POST

    [HttpPost("AddUnit")]
    [HttpPut("UpdateUnit")]
    [Produces(MediaTypeNames.Application.Json)]
    [Consumes(MediaTypeNames.Application.Json)]
    [ProducesResponseType(typeof(ResponseResult), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ResponseResult<Dictionary<string, string[]>>), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> AddOrUpdateUnit([FromBody] UnitMasterModel request, [FromServices] IValidator<UnitMasterModel> validator)
    {
        var validationResult = await validator.ValidateAsync(request);
        if (!validationResult.IsValid)
        {
            var errors = validationResult.Errors.ToErrorDictionary();
            return BadRequest(new ResponseResult<Dictionary<string, string[]>>(-1, ResultType.Error, ResultMessage.ValidationMessage, errors));
        }

        var result = await unitMasterService.AddOrUpdateUnitAsync(request);
        return Ok(result);
    }

#endregion

#region GET

    [HttpGet("GetUnit/{unitId:int}")]
    [Produces(MediaTypeNames.Application.Json)]
    [ProducesResponseType(typeof(UnitMasterModelRoot), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ResponseResult), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> GetUnit([FromRoute] int unitId)
    {
        var result = await unitMasterService.GetUnitAsync(unitId);
        return Ok(result);
    }

    [HttpGet("GetAllUnits")]
    [Produces(MediaTypeNames.Application.Json)]
    [ProducesResponseType(typeof(IEnumerable<UnitMasterModelRoot>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetAllUnits()
    {
        var result = await unitMasterService.GetAllUnitsAsync();
        return Ok(result);
    }

#endregion

#region DELETE

    [HttpDelete("DeleteUnit/{unitId:int}")]
    [Produces(MediaTypeNames.Application.Json)]
    [ProducesResponseType(typeof(ResponseResult), StatusCodes.Status200OK)]
    public async Task<IActionResult> DeleteUnit([FromRoute] int unitId)
    {
        var result = await unitMasterService.DeleteUnitAsync(unitId);
        return Ok(result);
    }

    [HttpDelete("DeleteUnits")]
    [Produces(MediaTypeNames.Application.Json)]
    [ProducesResponseType(typeof(ResponseResult), StatusCodes.Status200OK)]
    public async Task<IActionResult> DeleteUnits([FromBody] List<int> unitIds)
    {
        if (unitIds == null || unitIds.Count < 0)
        {
            return BadRequest(new ResponseResult(-1, ResultType.Error, "No Unit ID's Provided"));
        }

        var result = await unitMasterService.DeleteUnitsAsync(unitIds);
        return Ok(result);
    }

#endregion
}