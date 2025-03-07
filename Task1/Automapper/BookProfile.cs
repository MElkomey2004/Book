using AutoMapper;
using Task1.DTOs;
using Task1.Models;

namespace Task1.Automapper
{
	public class BookProfile : Profile
	{

		public BookProfile()
		{
			// Map Book -> BookDto
			CreateMap<Book, BookDto>();

			// Map BookDto -> Book
			CreateMap<BookDto, Book>();
		}
	}
}
