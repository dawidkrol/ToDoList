
namespace ToDoLibrary.DbAccess;

public interface ISqlDataAccess
{
    Task<IEnumerable<T>> LoadDataAsync<T, U>(string storedProcedure, U parameters, string connectionId = "Default");
    Task<IEnumerable<T>> LoadMultipleMapDataAsync<T, U, O>(string storedProcedure, U parameters, Func<T, O, T> func, string connectionId = "Default");
    Task SaveDataAsync<T>(string storedProcedire, T parameters, string connectionId = "Default");
}