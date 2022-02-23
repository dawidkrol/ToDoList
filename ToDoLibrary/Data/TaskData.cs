using ToDoLibrary.DbAccess;
using ToDoLibrary.Models;

namespace ToDoLibrary.Data
{
    public class TaskData : ITaskData
    {
        private readonly ISqlDataAccess _data;

        public TaskData(ISqlDataAccess data)
        {
            _data = data;
        }

        public async Task<IEnumerable<TaskModel>> GetTasksAsync(string userId) =>
            await _data.LoadDataAsync<TaskModel, dynamic>("[dbo].[spTasks_Get]", new { UserId = userId });

        public async Task<IEnumerable<TaskModel>> GetTasksByStatusAsync(int StatusId, string userId) =>
            await _data.LoadDataAsync<TaskModel, dynamic>("[dbo].[spTasks_GetByStatus]", new { UserId = userId, StatusId });

        public async Task CreateTaskAsync(TaskModel taskModel, string userId, int statusId) =>
            await _data.SaveDataAsync<dynamic>("[dbo].[spTasks_Insert]", new
            {
                Title = taskModel.Title,
                Description = taskModel.Description,
                UserId = userId,
                StatusId = statusId
            });

        public async Task UpdateTaskAsync(TaskModel taskModel, int statusId, string userId)
        {
            await _data.SaveDataAsync<dynamic>("[dbo].[spTasks_Update]", new
            {
                Id = taskModel.Id,
                Title = taskModel.Title,
                Description = taskModel.Description,
                StatusId = statusId,
                UserId = userId
            });
        }

        public async Task UpdateTaskStatusAsync(int taskId, int statusId, string userId)
        {
            await _data.SaveDataAsync<dynamic>("[dbo].[spTasks_UpdateStatus]", new
            {
                Id = taskId,
                StatusId = statusId,
                UserId = userId
            });
        }
        public async Task ChangeTaskActiveFieldAsync(int taskId, string userId)
        {
            await _data.SaveDataAsync<dynamic>("[dbo].[spTasks_ChangeAvailability]", new
            {
                Id = taskId,
                Avaliable = 0,
                UserId = userId
            });
        }
    }
}
