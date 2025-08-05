namespace RTUAutomation.Services.MasterServices.StatusDescriptionMasterServices;

[AutoRegisterService]
public sealed class StatusDescriptionMasterService(IStatusDescriptionMasterRepository repository) : IStatusDescriptionMasterService
{
    public Task<ResponseResult> AddOrUpdateStatusDescriptionAsync(StatusDescriptionMasterModel model) => repository.AddOrUpdateStatusDescriptionAsync(model);

    public Task<StatusDescriptionMasterModelRoot> GetStatusDescriptionAsync(int id) => repository.GetStatusDescriptionAsync(id);

    public Task<IEnumerable<StatusDescriptionMasterModelRoot>> GetAllStatusDescriptionsAsync() => repository.GetAllStatusDescriptionsAsync();

    public Task<ResponseResult> DeleteStatusDescriptionAsync(int id) => repository.DeleteStatusDescriptionAsync(id);

    public Task<ResponseResult> DeleteStatusDescriptionsAsync(List<int> ids) => repository.DeleteStatusDescriptionsAsync(ids);
}