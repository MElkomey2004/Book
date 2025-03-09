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
	public class AuthorController : ControllerBase
	{
		private readonly IAuthorRepository _authorRepository;

		private readonly IMapper _mapper;

        public AuthorController(IAuthorRepository authorRepository , IMapper mapper)
        {
			_authorRepository = authorRepository;
			_mapper = mapper;	
        }


        [HttpPost]
		[ProducesResponseType(StatusCodes.Status404NotFound)]

		public async Task<IActionResult> Create(AuthorDto authorDto)
		{
			_authorRepository.Create(authorDto);
			return Ok();
		}


		[HttpPost("{id}/books")]
		public async Task<ActionResult<BookDto>> CreateBook(int id, [FromBody] BookDto bookDto)
		{
			var book = await _authorRepository.CreateBook(id, bookDto);

			if (book == null)
				return NotFound();

			return Ok(bookDto);
		}
	



		[HttpGet]
		[ProducesResponseType(StatusCodes.Status200OK)] 
		[ProducesResponseType(StatusCodes.Status404NotFound)] 
		public async Task<IActionResult> GetAll()
		{
			var authors = await _authorRepository.GetAll();	
			if(authors == null)
				return NotFound();

			return Ok(authors);
		}





		[HttpGet("{id}/books")]
		public async Task<IActionResult> GetAllBooks(int id)
		{
			var books = await _authorRepository.GetAllBooks(id);
			if (books == null)
				return NotFound();

			return Ok(books);
		}


		[HttpGet("{AuthorID}/books/{BookID}")]

		public async Task<IActionResult> GetBookBySpecificAuthorID(int AuthorID , int BookID)
		{
			var books = await _authorRepository.GetBookBySpecificAuthorID(AuthorID, BookID);

			if (books == null)
				return NotFound();



			return Ok(books);
		}

		[HttpPut("{AuthorID}/books/{BookID}")]

		public async Task<IActionResult> UpdateBookBySpecificAuthorID(int AuthorID, int BookID , [FromBody] BookDto bookDto)
		{
			var book = await _authorRepository.UpdateBookBySpecificAuthorID(AuthorID, BookID , bookDto);

			if (book == null)
				return NotFound();



			return Ok(book);
		}


		[HttpDelete("{AuthorID}/books/{BookID}")]

		public async Task<IActionResult> DeleteBookBySpecificAuthorID(int AuthorID, int BookID)
		{
			var book = await _authorRepository.DeleteBookBySpecificAuthorID(AuthorID, BookID);

			if (book == false)
				return NotFound();



			return Ok("This is Deleted Successfully");
		}




		[HttpGet]
		[DefaultStatusCode(StatusCodes.Status404NotFound)]
		[Route("{id:int}")]

		public async Task<IActionResult> GetbyId([FromRoute]int id)
		{
			var author = await _authorRepository.GetById(id);
			if(author == null)
				return NotFound();
			return Ok(author);
		}


		[HttpPut]
		[Route("{id:int}")]

		public  async Task<IActionResult> Update([FromRoute] int id , [FromBody] AuthorDto authorDto) 
		{

			var author = await _authorRepository.GetById(id);

			if (author == null)
			{
				return  NotFound();
			}

		
			
			
			author = await _authorRepository.Update(id , authorDto);

			return Ok(author); 

		}


		[HttpDelete]
		[DefaultStatusCode(StatusCodes.Status404NotFound)]
		[Route("{id:int}")]

		public async Task<IActionResult> Delete([FromRoute]int id)
		{
			var author = await _authorRepository.Delete(id);

			if (author == false)
				return NotFound();

			return Ok("Author Is Deleted Successfully");	 


			

		}
	}
}
