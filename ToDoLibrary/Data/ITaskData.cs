using ToDoLibrary.Models;

namespace ToDoLibrary.Data
{
    public interface ITaskData
    {
        Task ChangeTaskActiveFieldAsync(int taskId, string userId);
        Task CreateTaskAsync(TaskDbModel taskModel, string userId);
        Task<IEnumerable<TaskDbModel>> GetTasksAsync(string userId);
        Task<IEnumerable<TaskDbModel>> GetTasksByStatusAsync(int StatusId, string userId);
        Task UpdateTaskAsync(TaskDbModel taskModel, string userId);
        Task UpdateTaskStatusAsync(int taskId, int statusId, string userId);
    }
}