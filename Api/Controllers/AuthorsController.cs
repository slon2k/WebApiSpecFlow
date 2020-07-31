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
    [Route("api/[controller]")]
    [ApiController]
    public class AuthorsController : ControllerBase
    {
        private readonly DataContext _context;
        private readonly IMapper _mapper;

        public AuthorsController(DataContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        // GET: api/Authors
        [HttpGet]
        public async Task<ActionResult<IEnumerable<AuthorDto>>> GetAuthors()
        {
            var authors = await _context.Authors.Include(a => a.Books).ToListAsync();
            return _mapper.Map<List<AuthorDto>>(authors);
        }

        // GET: api/Authors/5
        [HttpGet("{id}")]
        public async Task<ActionResult<AuthorDto>> GetAuthor(int id)
        {
            var author = await _context.Authors.Include(a => a.Books).FirstOrDefaultAsync(a => a.Id == id);

            if (author == null)
            {
                return NotFound();
            }

            return _mapper.Map<AuthorDto>(author);
        }

        // PUT: api/Authors/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutAuthor(int id, AuthorUpdateDto authorDto)
        {
            var author = await _context.Authors.FindAsync(id);

            if (author == null)
            {
                return NotFound();
            }

            _context.Entry(author).State = EntityState.Modified;

            _mapper.Map(authorDto, author);

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AuthorExists(id))
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

        // POST: api/Authors
        [HttpPost]
        public async Task<ActionResult<AuthorDto>> PostAuthor(AuthorCreateDto authorDto)
        {
            var author = _mapper.Map<Author>(authorDto);
            _context.Authors.Add(author);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetAuthor", new { id = author.Id }, _mapper.Map<AuthorDto>(author));
        }

        // DELETE: api/Authors/5
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteAuthor(int id)
        {
            var author = await _context.Authors.FindAsync(id);
            if (author == null)
            {
                return NotFound();
            }

            _context.Authors.Remove(author);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool AuthorExists(int id)
        {
            return _context.Authors.Any(e => e.Id == id);
        }
    }
}
