namespace RTUAutomation.Repository.MasterRepositories.FullScaleCountMasterRepositories;

[AutoRegisterService]
public sealed class FullScaleCountMasterRepository(DatabaseHelper databaseHelper) : IFullScaleCountMasterRepository
{
#region PUT & POST

    public async Task<ResponseResult> AddOrUpdateFullScaleCountAsync(FullScaleCountMasterModel model)
    {
        Dictionary<string, object> spParameters = new()
        {
            { "SL05_FullScaleCount_ID", model.Sl05FullScaleCountId ?? 0 },
            { "SL05_FullScaleCount_Name", model.Sl05FullScaleCountName },
            { "SL05_FullScaleCount_Active_Flag", model.Sl05FullScaleCountActiveFlag },
            { "SL05_FullScaleCount_Order", model.Sl05FullScaleCountOrder },
            { CommonConstants.SessionId, model.SessionId }
        };

        return await databaseHelper.ExecuteStoredProcedureWithOutputsAsync("dbo.SL05_FullScaleCount_Master_Insert", spParameters);
    }

#endregion

#region GET

    public async Task<FullScaleCountMasterModelRoot> GetFullScaleCountAsync(int id)
    {
        const string query = """

                                             SELECT *, 
                                                    dbo.Log_Name(SL05_Created_ID) AS [SL05_Created_Name],
                                                    dbo.Log_Name(SL05_Modified_ID) AS [SL05_Modified_Name],
                                                    dbo.Log_Name(SL05_Approved_ID) AS [SL05_Approved_Name]
                                               FROM dbo.SL05_FullScaleCount_Master
                                              WHERE SL05_FullScaleCount_ID = @SL05_FullScaleCount_ID
                                                AND SL05_FullScaleCount_Active_Flag = 'Y'
                             """;
        return await databaseHelper.QuerySingleAsync<FullScaleCountMasterModelRoot>(query, new { SL05_FullScaleCount_ID = id });
    }

    public async Task<IEnumerable<FullScaleCountMasterModelRoot>> GetAllFullScaleCountsAsync()
    {
        const string query = """

                                             SELECT *, 
                                                    dbo.Log_Name(SL05_Created_ID) AS [SL05_Created_Name],
                                                    dbo.Log_Name(SL05_Modified_ID) AS [SL05_Modified_Name],
                                                    dbo.Log_Name(SL05_Approved_ID) AS [SL05_Approved_Name]
                                               FROM dbo.SL05_FullScaleCount_Master
                                              WHERE SL05_FullScaleCount_Active_Flag = 'Y'
                                              ORDER BY SL05_FullScaleCount_Order
                             """;
        return await databaseHelper.QueryAsync<FullScaleCountMasterModelRoot>(query);
    }

#endregion

#region DELETE

    public async Task<ResponseResult> DeleteFullScaleCountAsync(int id)
    {
        const string query = "DELETE FROM dbo.SL05_FullScaleCount_Master WHERE SL05_FullScaleCount_ID = @SL05_FullScaleCount_ID";
        return await databaseHelper.ExecuteQueryAsync(query, new() { { "SL05_FullScaleCount_ID", id } });
    }

    public async Task<ResponseResult> DeleteFullScaleCountsAsync(List<int> ids)
    {
        var (inClause, parameters) = DatabaseHelper.BuildInClause(ids, "FullScaleCount_ID");
        var query = $"DELETE FROM dbo.SL05_FullScaleCount_Master WHERE SL05_FullScaleCount_ID IN ({inClause})";
        return await databaseHelper.ExecuteQueryAsync(query, parameters);
    }

#endregion
}