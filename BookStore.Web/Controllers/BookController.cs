using BookStore.Web.Models;
using BookStore.Web.Services;

using FluentValidation;
using FluentValidation.Results;

using Microsoft.AspNetCore.Mvc;

namespace BookStore.Web.Controllers;
public class BookController : Controller
{
    private readonly IBookService _bookService;
    private readonly IValidator<BookModel> _bookModelValidator;

    public BookController(IBookService bookService, IValidator<BookModel> bookModelValidator)
    {
        _bookService = bookService;
        _bookModelValidator = bookModelValidator;
    }

    public async Task<IActionResult> Index()
    {
        try
        {
            var books = await _bookService.GetBooks();
            return View(books);
        }
        catch (Exception)
        {
            throw;
        }
    }

    public IActionResult Create()
    {
        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(BookModel bookModel)
    {
        try
        {
            var result = _bookModelValidator.Validate(bookModel);

            if (!result.IsValid)
            {
                foreach (ValidationFailure failure in result.Errors)
                {
                    ModelState.AddModelError(failure.PropertyName, failure.ErrorMessage);
                }
                return View(bookModel);
            }

            await _bookService.CreateBookAsync(bookModel);
            return RedirectToAction(nameof(Index));
        }
        catch (Exception)
        {
            throw;
        }
    }

    public async Task<IActionResult> Edit(string id)
    {
        if (string.IsNullOrEmpty(id))
        {
            return NotFound();
        }

        var book = await _bookService.GetBookByIdAsync(id);

        if (book == null)
        {
            return NotFound();
        }

        return View(book);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(string id, BookModel bookModel)
    {
        if (id != bookModel.Id)
        {
            return NotFound();
        }

        var result = _bookModelValidator.Validate(bookModel);

        if (!result.IsValid)
        {
            foreach (ValidationFailure failure in result.Errors)
            {
                ModelState.AddModelError(failure.PropertyName, failure.ErrorMessage);
            }
            return View(bookModel);
        }

        await _bookService.UpdateBookAsync(id, bookModel);
        return RedirectToAction(nameof(Index));
    }

    public async Task<IActionResult> Details(string id)
    {
        if (string.IsNullOrEmpty(id))
        {
            return NotFound();
        }

        var book = await _bookService.GetBookByIdAsync(id);

        if (book == null)
        {
            return NotFound();
        }

        return View(book);
    }

    public async Task<IActionResult> Delete(string id)
    {
        if (string.IsNullOrEmpty(id))
        {
            return NotFound();
        }

        var book = await _bookService.GetBookByIdAsync(id);

        if (book == null)
        {
            return NotFound();
        }

        return View(book);
    }

    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeletePost(string id)
    {
        if (string.IsNullOrEmpty(id))
        {
            return NotFound();
        }

        await _bookService.DeleteBookAsync(id);
        return RedirectToAction(nameof(Index));
    }
}
