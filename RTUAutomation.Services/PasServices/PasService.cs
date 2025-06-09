namespace RTUAutomation.Services.PasServices;

internal class PasService(IPasRepository repository) : IPasService
{
    public async Task<IEnumerable<PasLocationsModel>> GetAllLocationsAsync() => await repository.GetAllLocationsAsync();
}