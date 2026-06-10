using Helpers.Zim.Services;
using Microsoft.AspNetCore.Mvc;
using ZimApi.WebApplication.Models;

namespace ZimApi.WebApplication.Controllers;

[Route("[controller]")]
[ApiController]
public class LibraryController(IZimService service) : ControllerBase
{
	[HttpGet]
	public async Task<ICollection<Book>> Get()
	{
		ICollection<Book> books = [];

		await foreach (var book in service.GetBooksAsync())
		{
			books.Add((Book)book);
		}

		return books;
	}
}
