using Core.Domain;

namespace Core.Application.DomainServices;

public interface IBookStateMachineValidator
{
    Task ValidateAsync(ulong id, BookStatus bookStatus);
}