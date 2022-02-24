using ToDoLibrary.Models;

namespace ToDoLibrary.Data
{
    public interface ITaskData
    {
        Task ChangeTaskActiveFieldAsync(int taskId, string userId);
        Task CreateTaskAsync(TaskModel taskModel, string userId);
        Task<IEnumerable<TaskModel>> GetTasksAsync(string userId);
        Task<IEnumerable<TaskModel>> GetTasksByStatusAsync(int StatusId, string userId);
        Task UpdateTaskAsync(TaskModel taskModel, string userId);
        Task UpdateTaskStatusAsync(int taskId, int statusId, string userId);
    }
}