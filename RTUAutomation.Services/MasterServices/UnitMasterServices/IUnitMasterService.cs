namespace RTUAutomation.Services.MasterServices.UnitMasterServices;

public interface IUnitMasterService
{
    public Task<ResponseResult> AddOrUpdateUnitAsync(UnitMasterModel unitModel);

    public Task<UnitMasterModelRoot> GetUnitAsync(int unitId);

    public Task<IEnumerable<UnitMasterModelRoot>> GetAllUnitsAsync();

    public Task<ResponseResult> DeleteUnitAsync(int unitId);

    public Task<ResponseResult> DeleteUnitsAsync(List<int> unitIds);
}