namespace RTUAutomation.Services.PasServices;

public interface IPasService
{
    public Task<IEnumerable<PasLocationsModel>> GetAllLocationsAsync();

    public Task<IEnumerable<PasAnalogTableModel>> GetAnalogTableAsync(string rtuLocation);

    public Task<IEnumerable<PasDigitalTableModel>> GetDigitalTableAsync(string rtuLocation);

    public Task<IEnumerable<PasControlTableModel>> GetControlTableAsync(string rtuLocation);
}