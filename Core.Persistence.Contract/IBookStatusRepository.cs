namespace Core.Persistence.Contract;

public interface IBookStatusRepository
{
    Task UpdateStatusAsync(ulong id, string status, CancellationToken cancellationToken = default);
    Task<string> GetStatusAsync(ulong id, CancellationToken cancellationToken = default);
}