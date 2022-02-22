using ToDoLibrary.DbAccess;
using ToDoLibrary.Models;

namespace ToDoLibrary.Data
{
    public class StatusData : IStatusData
    {
        private readonly ISqlDataAccess _data;

        public StatusData(ISqlDataAccess data)
        {
            _data = data;
        }
        public async Task<IEnumerable<StatusModel>> GetUserStatusesAsync(string userId) =>
            await _data.LoadDataAsync<StatusModel, dynamic>("[dbo].[spStatuses_Get]", new { UserId = userId });
        public async Task CreateStatusAsync(StatusModel model, string userId)
        {
            await _data.SaveDataAsync("[dbo].[spStatuses_Insert]", new
            {
                Title = model.Title,
                UserId = userId,
            });
        }
        public async Task ChangeStatusActiveFieldAsync(StatusModel model) =>
            await _data.SaveDataAsync<dynamic>("[dbo].[spStatuses_ChangeAvailability]", new
            {
                Id = model.Id,
                Avaliable = 0
            });
    }
}
