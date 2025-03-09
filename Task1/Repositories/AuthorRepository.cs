using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Net;
using Task1.Data;
using Task1.DTOs;
using Task1.Models;

namespace Task1.Repositories
{
	public class AuthorRepository : IAuthorRepository
	{
		private readonly BookDbContext _dbContext;
        public AuthorRepository(BookDbContext dbContext)
        {
			_dbContext = dbContext;   
        }
        public async Task Create(AuthorDto authorDto)
		{

			var Author = new Author
			{	
			
				Name = authorDto.Name,
				BirthDate = authorDto.BirthDate,
			};
	

		     await _dbContext.Authors.AddAsync(Author);
			_dbContext.SaveChangesAsync();
		   
			
		}

		public async Task<BookDto> CreateBook(int id, BookDto bookDto)
		{
			var author = await _dbContext.Authors.FindAsync(id);
			if (author == null)
			{
				return null;
			}

			var book = new Book
			{
				Id = bookDto.Id,
				AuthorId = id,
				Title = bookDto.Title,
				PublishedDate = bookDto.PublishedDate,
			};

			await _dbContext.Books.AddAsync(book);
			await _dbContext.SaveChangesAsync(); 

			bookDto.AuthorId = author.Id;
			bookDto.Title = book.Title;
			bookDto.PublishedDate = book.PublishedDate;


			return bookDto;
		}

		public async Task<bool> Delete(int id)
		{
			var Author = await _dbContext.Authors.FirstOrDefaultAsync(x => x.Id == id);
			if (Author == null)
				return false;


			 _dbContext.Authors.Remove(Author);
			await _dbContext.SaveChangesAsync();
			
			return true;

		}

		public async Task<IEnumerable<Author>> GetAll()
		{

			return await _dbContext.Authors.ToListAsync();
		}

		public async Task<GetAllBooksByAuthorDto> GetAllBooks(int id)
		{
			var author = await _dbContext.Authors.FirstOrDefaultAsync(x => x.Id == id);
			if (author == null)
				return null;  

			var books = await _dbContext.Books
				.Where(b => b.AuthorId == id)
				.Select(b => new BookDto
				{
					Id = b.Id,
					Title = b.Title,
					PublishedDate = b.PublishedDate
					
				})
				.ToListAsync();

			return new GetAllBooksByAuthorDto { Books = books };

		}

		public async Task<Author> GetById(int id)
		{
			var Author = await _dbContext.Authors.FirstOrDefaultAsync(i => i.Id == id);

			if (Author == null)
				return null;

			return Author;
		}


		public async Task<BookDto> GetBookBySpecificAuthorID(int AuthorID , int BookID)
		{
			var book = await _dbContext.Books
			  .Where(b => b.Id == BookID && b.AuthorId == AuthorID)
			  .Select(b => new BookDto
			  {
				  Id = b.Id,
				  Title = b.Title,
				  PublishedDate = b.PublishedDate,
				  AuthorId = b.AuthorId
			  })
			  .FirstOrDefaultAsync();

			return book; 
		}

		public async Task<BookDto> UpdateBookBySpecificAuthorID(int AuthorID, int BookID , BookDto bookDto)
		{
			var book = await _dbContext.Books
			  .Where(b => b.Id == BookID && b.AuthorId == AuthorID)
			  .FirstOrDefaultAsync();

			if (book == null)
				return null;

			book.Title = bookDto.Title;
			book.PublishedDate = bookDto.PublishedDate;

			_dbContext.Books.Update(book);

			_dbContext.SaveChangesAsync();


			return new BookDto
			{
				Id = book.Id,
				Title = book.Title,
				PublishedDate = book.PublishedDate,
				AuthorId = book.AuthorId
			};
		}


		public async Task<Author> Update(int id, AuthorDto authorDto)
		{


			var author = await _dbContext.Authors.FirstOrDefaultAsync(i => i.Id == id);
			author.Name = authorDto.Name;
			author.BirthDate = authorDto.BirthDate;


			_dbContext.Authors.Update(author);
			await _dbContext.SaveChangesAsync();


			return author;
		}

		public async Task<bool> DeleteBookBySpecificAuthorID(int AuthorID, int BookID)
		{
			var book = _dbContext.Books.Where(x => x.Id == BookID && x.AuthorId == AuthorID).FirstOrDefault();

			if (book == null)
				return false;

			_dbContext.Books.Remove(book);
			await _dbContext.SaveChangesAsync();

			return true;

		}
	}
}
