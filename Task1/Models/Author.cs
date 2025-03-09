using System.Text.Json.Serialization;

namespace Task1.Models
{
	public class Author
	{
		public int Id { get; set; }	
		public string Name { get; set; }
		public DateTime BirthDate { get; set; } = DateTime.Now;

		[JsonIgnore]
		public List<Book> Books { get; set; } = new List<Book>();	


	
	}
}
