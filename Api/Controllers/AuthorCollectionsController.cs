using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using Api.Data;
using Api.Entities;
using Api.Models;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthorCollectionsController : ControllerBase
    {
        private readonly DataContext _context;
        private readonly IMapper _mapper;

        public AuthorCollectionsController(DataContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IEnumerable<AuthorDto>> GetAuthorCollection(IEnumerable<int> ids)
        {
            var authors =  await _context.Authors.Include(a => a.Books).Where(a => ids.Contains(a.Id)).ToListAsync();
            return _mapper.Map<List<AuthorDto>>(authors);
        }

        [HttpPost]
        public async Task<ActionResult> Post([FromBody] IEnumerable<AuthorDto> collection)
        {
            var authors = _mapper.Map<List<Author>>(collection);
            await _context.AddRangeAsync(authors);
            await _context.SaveChangesAsync();

            return Ok(_mapper.Map<List<AuthorDto>>(authors));

        }
    }
}
