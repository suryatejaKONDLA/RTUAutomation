namespace RTUAutomation.Services.MasterServices.FullScaleCountMasterServices;

[AutoRegisterService]
public sealed class FullScaleCountMasterService(IFullScaleCountMasterRepository repository) : IFullScaleCountMasterService
{
    public Task<ResponseResult> AddOrUpdateFullScaleCountAsync(FullScaleCountMasterModel model) => repository.AddOrUpdateFullScaleCountAsync(model);

    public Task<FullScaleCountMasterModelRoot> GetFullScaleCountAsync(int id) => repository.GetFullScaleCountAsync(id);

    public Task<IEnumerable<FullScaleCountMasterModelRoot>> GetAllFullScaleCountsAsync() => repository.GetAllFullScaleCountsAsync();

    public Task<ResponseResult> DeleteFullScaleCountAsync(int id) => repository.DeleteFullScaleCountAsync(id);

    public Task<ResponseResult> DeleteFullScaleCountsAsync(List<int> ids) => repository.DeleteFullScaleCountsAsync(ids);
}