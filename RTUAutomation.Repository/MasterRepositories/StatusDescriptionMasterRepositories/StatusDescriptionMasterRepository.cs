namespace RTUAutomation.Repository.MasterRepositories.StatusDescriptionMasterRepositories;

[AutoRegisterService]
public sealed class StatusDescriptionMasterRepository(DatabaseHelper databaseHelper) : IStatusDescriptionMasterRepository
{
#region PUT & POST

    public async Task<ResponseResult> AddOrUpdateStatusDescriptionAsync(StatusDescriptionMasterModel model)
    {
        Dictionary<string, object> spParameters = new()
        {
            { "SL07_StatusDescription_ID", model.Sl07StatusDescriptionId ?? 0 },
            { "SL07_StatusDescription_Name", model.Sl07StatusDescriptionName },
            { "SL07_StatusDescription_Active_Flag", model.Sl07StatusDescriptionActiveFlag },
            { "SL07_StatusDescription_Order", model.Sl07StatusDescriptionOrder },
            { CommonConstants.SessionId, model.SessionId }
        };

        return await databaseHelper.ExecuteStoredProcedureWithOutputsAsync("dbo.SL07_StatusDescription_Master_Insert", spParameters);
    }

#endregion

#region GET

    public async Task<StatusDescriptionMasterModelRoot> GetStatusDescriptionAsync(int id)
    {
        const string query = """

                                             SELECT *,
                                                    dbo.Log_Name(SL07_Created_ID) AS [SL07_Created_Name],
                                                    dbo.Log_Name(SL07_Modified_ID) AS [SL07_Modified_Name],
                                                    dbo.Log_Name(SL07_Approved_ID) AS [SL07_Approved_Name]
                                               FROM dbo.SL07_StatusDescription_Master
                                              WHERE SL07_StatusDescription_ID = @SL07_StatusDescription_ID
                                                AND SL07_StatusDescription_Active_Flag = 'Y'
                             """;
        return await databaseHelper.QuerySingleAsync<StatusDescriptionMasterModelRoot>(query, new { SL07_StatusDescription_ID = id });
    }

    public async Task<IEnumerable<StatusDescriptionMasterModelRoot>> GetAllStatusDescriptionsAsync()
    {
        const string query = """

                                             SELECT *,
                                                    dbo.Log_Name(SL07_Created_ID) AS [SL07_Created_Name],
                                                    dbo.Log_Name(SL07_Modified_ID) AS [SL07_Modified_Name],
                                                    dbo.Log_Name(SL07_Approved_ID) AS [SL07_Approved_Name]
                                               FROM dbo.SL07_StatusDescription_Master
                                              WHERE SL07_StatusDescription_Active_Flag = 'Y'
                                              ORDER BY SL07_StatusDescription_Order
                             """;
        return await databaseHelper.QueryAsync<StatusDescriptionMasterModelRoot>(query);
    }

#endregion

#region DELETE

    public async Task<ResponseResult> DeleteStatusDescriptionAsync(int id)
    {
        const string query = "DELETE FROM dbo.SL07_StatusDescription_Master WHERE SL07_StatusDescription_ID = @SL07_StatusDescription_ID";
        return await databaseHelper.ExecuteQueryAsync(query, new() { { "SL07_StatusDescription_ID", id } });
    }

    public async Task<ResponseResult> DeleteStatusDescriptionsAsync(List<int> ids)
    {
        var (inClause, parameters) = DatabaseHelper.BuildInClause(ids, "StatusDescription_ID");
        var query = $"DELETE FROM dbo.SL07_StatusDescription_Master WHERE SL07_StatusDescription_ID IN ({inClause})";
        return await databaseHelper.ExecuteQueryAsync(query, parameters);
    }

#endregion
}