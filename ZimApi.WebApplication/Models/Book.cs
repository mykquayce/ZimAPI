using Helpers.Zim.Models.Generated;

namespace ZimApi.WebApplication.Models;

public readonly record struct Book(
	Guid Id,
	Uri Uri,
	string Title,
	string Name,
	string? Flavor,
	DateOnly Date)
{
	public static explicit operator Book(bookType book)
	{
		ArgumentNullException.ThrowIfNull(book);
		ArgumentException.ThrowIfNullOrEmpty(book.id);
		ArgumentException.ThrowIfNullOrEmpty(book.url);
		ArgumentException.ThrowIfNullOrEmpty(book.title);
		ArgumentException.ThrowIfNullOrEmpty(book.name);
		ArgumentOutOfRangeException.ThrowIfEqual(default, book.date);

		return new(
			new Guid(book.id),
			new Uri(book.url, UriKind.Absolute),
			book.title,
			book.name,
			book.flavour,
			DateOnly.FromDateTime(book.date));
	}
}
