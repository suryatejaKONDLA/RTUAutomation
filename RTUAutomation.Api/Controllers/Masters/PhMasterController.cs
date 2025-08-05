namespace RTUAutomation.Api.Controllers.Masters;

[ApiController]
[Tags("PH Master")]
[Route("[controller]")]
[ApiExplorerSettings(GroupName = GroupConstants.MastersModule)]
[ProducesResponseType(typeof(ResponseResult), StatusCodes.Status401Unauthorized)]
public class PhMasterController(IPhMasterService phMasterService) : ControllerBase
{
#region PUT & POST

    [HttpPost("AddPh")]
    [HttpPut("UpdatePh")]
    [Produces(MediaTypeNames.Application.Json)]
    [Consumes(MediaTypeNames.Application.Json)]
    [ProducesResponseType(typeof(ResponseResult), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ResponseResult<Dictionary<string, string[]>>), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> AddOrUpdatePh([FromBody] PhMasterModel request, [FromServices] IValidator<PhMasterModel> validator)
    {
        var validationResult = await validator.ValidateAsync(request);
        if (!validationResult.IsValid)
        {
            var errors = validationResult.Errors.ToErrorDictionary();
            return BadRequest(new ResponseResult<Dictionary<string, string[]>>(-1, ResultType.Error, ResultMessage.ValidationMessage, errors));
        }

        var result = await phMasterService.AddOrUpdatePhAsync(request);
        return Ok(result);
    }

#endregion

#region GET

    [HttpGet("GetPh/{phId:int}")]
    [Produces(MediaTypeNames.Application.Json)]
    [ProducesResponseType(typeof(PhMasterModelRoot), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ResponseResult), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> GetPh([FromRoute] int phId)
    {
        var result = await phMasterService.GetPhAsync(phId);
        return Ok(result);
    }

    [HttpGet("GetAllPhs")]
    [Produces(MediaTypeNames.Application.Json)]
    [ProducesResponseType(typeof(IEnumerable<PhMasterModelRoot>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetAllPhs()
    {
        var result = await phMasterService.GetAllPhsAsync();
        return Ok(result);
    }

#endregion

#region DELETE

    [HttpDelete("DeletePh/{phId:int}")]
    [Produces(MediaTypeNames.Application.Json)]
    [ProducesResponseType(typeof(ResponseResult), StatusCodes.Status200OK)]
    public async Task<IActionResult> DeletePh([FromRoute] int phId)
    {
        var result = await phMasterService.DeletePhAsync(phId);
        return Ok(result);
    }

    [HttpDelete("DeletePhs")]
    [Produces(MediaTypeNames.Application.Json)]
    [ProducesResponseType(typeof(ResponseResult), StatusCodes.Status200OK)]
    public async Task<IActionResult> DeletePhs([FromBody] List<int> phIds)
    {
        if (phIds == null || phIds.Count < 0)
        {
            return BadRequest(new ResponseResult(-1, ResultType.Error, "No PH ID's Provided"));
        }

        var result = await phMasterService.DeletePhsAsync(phIds);
        return Ok(result);
    }

#endregion
}