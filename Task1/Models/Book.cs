﻿namespace Task1.Models
{
	public class Book
	{

		public int Id { get; set; }
		public string Title { get; set; }
		public string Author { get; set; }

		public DateTime PublishedDate { get; set; } = DateTime.Now;
	}
}
