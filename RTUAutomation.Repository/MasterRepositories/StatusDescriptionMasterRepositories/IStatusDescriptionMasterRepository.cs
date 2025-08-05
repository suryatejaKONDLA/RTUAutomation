namespace RTUAutomation.Repository.MasterRepositories.StatusDescriptionMasterRepositories;

public interface IStatusDescriptionMasterRepository
{
    Task<ResponseResult> AddOrUpdateStatusDescriptionAsync(StatusDescriptionMasterModel model);

    Task<StatusDescriptionMasterModelRoot> GetStatusDescriptionAsync(int id);

    Task<IEnumerable<StatusDescriptionMasterModelRoot>> GetAllStatusDescriptionsAsync();

    Task<ResponseResult> DeleteStatusDescriptionAsync(int id);

    Task<ResponseResult> DeleteStatusDescriptionsAsync(List<int> ids);
}