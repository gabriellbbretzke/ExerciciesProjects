using AutoMapper;
using FilmesAPI.Data;
using FilmesAPI.Data.DTOs;
using FilmesAPI.Models;
using Microsoft.AspNetCore.Mvc;

namespace FilmesAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class FilmeController : ControllerBase
    {
        private FilmeContext _context;
        private IMapper _mapper;

        public FilmeController(FilmeContext context, IMapper imaper)
        {
            _context = context;
            _mapper = imaper;
        }

        [HttpPost]
        public IActionResult AdicionaFilme([FromBody] CreateFilmeDTO filmeDTO)
        {
            Filme filme = _mapper.Map<Filme>(filmeDTO);
            _context.Filme.Add(filme);
            _context.SaveChanges();
            return CreatedAtAction(nameof(RecuperaFilmesPorId), new { Id = filme.Id}, filme);
        }

        [HttpGet]
        public IEnumerable<Filme> RecuperaFilmes()
        {
            return _context.Filme;
        }

        [HttpGet("{id}")]
        public IActionResult RecuperaFilmesPorId(int id)
        {
            Filme filme = _context.Filme.FirstOrDefault(filme => filme.Id == id);
            if (filme != null)
            {
                ReadFilmeDTO readFilmeDTO = _mapper.Map<ReadFilmeDTO>(filme);
                return Ok(readFilmeDTO);
            }
            return NotFound();
        }

        [HttpPut("{id}")]
        public IActionResult AtualizaFilme(int id, [FromBody] UpdateFilmeDTO filmeDTO)
        {
            Filme filme = _context.Filme.FirstOrDefault(filme => filme.Id == id);
            if (filme == null)
            {
                return NotFound();
            }
            _mapper.Map(filmeDTO, filme);
            _context.SaveChanges();
            return NoContent();
        }

        [HttpDelete]
        public IActionResult DeletaFilme(int id)
        {
            Filme filme = _context.Filme.FirstOrDefault(filme => filme.Id == id);
            if (filme == null)
                return NotFound();
            _context.Remove(filme);
            _context.SaveChanges();
            return NoContent();
        }
    }
}
