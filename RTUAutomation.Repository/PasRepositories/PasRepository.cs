namespace RTUAutomation.Repository.PasRepositories;

public sealed class PasRepository(DatabaseHelper databaseHelper) : IPasRepository
{
    public async Task<IEnumerable<PasLocationsModel>> GetAllLocationsAsync()
    {
        const string query = "SELECT a.RTUName, a.ProtocolName, b.StnName FROM Monarch.dbo.vRTU_Protocol a LEFT JOIN Monarch.dbo.RTU_Station b ON (b.pRTU = a.record) ORDER BY RTUName";
        return await databaseHelper.QueryAsync<PasLocationsModel>(query);
    }
}