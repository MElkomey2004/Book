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
		public async Task<BookDto> Create(BookDto bookDto)
		{
			var author = await _dbContext.Authors.FindAsync(bookDto.AuthorId);
			if (author == null)
			{
				throw new Exception("Author not found"); // 🔹 Throw an error instead of returning null
			}

			var book = new Book
			{
				Title = bookDto.Title,
				AuthorId = bookDto.AuthorId,
				PublishedDate = bookDto.PublishedDate,
			};

			await _dbContext.Books.AddAsync(book);
			await _dbContext.SaveChangesAsync(); // ✅ Ensure this runs correctly

			// ✅ Return the saved book
			return new BookDto
			{
				Id = book.Id,
				Title = book.Title,
				AuthorId = book.AuthorId,
				PublishedDate = book.PublishedDate
			};
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
			var books = await _dbContext.Books
				.Include(b => b.Author) // ✅ Ensure Author data is included
				.ToListAsync();

			return books;
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
			book.Author.Id = bookDto.AuthorId;
			book.PublishedDate = bookDto.PublishedDate;


			_dbContext.Books.Update(book);
			await _dbContext.SaveChangesAsync();


			return book;
		}
	}
}
