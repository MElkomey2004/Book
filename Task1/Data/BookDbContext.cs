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
		public DbSet<Author> Authors { get; set; }



		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			modelBuilder.Entity<Book>()
				.HasOne(b => b.Author)  
				.WithMany(a => a.Books) 
				.HasForeignKey(b => b.AuthorId) 
				.OnDelete(DeleteBehavior.Cascade); 
		}
	}
}
