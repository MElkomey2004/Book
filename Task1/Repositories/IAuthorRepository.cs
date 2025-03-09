using Microsoft.AspNetCore.Mvc;
using System.Net;
using Task1.DTOs;
using Task1.Models;

namespace Task1.Repositories
{
	public interface IAuthorRepository
	{
		Task<IEnumerable<Author>> GetAll();
		Task<GetAllBooksByAuthorDto> GetAllBooks(int id);
		Task<Author> GetById(int id);

		Task<BookDto> GetBookBySpecificAuthorID(int AuthorID , int BookID);
		Task<BookDto> UpdateBookBySpecificAuthorID(int AuthorID , int BookID , BookDto bookDto);

		Task<bool> DeleteBookBySpecificAuthorID(int AuthorID, int BookID);
		Task Create(AuthorDto authorDto);
		Task<BookDto> CreateBook(int id ,BookDto bookDto);
		Task<Author> Update(int id, AuthorDto authorDto);
		Task<bool> Delete(int id);
	}
}
