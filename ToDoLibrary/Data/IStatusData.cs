using ToDoLibrary.Models;

namespace ToDoLibrary.Data
{
    public interface IStatusData
    {
        Task ChangeStatusActiveFieldAsync(int id, string userId);
        Task CreateStatusAsync(StatusDbModel model, string userId);
        Task<IEnumerable<StatusDbModel>> GetUserStatusesAsync(string userId);
    }
}