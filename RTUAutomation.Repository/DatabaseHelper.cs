namespace RTUAutomation.Repository;

[AutoRegisterService]
public sealed class DatabaseHelper(IConfiguration configuration)
{
#region ADO.NET Execute Methods (For Insert, Update, Delete)

    public async Task<ResponseResult> ExecuteQueryAsync(string sqlQuery, Dictionary<string, object> parameters = null)
    {
        await using var connection = CreateConnection();
        await using var command = new SqlCommand(sqlQuery, connection);
        command.CommandType = CommandType.Text;
        command.CommandTimeout = 30;

        if (parameters is { Count: > 0 })
        {
            foreach (var param in parameters)
            {
                command.Parameters.AddWithValue(param.Key, param.Value ?? DBNull.Value);
            }
        }

        await connection.OpenAsync();
        await using var transaction = await connection.BeginTransactionAsync(IsolationLevel.ReadCommitted);
        command.Transaction = (SqlTransaction)transaction;

        try
        {
            var affectedRows = await command.ExecuteNonQueryAsync();
            if (affectedRows > 0)
            {
                await transaction.CommitAsync();
                Log.Information("ExecuteQueryAsync: Success. Query: {Query}, RowsAffected: {Rows}", sqlQuery, affectedRows);
                return new(1, ResultType.Success, "Operation Successful");
            }

            await transaction.RollbackAsync();
            Log.Warning("ExecuteQueryAsync: No rows affected. Query: {Query}", sqlQuery);
            return new(-1, ResultType.Error, "Operation Failed, Please Contact Your Administrator.");
        }
        catch (SqlException sqlEx)
        {
            await transaction.RollbackAsync();
            Log.Error(sqlEx, "ExecuteQueryAsync: SQL error. Query: {Query}", sqlQuery);
            return new(-1, ResultType.Error, $"A database error occurred: {sqlEx.Message}");
        }
        catch (Exception ex)
        {
            await transaction.RollbackAsync();
            Log.Error(ex, "ExecuteQueryAsync: Unexpected error. Query: {Query}", sqlQuery);
            return new(-1, ResultType.Error, $"An unexpected error occurred: {ex.Message}");
        }
    }

#endregion

#region Helpers

    public static (string inClause, Dictionary<string, object> parameters) BuildInClause<T>(List<T> id, string paramPrefix)
    {
        var parameters = new Dictionary<string, object>();
        var parameterNames = new List<string>();

        for (var i = 0; i < id.Count; i++)
        {
            var paramName = $"@{paramPrefix}{i}";
            parameterNames.Add(paramName);
            parameters.Add(paramName, id[i]);
        }

        var inClause = string.Join(",", parameterNames);
        return (inClause, parameters);
    }

#endregion

#region ConnectionString

    private string GetConnectionStringForCurrentRequest()
    {
        try
        {
            var connStr = configuration.GetConnectionString("DefaultConnection") ?? throw new InvalidOperationException("Connection string not found.");
            return connStr;
        }
        catch (Exception ex)
        {
            Log.Error(ex, "GetConnectionStringForCurrentRequest failed.");
            throw;
        }
    }

    private SqlConnection CreateConnection() => new(GetConnectionStringForCurrentRequest());

#endregion

#region ADO.NET Methods (For Executing Stored Procedures)

    public async Task<int> ExecuteStoredProcedureAsync(string spName, List<SqlParameter> parameters)
    {
        try
        {
            await using var connection = CreateConnection();
            await using var command = new SqlCommand(spName, connection);
            command.CommandType = CommandType.StoredProcedure;

            if (parameters is { Count: > 0 })
            {
                command.Parameters.AddRange(parameters.ToArray());
            }

            await connection.OpenAsync();
            var result = await command.ExecuteNonQueryAsync();
            Log.Information("ExecuteStoredProcedureAsync: Success. SP: {SPName}, Parameters: {Params}", spName, SerializeParams(parameters));
            return result;
        }
        catch (Exception ex)
        {
            Log.Error(ex, "ExecuteStoredProcedureAsync failed. SP: {SPName}, Parameters: {Params}", spName, SerializeParams(parameters));
            throw;
        }
    }

    public async Task<ResponseResult> ExecuteStoredProcedureWithOutputsAsync(string spName, Dictionary<string, object> spParameters)
    {
        try
        {
            var parameters = spParameters.Select(kvp => new SqlParameter(kvp.Key, kvp.Value ?? DBNull.Value));
            await using var connection = CreateConnection();
            await using var command = new SqlCommand(spName, connection);
            command.CommandType = CommandType.StoredProcedure;

            command.Parameters.AddRange(parameters.ToArray());
            command.Parameters.Add(new("ReturnVal", SqlDbType.Int) { Direction = ParameterDirection.Output });
            command.Parameters.Add(new("ReturnType", SqlDbType.VarChar, 10) { Direction = ParameterDirection.Output });
            command.Parameters.Add(new("ReturnMessage", SqlDbType.VarChar, 4000) { Direction = ParameterDirection.Output });

            await connection.OpenAsync();
            await command.ExecuteNonQueryAsync();

            var returnVal = Convert.ToInt32(command.Parameters["ReturnVal"].Value);
            var returnType = command.Parameters["ReturnType"].Value?.ToString();
            var returnMessage = command.Parameters["ReturnMessage"].Value?.ToString();

            Log.Information("ExecuteStoredProcedureWithOutputsAsync: SP: {SPName}, Parameters: {Params}, Returned: {ReturnVal} {ReturnType} {ReturnMessage}", spName, SerializeParams(spParameters), returnVal, returnType, returnMessage);

            return new(returnVal, returnType, returnMessage);
        }
        catch (Exception ex)
        {
            Log.Error(ex, "ExecuteStoredProcedureWithOutputsAsync failed. SP: {SPName}, Parameters: {Params}", spName, SerializeParams(spParameters));
            throw;
        }
    }

#endregion

#region Dapper Methods (For Select Queries Only)

    public async Task<IEnumerable<T>> QueryAsync<T>(string sqlQuery, object parameters = null)
    {
        try
        {
            await using var connection = CreateConnection();
            await connection.OpenAsync();
            DefaultTypeMap.MatchNamesWithUnderscores = true;
            var result = await connection.QueryAsync<T>(sqlQuery, parameters);
            Log.Information("QueryAsync<{Type}>: Success. Query: {Query}, Parameters: {Params}", typeof(T).Name, sqlQuery, SerializeParams(parameters));
            return result;
        }
        catch (SqlException ex) when (ex.Number is 4060 or 18456)
        {
            Log.Warning(ex, "QueryAsync: Invalid tenant or access denied. Query: {Query}, Parameters: {Params}", sqlQuery, SerializeParams(parameters));
            throw new InvalidOperationException("Invalid tenant or access denied.");
        }
        catch (Exception ex)
        {
            Log.Error(ex, "QueryAsync: Error. Query: {Query}, Parameters: {Params}", sqlQuery, SerializeParams(parameters));
            throw new InvalidOperationException("Something went wrong. Please contact your administrator.");
        }
    }

    public async Task<T> QuerySingleAsync<T>(string sqlQuery, object parameters = null)
    {
        try
        {
            await using var connection = CreateConnection();
            await connection.OpenAsync();
            DefaultTypeMap.MatchNamesWithUnderscores = true;
            var result = await connection.QuerySingleOrDefaultAsync<T>(sqlQuery, parameters);
            Log.Information("QuerySingleAsync<{Type}>: Success. Query: {Query}, Parameters: {Params}", typeof(T).Name, sqlQuery, SerializeParams(parameters));
            return result;
        }
        catch (SqlException ex) when (ex.Number is 4060 or 18456)
        {
            Log.Warning(ex, "QuerySingleAsync: Invalid tenant or access denied. Query: {Query}, Parameters: {Params}", sqlQuery, SerializeParams(parameters));
            throw new InvalidOperationException("Invalid tenant or access denied.");
        }
        catch (Exception ex)
        {
            Log.Error(ex, "QuerySingleAsync: Error. Query: {Query}, Parameters: {Params}", sqlQuery, SerializeParams(parameters));
            throw new InvalidOperationException("Something went wrong. Please contact your administrator.");
        }
    }

#region QueryMultipleAsync

    public async Task<(IEnumerable<T1>, IEnumerable<T2>)> QueryMultipleAsync<T1, T2>(string sqlQuery, object parameters = null)
    {
        try
        {
            await using var connection = CreateConnection();
            await connection.OpenAsync();
            await using var multi = await connection.QueryMultipleAsync(sqlQuery, parameters);
            var result1 = await multi.ReadAsync<T1>();
            var result2 = await multi.ReadAsync<T2>();
            Log.Information("QueryMultipleAsync<{T1},{T2}>: Success. Query: {Query}, Parameters: {Params}", typeof(T1).Name, typeof(T2).Name, sqlQuery, SerializeParams(parameters));
            return (result1, result2);
        }
        catch (Exception ex)
        {
            Log.Error(ex, "QueryMultipleAsync<{T1},{T2}>: Error. Query: {Query}, Parameters: {Params}", typeof(T1).Name, typeof(T2).Name, sqlQuery, SerializeParams(parameters));
            throw;
        }
    }

    public async Task<(IEnumerable<T1>, IEnumerable<T2>, IEnumerable<T3>)> QueryMultipleAsync<T1, T2, T3>(string sqlQuery, object parameters = null)
    {
        try
        {
            await using var connection = CreateConnection();
            await connection.OpenAsync();
            await using var multi = await connection.QueryMultipleAsync(sqlQuery, parameters);
            var result1 = await multi.ReadAsync<T1>();
            var result2 = await multi.ReadAsync<T2>();
            var result3 = await multi.ReadAsync<T3>();
            Log.Information("QueryMultipleAsync<{T1},{T2},{T3}>: Success. Query: {Query}, Parameters: {Params}", typeof(T1).Name, typeof(T2).Name, typeof(T3).Name, sqlQuery, SerializeParams(parameters));
            return (result1, result2, result3);
        }
        catch (Exception ex)
        {
            Log.Error(ex, "QueryMultipleAsync<{T1},{T2},{T3}>: Error. Query: {Query}, Parameters: {Params}", typeof(T1).Name, typeof(T2).Name, typeof(T3).Name, sqlQuery, SerializeParams(parameters));
            throw;
        }
    }

    public async Task<(IEnumerable<T1>, IEnumerable<T2>, IEnumerable<T3>, IEnumerable<T4>)> QueryMultipleAsync<T1, T2, T3, T4>(string sqlQuery, object parameters = null)
    {
        try
        {
            await using var connection = CreateConnection();
            await connection.OpenAsync();
            await using var multi = await connection.QueryMultipleAsync(sqlQuery, parameters);
            var result1 = await multi.ReadAsync<T1>();
            var result2 = await multi.ReadAsync<T2>();
            var result3 = await multi.ReadAsync<T3>();
            var result4 = await multi.ReadAsync<T4>();
            Log.Information("QueryMultipleAsync<{T1},{T2},{T3},{T4}>: Success. Query: {Query}, Parameters: {Params}", typeof(T1).Name, typeof(T2).Name, typeof(T3).Name, typeof(T4).Name, sqlQuery, SerializeParams(parameters));
            return (result1, result2, result3, result4);
        }
        catch (Exception ex)
        {
            Log.Error(ex, "QueryMultipleAsync<{T1},{T2},{T3},{T4}>: Error. Query: {Query}, Parameters: {Params}", typeof(T1).Name, typeof(T2).Name, typeof(T3).Name, typeof(T4).Name, sqlQuery, SerializeParams(parameters));
            throw;
        }
    }

    public async Task<(IEnumerable<T1>, IEnumerable<T2>, IEnumerable<T3>, IEnumerable<T4>, IEnumerable<T5>)> QueryMultipleAsync<T1, T2, T3, T4, T5>(string sqlQuery, object parameters = null)
    {
        try
        {
            await using var connection = CreateConnection();
            await connection.OpenAsync();
            await using var multi = await connection.QueryMultipleAsync(sqlQuery, parameters);
            var result1 = await multi.ReadAsync<T1>();
            var result2 = await multi.ReadAsync<T2>();
            var result3 = await multi.ReadAsync<T3>();
            var result4 = await multi.ReadAsync<T4>();
            var result5 = await multi.ReadAsync<T5>();
            Log.Information("QueryMultipleAsync<{T1},{T2},{T3},{T4},{T5}>: Success. Query: {Query}, Parameters: {Params}", typeof(T1).Name, typeof(T2).Name, typeof(T3).Name, typeof(T4).Name, typeof(T5).Name, sqlQuery, SerializeParams(parameters));
            return (result1, result2, result3, result4, result5);
        }
        catch (Exception ex)
        {
            Log.Error(ex, "QueryMultipleAsync<{T1},{T2},{T3},{T4},{T5}>: Error. Query: {Query}, Parameters: {Params}", typeof(T1).Name, typeof(T2).Name, typeof(T3).Name, typeof(T4).Name, typeof(T5).Name, sqlQuery, SerializeParams(parameters));
            throw;
        }
    }

    public async Task<(IEnumerable<T1>, IEnumerable<T2>, IEnumerable<T3>, IEnumerable<T4>, IEnumerable<T5>, IEnumerable<T6>)> QueryMultipleAsync<T1, T2, T3, T4, T5, T6>(string sqlQuery, object parameters = null)
    {
        try
        {
            await using var connection = CreateConnection();
            await connection.OpenAsync();
            await using var multi = await connection.QueryMultipleAsync(sqlQuery, parameters);
            var result1 = await multi.ReadAsync<T1>();
            var result2 = await multi.ReadAsync<T2>();
            var result3 = await multi.ReadAsync<T3>();
            var result4 = await multi.ReadAsync<T4>();
            var result5 = await multi.ReadAsync<T5>();
            var result6 = await multi.ReadAsync<T6>();
            Log.Information("QueryMultipleAsync<{T1},{T2},{T3},{T4},{T5},{T6}>: Success. Query: {Query}, Parameters: {Params}", typeof(T1).Name, typeof(T2).Name, typeof(T3).Name, typeof(T4).Name, typeof(T5).Name, typeof(T6).Name, sqlQuery, SerializeParams(parameters));
            return (result1, result2, result3, result4, result5, result6);
        }
        catch (Exception ex)
        {
            Log.Error(ex, "QueryMultipleAsync<{T1},{T2},{T3},{T4},{T5},{T6}>: Error. Query: {Query}, Parameters: {Params}", typeof(T1).Name, typeof(T2).Name, typeof(T3).Name, typeof(T4).Name, typeof(T5).Name, typeof(T6).Name, sqlQuery, SerializeParams(parameters));
            throw;
        }
    }

    public async Task<(IEnumerable<T1>, IEnumerable<T2>, IEnumerable<T3>, IEnumerable<T4>, IEnumerable<T5>, IEnumerable<T6>, IEnumerable<T7>)> QueryMultipleAsync<T1, T2, T3, T4, T5, T6, T7>(string sqlQuery, object parameters = null)
    {
        try
        {
            await using var connection = CreateConnection();
            await connection.OpenAsync();
            await using var multi = await connection.QueryMultipleAsync(sqlQuery, parameters);
            var result1 = await multi.ReadAsync<T1>();
            var result2 = await multi.ReadAsync<T2>();
            var result3 = await multi.ReadAsync<T3>();
            var result4 = await multi.ReadAsync<T4>();
            var result5 = await multi.ReadAsync<T5>();
            var result6 = await multi.ReadAsync<T6>();
            var result7 = await multi.ReadAsync<T7>();
            Log.Information("QueryMultipleAsync<{T1},{T2},{T3},{T4},{T5},{T6},{T7}>: Success. Query: {Query}, Parameters: {Params}", typeof(T1).Name, typeof(T2).Name, typeof(T3).Name, typeof(T4).Name, typeof(T5).Name, typeof(T6).Name, typeof(T7).Name, sqlQuery, SerializeParams(parameters));
            return (result1, result2, result3, result4, result5, result6, result7);
        }
        catch (Exception ex)
        {
            Log.Error(ex, "QueryMultipleAsync<{T1},{T2},{T3},{T4},{T5},{T6},{T7}>: Error. Query: {Query}, Parameters: {Params}", typeof(T1).Name, typeof(T2).Name, typeof(T3).Name, typeof(T4).Name, typeof(T5).Name, typeof(T6).Name, typeof(T7).Name, sqlQuery, SerializeParams(parameters));
            throw;
        }
    }

    public async Task<(IEnumerable<T1>, IEnumerable<T2>, IEnumerable<T3>, IEnumerable<T4>, IEnumerable<T5>, IEnumerable<T6>, IEnumerable<T7>, IEnumerable<T8>)> QueryMultipleAsync<T1, T2, T3, T4, T5, T6, T7, T8>(string sqlQuery, object parameters = null)
    {
        try
        {
            await using var connection = CreateConnection();
            await connection.OpenAsync();
            await using var multi = await connection.QueryMultipleAsync(sqlQuery, parameters);
            var result1 = await multi.ReadAsync<T1>();
            var result2 = await multi.ReadAsync<T2>();
            var result3 = await multi.ReadAsync<T3>();
            var result4 = await multi.ReadAsync<T4>();
            var result5 = await multi.ReadAsync<T5>();
            var result6 = await multi.ReadAsync<T6>();
            var result7 = await multi.ReadAsync<T7>();
            var result8 = await multi.ReadAsync<T8>();
            Log.Information("QueryMultipleAsync<{T1},{T2},{T3},{T4},{T5},{T6},{T7},{T8}>: Success. Query: {Query}, Parameters: {Params}", typeof(T1).Name, typeof(T2).Name, typeof(T3).Name, typeof(T4).Name, typeof(T5).Name, typeof(T6).Name, typeof(T7).Name, typeof(T8).Name, sqlQuery, SerializeParams(parameters));
            return (result1, result2, result3, result4, result5, result6, result7, result8);
        }
        catch (Exception ex)
        {
            Log.Error(ex, "QueryMultipleAsync<{T1},{T2},{T3},{T4},{T5},{T6},{T7},{T8}>: Error. Query: {Query}, Parameters: {Params}", typeof(T1).Name, typeof(T2).Name, typeof(T3).Name, typeof(T4).Name, typeof(T5).Name, typeof(T6).Name, typeof(T7).Name, typeof(T8).Name, sqlQuery, SerializeParams(parameters));
            throw;
        }
    }

#endregion

#endregion

#region Logging Helpers

    private static readonly JsonSerializerOptions JsonSerializerOptions = new()
    {
        WriteIndented = false,
        DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull,
        ReferenceHandler = ReferenceHandler.IgnoreCycles
    };

    private static string SerializeParams(object parameters)
    {
        try
        {
            return JsonSerializer.Serialize(parameters, JsonSerializerOptions);
        }
        catch (Exception ex)
        {
            Log.Warning(ex, "Parameter serialization failed.");
            return "Parameter serialization failed.";
        }
    }

#endregion
}