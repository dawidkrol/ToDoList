using ToDoLibrary.Models;

namespace ToDoLibrary.Data
{
    public interface IStatusData
    {
        Task ChangeStatusActiveFieldAsync(int id, string userId);
        Task CreateStatusAsync(StatusModel model, string userId);
        Task<IEnumerable<StatusModel>> GetUserStatusesAsync(string userId);
    }
}