using BookStore.Web.Entities;
using BookStore.Web.Repositories;
using BookStore.Web.Services;

using FluentAssertions;

using NSubstitute;
using NSubstitute.ReturnsExtensions;

using Xunit;

namespace BookStore.Web.Tests.Unit;
public class BookServiceTests
{
    private readonly BookService _sut;
    private readonly IBookRepository _bookRepository = Substitute.For<IBookRepository>();

    public BookServiceTests()
    {
        _sut = new BookService(_bookRepository);
    }

    [Fact]
    public async Task GetBooks_ShouldReturnEmptyList_WhenNoRecordExists()
    {
        // Arrange
        _bookRepository.GetBooksAsync().Returns(Enumerable.Empty<Book>());

        // Act
        var result = await _sut.GetBooks();

        // Assert
        result.Should().BeEmpty();
    }

    [Fact]
    public async Task GetBooks_ShouldReturnBooks_WhenRecordExists()
    {
        // Arrange
        var books = new List<Book>
        {
            new Book
            {
                Id = "123",
                Name = "Name",
                AuthorName = "Author",
                Description = "Description",
                ISBN = "ISBN",
                Price = 0.0,
                AddedOn = DateTime.UtcNow
            }
        };
        _bookRepository.GetBooksAsync().Returns(books);

        // Act
        var result = await _sut.GetBooks();

        // Assert
        result.Should().NotBeNullOrEmpty();
        result.Should().HaveCount(1);
    }

    [Fact]
    public async Task GetBookByIdAsync_ReturnsNull_WhenNoRecordsExists()
    {
        // Arrange
        var bookId = "123";
        _bookRepository.GetBookByIdAsync(bookId).ReturnsNull();

        // Act
        var result = await _sut.GetBookByIdAsync(bookId);

        // Assert
        result.Should().BeNull();
    }

    [Fact]
    public async Task GetBookByIdAsync_ReturnsBook_WhenRecordExists()
    {
        // Arrange
        var bookId = "123";

        var expectedBook = new Book
        {
            Id = bookId
        };

        _bookRepository.GetBookByIdAsync(bookId).Returns(expectedBook);

        // Act
        var result = await _sut.GetBookByIdAsync(bookId);

        // Assert
        result.Should().NotBeNull();
        result.Should().BeEquivalentTo(expectedBook, option => option.Excluding(x => x.AddedOn));
    }
}
