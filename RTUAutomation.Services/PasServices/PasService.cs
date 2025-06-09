namespace RTUAutomation.Services.PasServices;

internal class PasService(IPasRepository repository) : IPasService
{
    public async Task<IEnumerable<PasLocationsModel>> GetAllLocationsAsync() => await repository.GetAllLocationsAsync();

    public async Task<IEnumerable<PasAnalogTableModel>> GetAnalogTableAsync(string rtuLocation) => await repository.GetAnalogTableAsync(rtuLocation);

    public async Task<IEnumerable<PasDigitalTableModel>> GetDigitalTableAsync(string rtuLocation) => await repository.GetDigitalTableAsync(rtuLocation);

    public async Task<IEnumerable<PasControlTableModel>> GetControlTableAsync(string rtuLocation) => await repository.GetControlTableAsync(rtuLocation);
}