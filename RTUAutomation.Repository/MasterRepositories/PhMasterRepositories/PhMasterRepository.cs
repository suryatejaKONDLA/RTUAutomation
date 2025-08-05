namespace RTUAutomation.Repository.MasterRepositories.PhMasterRepositories;

[AutoRegisterService]
public sealed class PhMasterRepository(DatabaseHelper databaseHelper) : IPhMasterRepository
{
#region PUT & POST

    public async Task<ResponseResult> AddOrUpdatePhAsync(PhMasterModel phModel)
    {
        Dictionary<string, object> spParameters = new()
        {
            { "SL01_PH_ID", phModel.Sl01PhId ?? 0 },
            { "SL01_PH_Name", phModel.Sl01PhName },
            { "SL01_PH_Active_Flag", phModel.Sl01PhActiveFlag },
            { "SL01_PH_Order", phModel.Sl01PhOrder },
            { CommonConstants.SessionId, phModel.SessionId }
        };

        return await databaseHelper.ExecuteStoredProcedureWithOutputsAsync("dbo.SL01_PH_Master_Insert", spParameters);
    }

#endregion

#region GET

    public async Task<PhMasterModelRoot> GetPhAsync(int phId)
    {
        const string query = "SELECT *, dbo.Log_Name(SL01_Created_ID) [SL01_Created_Name], dbo.Log_Name(SL01_Modified_ID) [SL01_Modified_Name], dbo.Log_Name(SL01_Approved_ID) [SL01_Approved_Name] FROM dbo.SL01_PH_Master WHERE SL01_PH_ID = @SL01_PH_ID AND SL01_PH_Active_Flag = 'Y'";
        return await databaseHelper.QuerySingleAsync<PhMasterModelRoot>(query, new { SL01_PH_ID = phId });
    }

    public async Task<IEnumerable<PhMasterModelRoot>> GetAllPhAsync()
    {
        const string query = "SELECT *, dbo.Log_Name(SL01_Created_ID) [SL01_Created_Name], dbo.Log_Name(SL01_Modified_ID) [SL01_Modified_Name], dbo.Log_Name(SL01_Approved_ID) [SL01_Approved_Name] FROM dbo.SL01_PH_Master WHERE SL01_PH_Active_Flag = 'Y' ORDER BY SL01_PH_Order";
        return await databaseHelper.QueryAsync<PhMasterModelRoot>(query);
    }

#endregion

#region DELETE

    public async Task<ResponseResult> DeletePhAsync(int phId)
    {
        const string query = "DELETE FROM dbo.SL01_PH_Master WHERE SL01_PH_ID = @SL01_PH_ID";
        return await databaseHelper.ExecuteQueryAsync(query, new() { { "SL01_PH_ID", phId } });
    }

    public async Task<ResponseResult> DeletePhsAsync(List<int> phIds)
    {
        var (inClause, parameters) = DatabaseHelper.BuildInClause(phIds, "Ph_ID");

        var query = $"DELETE FROM dbo.SL01_PH_Master WHERE SL01_PH_ID in ({inClause})";

        return await databaseHelper.ExecuteQueryAsync(query, parameters);
    }

#endregion
}