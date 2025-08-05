namespace RTUAutomation.Api.Controllers.Masters;

[ApiController]
[Tags("Full Scale Count Master")]
[Route("[controller]")]
[ApiExplorerSettings(GroupName = GroupConstants.MastersModule)]
[ProducesResponseType(typeof(ResponseResult), StatusCodes.Status401Unauthorized)]
public class FullScaleCountMasterController(IFullScaleCountMasterService fullScaleCountService) : ControllerBase
{
#region PUT & POST

    [HttpPost("AddFullScaleCount")]
    [HttpPut("UpdateFullScaleCount")]
    [Produces(MediaTypeNames.Application.Json)]
    [Consumes(MediaTypeNames.Application.Json)]
    [ProducesResponseType(typeof(ResponseResult), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ResponseResult<Dictionary<string, string[]>>), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> AddOrUpdateFullScaleCount(
        [FromBody] FullScaleCountMasterModel request,
        [FromServices] IValidator<FullScaleCountMasterModel> validator)
    {
        var validationResult = await validator.ValidateAsync(request);
        if (!validationResult.IsValid)
        {
            var errors = validationResult.Errors.ToErrorDictionary();
            return BadRequest(new ResponseResult<Dictionary<string, string[]>>(
                -1, ResultType.Error, ResultMessage.ValidationMessage, errors));
        }

        var result = await fullScaleCountService.AddOrUpdateFullScaleCountAsync(request);
        return Ok(result);
    }

#endregion

#region GET

    [HttpGet("GetFullScaleCount/{id:int}")]
    [Produces(MediaTypeNames.Application.Json)]
    [ProducesResponseType(typeof(FullScaleCountMasterModelRoot), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ResponseResult), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> GetFullScaleCount([FromRoute] int id)
    {
        var result = await fullScaleCountService.GetFullScaleCountAsync(id);
        return Ok(result);
    }

    [HttpGet("GetAllFullScaleCounts")]
    [Produces(MediaTypeNames.Application.Json)]
    [ProducesResponseType(typeof(IEnumerable<FullScaleCountMasterModelRoot>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetAllFullScaleCounts()
    {
        var result = await fullScaleCountService.GetAllFullScaleCountsAsync();
        return Ok(result);
    }

#endregion

#region DELETE

    [HttpDelete("DeleteFullScaleCount/{id:int}")]
    [Produces(MediaTypeNames.Application.Json)]
    [ProducesResponseType(typeof(ResponseResult), StatusCodes.Status200OK)]
    public async Task<IActionResult> DeleteFullScaleCount([FromRoute] int id)
    {
        var result = await fullScaleCountService.DeleteFullScaleCountAsync(id);
        return Ok(result);
    }

    [HttpDelete("DeleteFullScaleCounts")]
    [Produces(MediaTypeNames.Application.Json)]
    [ProducesResponseType(typeof(ResponseResult), StatusCodes.Status200OK)]
    public async Task<IActionResult> DeleteFullScaleCounts([FromBody] List<int> ids)
    {
        if (ids == null || ids.Count < 1)
        {
            return BadRequest(new ResponseResult(-1, ResultType.Error, "No FullScaleCount ID's Provided"));
        }

        var result = await fullScaleCountService.DeleteFullScaleCountsAsync(ids);
        return Ok(result);
    }

#endregion
}