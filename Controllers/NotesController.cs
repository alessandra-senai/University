using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using University.Context;
using University.Models;
 
namespace University.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NotesController : ControllerBase
    {
        private UniversityContext _universityContext;
        private readonly ILogger<NotesController> _logger;

        public NotesController(UniversityContext universityContext
            , ILogger<NotesController> logger)
        {
            _universityContext = universityContext;
            _logger = logger;
        }

        /// <summary>
        /// Retorna uma  lista de notas
        /// </summary>
        /// <returns>Retorna notas cadastradas no banco de dados</returns>
        /// <response code="200">Retorna uma lista de notas</response>
        /// <response code="404"> Não encontrou lista de notas</response>
        /// <response code="500">Ocorreu erro durante a execução</response>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<IEnumerable<Note>>> Get()
        {
            try
            {
                var notes = await _universityContext.Notes
                    .Include(x => x.Registration)
                    .Include(y => y.PeriodNote)
                    .ToListAsync();

                _logger.LogInformation($"Class:{nameof(NotesController)}-Method:{nameof(Get)}");


                return notes.Any() ? Ok(notes) : StatusCode(404);
            }
            catch (Exception e)
            {
                _logger.LogError(e, $"Class:{nameof(NotesController)}-Method:{nameof(Get)}");

                return StatusCode(500);
            }

        }

        /// <summary>
        /// Retorna nota
        /// </summary>
        /// <param name="id">Id do Nota</param>
        /// <returns>Retorna nota cadastrada no banco de dados</returns>
        /// <response code="200">Retorna nota</response>
        /// <response code="404"> Não encontrou nota pesquisado</response>
        /// <response code="500">Ocorreu erro durante a execução</response>
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<Note>> GetById(int id)
        {
            try
            {
                var note = await _universityContext.Notes
                    .Include(x => x.Registration)
                    .Include(y => y.PeriodNote)
                    .FirstOrDefaultAsync(x => x.Id == id);

                _logger.LogInformation($"Class:{nameof(NotesController)}-Method:{nameof(GetById)}");


                return note is not null ? Ok(note) : StatusCode(204);
            }
            catch (Exception e)
            {
                _logger.LogError(e, $"Class:{nameof(NotesController)}-Method:{nameof(GetById)}");

                return StatusCode(500);
            }
        }

        /// <summary>
        /// Inserir Nota
        /// </summary>
        /// <param name="note">Nota</param>
        /// <returns>Retorna nota inserido com sucesso no banco de dados</returns>
        /// <response code="201">Nota inserido com sucesso</response>
        /// <response code="404"> Inserção não realizada</response>
        /// <response code="500">Ocorreu erro durante a execução</response>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Post([FromBody] Note note)
        {
            try
            {
                _universityContext.Notes.Add(note);
                await _universityContext.SaveChangesAsync();

                _logger.LogInformation($"Class:{nameof(NotesController)}-Method:{nameof(Post)}");


                return CreatedAtAction("GetById", new { id = note.Id }, note);
            }
            catch (Exception e)
            {
                _logger.LogError(e, $"Class:{nameof(NotesController)}-Method:{nameof(Post)}");

                return StatusCode(500);
            }
        }

        /// <summary>
        /// Atualizar Nota
        /// </summary>
        /// <param name="note">Nota</param>
        /// <returns>Retorna nota atualizada com sucesso no banco de dados</returns>
        /// <response code="201">Nota atualizada com sucesso</response>
        /// <response code="404"> Atualização não realizada</response>
        /// <response code="500">Ocorreu erro durante a execução</response>
        [HttpPut]
        [ProducesResponseType(StatusCodes.Status202Accepted)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Put([FromBody] Note note)
        {
            try
            {
                bool noteExist = _universityContext.Notes.Any(e => e.Id == note.Id);

                if (!noteExist)
                    return NotFound();

                _universityContext.Entry(note).State = EntityState.Modified;

                await _universityContext.SaveChangesAsync();

                _logger.LogInformation($"Class:{nameof(NotesController)}-Method:{nameof(Put)}");

                return StatusCode(202);

            }
            catch (Exception e)
            {
                _logger.LogError(e, $"Class:{nameof(NotesController)}-Method:{nameof(Put)}");

                return StatusCode(500);
            }
        }

        /// <summary>
        /// Remover Nota
        /// </summary>
        /// <param name="id">Id Nota</param>
        /// <returns>Retorna nota excluída com sucesso no banco de dados</returns>
        /// <response code="201">Nota excluída com sucesso</response>
        /// <response code="404"> Exclusão não realizada</response>
        /// <response code="500">Ocorreu erro durante a execução</response>
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var note = await _universityContext.Notes.FindAsync(id);

                if (note is null)
                    return NotFound();

                _universityContext.Notes.Remove(note);
                await _universityContext.SaveChangesAsync();

                _logger.LogInformation($"Class:{nameof(NotesController)}-Method:{nameof(Delete)}");

                return NoContent();
            }
            catch (Exception e)
            {
                _logger.LogError(e, $"Class:{nameof(NotesController)}-Method:{nameof(Delete)}");

                return StatusCode(500);
            }
        }
    }
}