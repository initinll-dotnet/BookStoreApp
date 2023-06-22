using BookStore.Web.Models;
using BookStore.Web.Validators;

using FluentAssertions;

using FluentValidation.Results;
using FluentValidation.TestHelper;

using Xunit;

namespace BookStore.Web.Tests.Unit;
public class BookModelValidatorTests
{
    private readonly BookModelValidator _sut;
    public BookModelValidatorTests()
    {
        _sut = new BookModelValidator();
    }

    [Fact]
    public void Should_Have_Error_When_Name_Is_Empty()
    {
        //Arrange
        var bookModel = new BookModel
        {
            Name = "",
            AuthorName = "AuthorName",
            Description = "Description",
            ISBN = "ISBN",
            Price = 0.0
        };

        // Act
        var result = _sut.TestValidate(bookModel);
        //var result = _sut.Validate(bookModel);

        // Assert
        result
            .ShouldHaveValidationErrorFor(book => book.Name)
            .WithErrorMessage("Book Name is required");
    }
}
