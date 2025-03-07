using Microsoft.EntityFrameworkCore;
using Task1.Models;

namespace Task1.Data
{
	public class BookDbContext : DbContext
	{
		public BookDbContext(DbContextOptions<BookDbContext> options) : base(options) 
		{
		
		
		
		}

		public DbSet<Book> Books { get; set; }
	}
}
