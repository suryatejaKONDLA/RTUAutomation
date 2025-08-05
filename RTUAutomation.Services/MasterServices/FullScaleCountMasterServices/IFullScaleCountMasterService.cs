namespace RTUAutomation.Services.MasterServices.FullScaleCountMasterServices;

public interface IFullScaleCountMasterService
{
    Task<ResponseResult> AddOrUpdateFullScaleCountAsync(FullScaleCountMasterModel model);
    Task<FullScaleCountMasterModelRoot> GetFullScaleCountAsync(int id);
    Task<IEnumerable<FullScaleCountMasterModelRoot>> GetAllFullScaleCountsAsync();
    Task<ResponseResult> DeleteFullScaleCountAsync(int id);
    Task<ResponseResult> DeleteFullScaleCountsAsync(List<int> ids);
}