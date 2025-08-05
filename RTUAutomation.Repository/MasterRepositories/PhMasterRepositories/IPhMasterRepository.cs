namespace RTUAutomation.Repository.MasterRepositories.PhMasterRepositories;

public interface IPhMasterRepository
{
    public Task<ResponseResult> AddOrUpdatePhAsync(PhMasterModel phModel);

    public Task<PhMasterModelRoot> GetPhAsync(int phId);

    public Task<IEnumerable<PhMasterModelRoot>> GetAllPhAsync();

    public Task<ResponseResult> DeletePhAsync(int phId);

    public Task<ResponseResult> DeletePhsAsync(List<int> phIds);
}