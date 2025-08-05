namespace RTUAutomation.Repository.MasterRepositories.UnitMasterRepositories;

[AutoRegisterService]
public sealed class UnitMasterRepository(DatabaseHelper databaseHelper) : IUnitMasterRepository
{
#region PUT & POST

    public async Task<ResponseResult> AddOrUpdateUnitAsync(UnitMasterModel unitModel)
    {
        Dictionary<string, object> spParameters = new()
        {
            { "SL03_Unit_ID", unitModel.Sl03UnitId ?? 0 },
            { "SL03_Unit_Name", unitModel.Sl03UnitName },
            { "SL03_Unit_Active_Flag", unitModel.Sl03UnitActiveFlag },
            { "SL03_Unit_Order", unitModel.Sl03UnitOrder },
            { CommonConstants.SessionId, unitModel.SessionId }
        };

        return await databaseHelper.ExecuteStoredProcedureWithOutputsAsync("dbo.SL03_Unit_Master_Insert", spParameters);
    }

#endregion

#region GET

    public async Task<UnitMasterModelRoot> GetUnitAsync(int unitId)
    {
        const string query = """
                                             SELECT *,
                                                    dbo.Log_Name(SL03_Created_ID) AS [SL03_Created_Name],
                                                    dbo.Log_Name(SL03_Modified_ID) AS [SL03_Modified_Name],
                                                    dbo.Log_Name(SL03_Approved_ID) AS [SL03_Approved_Name]
                                               FROM dbo.SL03_Unit_Master
                                              WHERE SL03_Unit_ID = @SL03_Unit_ID
                                                AND SL03_Unit_Active_Flag = 'Y'
                             """;
        return await databaseHelper.QuerySingleAsync<UnitMasterModelRoot>(query, new { SL03_Unit_ID = unitId });
    }

    public async Task<IEnumerable<UnitMasterModelRoot>> GetAllUnitsAsync()
    {
        const string query = """
                                             SELECT *,
                                                    dbo.Log_Name(SL03_Created_ID) AS [SL03_Created_Name],
                                                    dbo.Log_Name(SL03_Modified_ID) AS [SL03_Modified_Name],
                                                    dbo.Log_Name(SL03_Approved_ID) AS [SL03_Approved_Name]
                                               FROM dbo.SL03_Unit_Master
                                              WHERE SL03_Unit_Active_Flag = 'Y'
                                              ORDER BY SL03_Unit_Order
                             """;
        return await databaseHelper.QueryAsync<UnitMasterModelRoot>(query);
    }

#endregion

#region DELETE

    public async Task<ResponseResult> DeleteUnitAsync(int unitId)
    {
        const string query = "DELETE FROM dbo.SL03_Unit_Master WHERE SL03_Unit_ID = @SL03_Unit_ID";
        return await databaseHelper.ExecuteQueryAsync(query, new() { { "SL03_Unit_ID", unitId } });
    }

    public async Task<ResponseResult> DeleteUnitsAsync(List<int> unitIds)
    {
        var (inClause, parameters) = DatabaseHelper.BuildInClause(unitIds, "Unit_ID");
        var query = $"DELETE FROM dbo.SL03_Unit_Master WHERE SL03_Unit_ID IN ({inClause})";
        return await databaseHelper.ExecuteQueryAsync(query, parameters);
    }

#endregion
}