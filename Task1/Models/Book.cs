using System.ComponentModel.DataAnnotations.Schema;

namespace Task1.Models
{
	public class Book
	{

		public int Id { get; set; }
		public string Title { get; set; }

		public int AuthorId { get; set; }
		public Author Author { get; set; }
		public DateTime PublishedDate { get; set; } = DateTime.Now;



	}
}
