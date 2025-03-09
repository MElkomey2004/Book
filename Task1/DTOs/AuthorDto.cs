using System.ComponentModel.DataAnnotations.Schema;
using Task1.Models;

namespace Task1.DTOs
{
	public class AuthorDto
	{

		public string Name { get; set; }
		public DateTime BirthDate { get; set; } = DateTime.Now;

	}
}
