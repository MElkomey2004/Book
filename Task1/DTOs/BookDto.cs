using System.ComponentModel.DataAnnotations.Schema;
using Task1.Models;

namespace Task1.DTOs
{
	public class BookDto
	{
		
		public int Id { get; set; }	
		public string Title { get; set; }

		public DateTime PublishedDate { get; set; } = DateTime.Now;

		public int AuthorId { get; set; }
	

	}
}
