using Microsoft.AspNetCore.Mvc;
using Task1.DTOs;
using Task1.Models;

namespace Task1.Repositories
{
	public interface IBookRepository
	{
		Task<IEnumerable<Book>> GetAll();
		Task<Book> GetBookById(int id);
		Task<BookDto> Create(BookDto bookDto);
		Task<Book> UpdateBook(int id, BookDto bookDto);
		Task<bool> DeleteBook(int id);
	}
}
