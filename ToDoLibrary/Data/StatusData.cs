using Microsoft.Extensions.Logging;
using System.Data.SqlClient;
using ToDoLibrary.DbAccess;
using ToDoLibrary.Models;

namespace ToDoLibrary.Data
{
    public class StatusData : IStatusData
    {
        private readonly ISqlDataAccess _data;
        private readonly ILogger<StatusData> _logger;

        public StatusData(ISqlDataAccess data, ILogger<StatusData> logger)
        {
            _data = data;
            _logger = logger;
        }
        public async Task<IEnumerable<StatusDbModel>> GetUserStatusesAsync(string userId)
        {
            try
            {
                return await _data.LoadDataAsync<StatusDbModel, dynamic>("[dbo].[spStatuses_Get]", new { UserId = userId });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Exception when loading statuses from database");
                throw;
            }
        }
        public async Task CreateStatusAsync(StatusDbModel model, string userId)
        {
            try
            {
                await _data.SaveDataAsync("[dbo].[spStatuses_Insert]", new
                {
                    Title = model.Title,
                    UserId = userId,
                });
                _logger.LogTrace("Status has been added to database");
            }
            catch(SqlException ex)
            {
                _logger.LogError(ex, "Exception when adding a status from database");
                throw;
            }
        }
        public async Task ChangeStatusActiveFieldAsync(int id, string userId)
        {
            try
            {
                await _data.SaveDataAsync<dynamic>("[dbo].[spStatuses_ChangeAvailability]", new
                {
                    Id = id,
                    Avaliable = 0,
                    UserId = userId
                });
                _logger.LogTrace("Field active has been changed in database");
            }
            catch (SqlException ex)
            {
                _logger.LogError(ex, "Exception when changing active field in statusin database");
                throw;
            }
        }
    }
}
