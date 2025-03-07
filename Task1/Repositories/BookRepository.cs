using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Task1.Data;
using Task1.DTOs;
using Task1.Models;

namespace Task1.Repositories
{
	public class BookRepository : IBookRepository
	{
		private readonly BookDbContext _dbContext;
        public BookRepository(BookDbContext dbContext)
        {
			_dbContext = dbContext;   
        }
        public async Task Create(BookDto bookDto)
		{

			var book = new Book
			{	
				Title = bookDto.Title,
				Author = bookDto.Author,
				PublishedDate = bookDto.PublishedDate,
			};


		     await _dbContext.Books.AddAsync(book);
			_dbContext.SaveChangesAsync();
		   
			
		}

		public async Task<bool> DeleteBook(int id)
		{
			var book = await _dbContext.Books.FirstOrDefaultAsync(x => x.Id == id);
			if (book == null)
				return false;


			 _dbContext.Books.Remove(book);
			await _dbContext.SaveChangesAsync();
			
			return true;

		}

		public async Task<IEnumerable<Book>> GetAll()
		{

			return await _dbContext.Books.ToListAsync();
		}

		public async Task<Book> GetBookById(int id)
		{
			var book = await _dbContext.Books.FirstOrDefaultAsync(i => i.Id == id);

			if (book == null)
				return null;

			return book;
		}

		public async Task<Book> UpdateBook(int id, BookDto bookDto)
		{


			var book = await _dbContext.Books.FirstOrDefaultAsync(i => i.Id == id);
			book.Title = bookDto.Title;
			book.Author = bookDto.Author;
			book.PublishedDate = bookDto.PublishedDate;


			_dbContext.Books.Update(book);
			await _dbContext.SaveChangesAsync();


			return book;
		}
	}
}
