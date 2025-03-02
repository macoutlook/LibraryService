namespace Core.Persistence.Contract;

public interface IBookStatusRepository
{
    Task UpdateStatusAsync(ulong id, string status);
    Task<string> GetStatusAsync(ulong id);
}