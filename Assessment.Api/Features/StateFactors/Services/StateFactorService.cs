using Assessment.Api.Features.StateFactors.Models;
using Assessment.Data;
using Dapper;

namespace Assessment.Api.Features.StateFactors.Services;

public class StateFactorService : IStateFactorService
{
    private readonly IDbConnectionFactory _connectionFactory;

    public StateFactorService(IDbConnectionFactory connectionFactory)
    {
        _connectionFactory = connectionFactory;
    }

    public async Task<IEnumerable<StateFactor>> GetAllAsync()
    {
        using var connection = _connectionFactory.CreateConnection();
        var sql = "SELECT State, Factor FROM StateFactors";
        return await connection.QueryAsync<StateFactor>(sql);
    }

    public async Task<StateFactor?> GetByStateAsync(string state)
    {
        using var connection = _connectionFactory.CreateConnection();
        var sql = "SELECT State, Factor FROM StateFactors WHERE State = @State";
        return await connection.QuerySingleOrDefaultAsync<StateFactor>(sql, new { State = state });
    }

    public async Task<StateFactor> CreateAsync(StateFactor factor)
    {
        using var connection = _connectionFactory.CreateConnection();
        var sql = "INSERT INTO StateFactors (State, Factor) VALUES (@State, @Factor)";
        await connection.ExecuteAsync(sql, factor);
        return factor;
    }

    public async Task<bool> UpdateAsync(string state, StateFactor factor)
    {
        if (state != factor.State)
        {
            return false;
        }

        using var connection = _connectionFactory.CreateConnection();
        var sql = "UPDATE StateFactors SET Factor = @Factor WHERE State = @State";
        var rowsAffected = await connection.ExecuteAsync(sql, factor);
        return rowsAffected > 0;
    }

    public async Task<bool> DeleteAsync(string state)
    {
        using var connection = _connectionFactory.CreateConnection();
        var sql = "DELETE FROM StateFactors WHERE State = @State";
        var rowsAffected = await connection.ExecuteAsync(sql, new { State = state });
        return rowsAffected > 0;
    }

    public async Task<bool> ExistsAsync(string state)
    {
        using var connection = _connectionFactory.CreateConnection();
        var sql = "SELECT COUNT(1) FROM StateFactors WHERE State = @State";
        var count = await connection.ExecuteScalarAsync<int>(sql, new { State = state });
        return count > 0;
    }
}
