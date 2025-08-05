namespace RTUAutomation.Repository.MasterRepositories.FullScaleCountMasterRepositories;

public interface IFullScaleCountMasterRepository
{
    Task<ResponseResult> AddOrUpdateFullScaleCountAsync(FullScaleCountMasterModel model);

    Task<FullScaleCountMasterModelRoot> GetFullScaleCountAsync(int id);

    Task<IEnumerable<FullScaleCountMasterModelRoot>> GetAllFullScaleCountsAsync();

    Task<ResponseResult> DeleteFullScaleCountAsync(int id);

    Task<ResponseResult> DeleteFullScaleCountsAsync(List<int> ids);
}