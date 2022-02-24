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
            await _data.LoadMultipleMapDataAsync<TaskModel, dynamic, StatusModel>("[dbo].[spTasks_Get]", new { UserId = userId }, GetTaskModel);

        public async Task<IEnumerable<TaskModel>> GetTasksByStatusAsync(int StatusId, string userId) =>
            await _data.LoadMultipleMapDataAsync<TaskModel, dynamic, StatusModel>("[dbo].[spTasks_GetByStatus]", new { UserId = userId, StatusId }, GetTaskModel);

        public async Task CreateTaskAsync(TaskModel taskModel, string userId) =>
            await _data.SaveDataAsync<dynamic>("[dbo].[spTasks_Insert]", new
            {
                Title = taskModel.Title,
                Description = taskModel.Description,
                StatusId = taskModel.Status.Id,
                UserId = userId
            });

        public async Task UpdateTaskAsync(TaskModel taskModel, string userId)
        {
            await _data.SaveDataAsync<dynamic>("[dbo].[spTasks_Update]", new
            {
                Id = taskModel.Id,
                Title = taskModel.Title,
                Description = taskModel.Description,
                StatusId = taskModel.Status.Id,
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

        private TaskModel GetTaskModel(TaskModel task,StatusModel status)
        {
            task.Status = status;
            return task;
        }
}
}
