namespace RTUAutomation.Services.MasterServices.UnitMasterServices;

[AutoRegisterService]
public sealed class UnitMasterService(IUnitMasterRepository repository) : IUnitMasterService
{
    public Task<ResponseResult> AddOrUpdateUnitAsync(UnitMasterModel unitModel) => repository.AddOrUpdateUnitAsync(unitModel);

    public Task<UnitMasterModelRoot> GetUnitAsync(int unitId) => repository.GetUnitAsync(unitId);

    public Task<IEnumerable<UnitMasterModelRoot>> GetAllUnitsAsync() => repository.GetAllUnitsAsync();

    public Task<ResponseResult> DeleteUnitAsync(int unitId) => repository.DeleteUnitAsync(unitId);

    public Task<ResponseResult> DeleteUnitsAsync(List<int> unitIds) => repository.DeleteUnitsAsync(unitIds);
}