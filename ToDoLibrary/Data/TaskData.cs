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
            await _data.LoadDataAsync<TaskModel, dynamic>("[dbo].[spTasks_Get]", new { Id = userId });

        public async Task<IEnumerable<TaskModel>> GetTasksByStatusAsync(StatusModel model, string userId) =>
            await _data.LoadDataAsync<TaskModel, dynamic>("[dbo].[spTasks_GetByStatus]", new { UserId = userId, StatusId = model.Id });

        public async Task CreateTaskAsync(TaskModel taskModel, string userId, StatusModel statusModel) =>
            await _data.SaveDataAsync<dynamic>("[dbo].[spTasks_Insert]", new
            {
                Title = taskModel.Title,
                Description = taskModel.Description,
                UserId = userId,
                StatusId = statusModel.Id
            });

        public async Task UpdateTaskAsync(TaskModel taskModel, StatusModel statusModel)
        {
            await _data.SaveDataAsync<dynamic>("[dbo].[spTasks_Update]", new
            {
                Id = taskModel.Id,
                Title = taskModel.Title,
                Description = taskModel.Description,
                StatusId = statusModel.Id
            });
        }

        public async Task UpdateTaskStatusAsync(StatusModel statusModel)
        {
            await _data.SaveDataAsync<dynamic>("[dbo].[spTasks_UpdateStatus]", new
            {
                StatusId = statusModel.Id,
            });
        }
        public async Task ChangeTaskActiveFieldAsync(TaskModel taskModel)
        {
            await _data.SaveDataAsync<dynamic>("[dbo].[spTasks_ChangeAvailability]", new
            {
                Id = taskModel.Id,
                Avaliable = 0
            });
        }
    }
}
