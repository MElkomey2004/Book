namespace Task1.DTOs
{
	public class BookDto
	{
		
		public string Title { get; set; }
		public string Author { get; set; }

		public DateTime PublishedDate { get; set; } = DateTime.Now;
	}
}
