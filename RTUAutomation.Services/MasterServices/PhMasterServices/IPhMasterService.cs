namespace RTUAutomation.Services.MasterServices.PhMasterServices;

public interface IPhMasterService
{
    Task<ResponseResult> AddOrUpdatePhAsync(PhMasterModel phModel);
    Task<PhMasterModelRoot> GetPhAsync(int phId);
    Task<IEnumerable<PhMasterModelRoot>> GetAllPhsAsync();
    Task<ResponseResult> DeletePhAsync(int phId);
    Task<ResponseResult> DeletePhsAsync(List<int> phIds);
}