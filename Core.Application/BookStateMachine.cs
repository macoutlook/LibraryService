using Core.Domain;
using Core.Persistence.Contract;

namespace Core.Application;

public class BookStateMachine(IBookStatusRepository bookStatusRepository)
{
    private BookStatus _currentState;

    public async Task DoTransitionAsync(ulong id, BookStatus bookStatus)
    {
        _currentState = Enum.Parse<BookStatus>(await bookStatusRepository.GetStatusAsync(id));

        if (bookStatus.Equals(BookStatus.OnShelf))
            PutOnShelf();

        if (bookStatus.Equals(BookStatus.CheckedOut))
            CheckOut();

        if (bookStatus.Equals(BookStatus.Returned))
            Return();

        if (bookStatus.Equals(BookStatus.BrokenDown))
            MarkAsBrokenDown();
    }


    private void MarkAsBrokenDown()
    {
        if (_currentState is BookStatus.OnShelf or BookStatus.Returned)
            _currentState = BookStatus.CheckedOut;
        else
            throw new InvalidOperationException(
                "Cannot mark book as broken down if it is a book that is not on the shelf or is not returned");
    }

    private void Return()
    {
        if (_currentState == BookStatus.CheckedOut)
            _currentState = BookStatus.Returned;
        else
            throw new InvalidOperationException("Cannot return a book that is not checked out");
    }

    private void PutOnShelf()
    {
        if (_currentState is BookStatus.Returned or BookStatus.BrokenDown)
            _currentState = BookStatus.OnShelf;
        else
            throw new InvalidOperationException(
                "Cannot put book to shelf if it is not already returned or broken down");
    }

    private void CheckOut()
    {
        if (_currentState is BookStatus.OnShelf)
            _currentState = BookStatus.OnShelf;
        else
            throw new InvalidOperationException("Cannot check book out if it is not on shelf");
    }
}