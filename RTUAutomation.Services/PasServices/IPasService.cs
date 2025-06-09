namespace RTUAutomation.Services.PasServices;

public interface IPasService
{
    public Task<IEnumerable<PasLocationsModel>> GetAllLocationsAsync();
}