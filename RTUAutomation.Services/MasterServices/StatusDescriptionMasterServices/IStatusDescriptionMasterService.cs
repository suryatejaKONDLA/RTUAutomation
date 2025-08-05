namespace RTUAutomation.Services.MasterServices.StatusDescriptionMasterServices;

public interface IStatusDescriptionMasterService
{
    Task<ResponseResult> AddOrUpdateStatusDescriptionAsync(StatusDescriptionMasterModel model);
    Task<StatusDescriptionMasterModelRoot> GetStatusDescriptionAsync(int id);
    Task<IEnumerable<StatusDescriptionMasterModelRoot>> GetAllStatusDescriptionsAsync();
    Task<ResponseResult> DeleteStatusDescriptionAsync(int id);
    Task<ResponseResult> DeleteStatusDescriptionsAsync(List<int> ids);
}