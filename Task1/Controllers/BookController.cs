using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Task1.DTOs;
using Task1.Models;
using Task1.Repositories;

namespace Task1.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class BookController : ControllerBase
	{
		private readonly IBookRepository _bookRepository;

		private readonly IMapper _mapper;

        public BookController(IBookRepository bookRepository , IMapper mapper)
        {
			_bookRepository = bookRepository;
			_mapper = mapper;	
        }


        [HttpPost]
		[ProducesResponseType(StatusCodes.Status404NotFound)]

		public async Task<IActionResult> Create(BookDto bookDto)
		{
			var book = await _bookRepository.Create(bookDto);

			if (book == null)
			{
				return NotFound();
			}
			return Ok("The Book Will Added Successfully");
		}

		[HttpGet]
		[ProducesResponseType(StatusCodes.Status200OK)] 
		[ProducesResponseType(StatusCodes.Status404NotFound)] 
		public async Task<IActionResult> GetAll()
		{
			var books = await _bookRepository.GetAll();	
			if(books == null)
				return NotFound();

			return Ok(books);
		}


		[HttpGet]
		[DefaultStatusCode(StatusCodes.Status404NotFound)]
		[Route("{id:int}")]

		public async Task<IActionResult> GetbyId([FromRoute]int id)
		{
			var book = await _bookRepository.GetBookById(id);
			if(book == null)
				return NotFound();
			return Ok(book);
		}


		[HttpPut]
		[Route("{id:int}")]

		public  async Task<IActionResult> Update([FromRoute] int id , [FromBody] BookDto bookDto) 
		{

			var book = await _bookRepository.GetBookById(id);

			if (book == null)
			{
				return  NotFound();
			}

		
			
			
			book = await _bookRepository.UpdateBook(id , bookDto);

			return Ok(book); 

		}


		[HttpDelete]
		[DefaultStatusCode(StatusCodes.Status404NotFound)]
		[Route("{id:int}")]

		public async Task<IActionResult> Delete([FromRoute]int id)
		{
			var book = await _bookRepository.DeleteBook(id);

			if (book == false)
				return NotFound();

			return Ok("book Is Deleted Successfully");	 


			

		}
	}
}
