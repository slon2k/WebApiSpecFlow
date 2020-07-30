using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Api.Data;
using Api.Entities;
using AutoMapper;
using Api.Models;

namespace Api.Controllers
{
    [Route("api/authors/{authorId}/[controller]")]
    [ApiController]
    public class BooksController : ControllerBase
    {
        private readonly DataContext _context;
        private readonly IMapper _mapper;

        public BooksController(DataContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        // GET: api/Authors/2/Books
        [HttpGet]
        public async Task<ActionResult<IEnumerable<BookDto>>> GetBooksForAuthor(int authorId)
        {
            if (!_context.Authors.Any(a => a.Id == authorId))
            {
                return NotFound();
            }
            var books = await _context.Books.Where(b => b.AuthorId == authorId).ToListAsync();
            return _mapper.Map<List<BookDto>>(books);
        }

        // GET: api/Authors/2/Books/5
        [HttpGet("{id}")]
        public async Task<ActionResult<BookDto>> GetBook(int authorId, int id)
        {
            if (!_context.Authors.Any(a => a.Id == authorId))
            {
                return NotFound();
            }

            var book = await _context.Books.FindAsync(id);

            if (book == null)
            {
                return NotFound();
            }

            if (book.AuthorId != authorId)
            {
                return BadRequest();
            }

            return _mapper.Map<BookDto>(book);
        }

        // PUT: api/Authors/2/Books/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutBook(int authorId, int id, BookUpdateDto bookDto)
        {
            if (!_context.Authors.Any(a => a.Id == authorId))
            {
                return NotFound();
            }
            var book = await _context.Books.FindAsync(id);
            
            if ( book.AuthorId != authorId)
            {
                return BadRequest();
            }

            _context.Entry(book).State = EntityState.Modified;

            _mapper.Map(bookDto, book);

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!BookExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Authors/2/Books
        [HttpPost]
        public async Task<ActionResult<BookDto>> PostBook(int authorId, BookCreateDto bookDto)
        {
            if (!_context.Authors.Any(a => a.Id == authorId))
            {
                return NotFound();
            }

            var book = _mapper.Map<Book>(bookDto);
            book.AuthorId = authorId;

            _context.Books.Add(book);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetBook", new {authorId = authorId, id = book.Id }, _mapper.Map<BookDto>(book));
        }

        // DELETE: api/Authors/2/Books/5
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteBook(int authorId, int id)
        {
            if (!_context.Authors.Any(a => a.Id == authorId))
            {
                return NotFound();
            }

            var book = await _context.Books.FindAsync(id);
            
            if (book == null)
            {
                return NotFound();
            }

            if (book.AuthorId != authorId)
            {
                return BadRequest();
            }

            _context.Books.Remove(book);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool BookExists(int id)
        {
            return _context.Books.Any(e => e.Id == id);
        }
    }
}
