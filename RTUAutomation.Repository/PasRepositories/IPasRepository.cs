namespace RTUAutomation.Repository.PasRepositories;

public interface IPasRepository
{
    public Task<IEnumerable<PasLocationsModel>> GetAllLocationsAsync();
}