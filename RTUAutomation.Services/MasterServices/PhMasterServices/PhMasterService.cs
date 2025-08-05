namespace RTUAutomation.Services.MasterServices.PhMasterServices;

[AutoRegisterService]
public sealed class PhMasterService(IPhMasterRepository repository) : IPhMasterService
{
    public Task<ResponseResult> AddOrUpdatePhAsync(PhMasterModel phModel) => repository.AddOrUpdatePhAsync(phModel);

    public Task<PhMasterModelRoot> GetPhAsync(int phId) => repository.GetPhAsync(phId);

    public Task<IEnumerable<PhMasterModelRoot>> GetAllPhsAsync() => repository.GetAllPhAsync();

    public Task<ResponseResult> DeletePhAsync(int phId) => repository.DeletePhAsync(phId);

    public Task<ResponseResult> DeletePhsAsync(List<int> phIds) => repository.DeletePhsAsync(phIds);
}