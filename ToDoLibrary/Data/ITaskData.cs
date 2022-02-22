using ToDoLibrary.Models;

namespace ToDoLibrary.Data
{
    public interface ITaskData
    {
        Task ChangeTaskActiveFieldAsync(TaskModel taskModel);
        Task CreateTaskAsync(TaskModel taskModel, string userId, StatusModel statusModel);
        Task<IEnumerable<TaskModel>> GetTasksAsync(string userId);
        Task<IEnumerable<TaskModel>> GetTasksByStatusAsync(StatusModel model, string userId);
        Task UpdateTaskAsync(TaskModel taskModel, StatusModel statusModel);
        Task UpdateTaskStatusAsync(StatusModel statusModel);
    }
}