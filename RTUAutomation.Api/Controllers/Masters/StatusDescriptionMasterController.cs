namespace RTUAutomation.Api.Controllers.Masters;

[ApiController]
[Tags("Status Description Master")]
[Route("[controller]")]
[ApiExplorerSettings(GroupName = GroupConstants.MastersModule)]
[ProducesResponseType(typeof(ResponseResult), StatusCodes.Status401Unauthorized)]
public class StatusDescriptionMasterController(IStatusDescriptionMasterService statusDescriptionService) : ControllerBase
{
#region PUT & POST

    [HttpPost("AddStatusDescription")]
    [HttpPut("UpdateStatusDescription")]
    [Produces(MediaTypeNames.Application.Json)]
    [Consumes(MediaTypeNames.Application.Json)]
    [ProducesResponseType(typeof(ResponseResult), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ResponseResult<Dictionary<string, string[]>>), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> AddOrUpdateStatusDescription(
        [FromBody] StatusDescriptionMasterModel request,
        [FromServices] IValidator<StatusDescriptionMasterModel> validator)
    {
        var validationResult = await validator.ValidateAsync(request);
        if (!validationResult.IsValid)
        {
            var errors = validationResult.Errors.ToErrorDictionary();
            return BadRequest(new ResponseResult<Dictionary<string, string[]>>(
                -1, ResultType.Error, ResultMessage.ValidationMessage, errors));
        }

        var result = await statusDescriptionService.AddOrUpdateStatusDescriptionAsync(request);
        return Ok(result);
    }

#endregion

#region GET

    [HttpGet("GetStatusDescription/{id:int}")]
    [Produces(MediaTypeNames.Application.Json)]
    [ProducesResponseType(typeof(StatusDescriptionMasterModelRoot), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ResponseResult), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> GetStatusDescription([FromRoute] int id)
    {
        var result = await statusDescriptionService.GetStatusDescriptionAsync(id);
        return Ok(result);
    }

    [HttpGet("GetAllStatusDescriptions")]
    [Produces(MediaTypeNames.Application.Json)]
    [ProducesResponseType(typeof(IEnumerable<StatusDescriptionMasterModelRoot>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetAllStatusDescriptions()
    {
        var result = await statusDescriptionService.GetAllStatusDescriptionsAsync();
        return Ok(result);
    }

#endregion

#region DELETE

    [HttpDelete("DeleteStatusDescription/{id:int}")]
    [Produces(MediaTypeNames.Application.Json)]
    [ProducesResponseType(typeof(ResponseResult), StatusCodes.Status200OK)]
    public async Task<IActionResult> DeleteStatusDescription([FromRoute] int id)
    {
        var result = await statusDescriptionService.DeleteStatusDescriptionAsync(id);
        return Ok(result);
    }

    [HttpDelete("DeleteStatusDescriptions")]
    [Produces(MediaTypeNames.Application.Json)]
    [ProducesResponseType(typeof(ResponseResult), StatusCodes.Status200OK)]
    public async Task<IActionResult> DeleteStatusDescriptions([FromBody] List<int> ids)
    {
        if (ids == null || ids.Count < 1)
        {
            return BadRequest(new ResponseResult(-1, ResultType.Error, "No Status Description ID's Provided"));
        }

        var result = await statusDescriptionService.DeleteStatusDescriptionsAsync(ids);
        return Ok(result);
    }

#endregion
}