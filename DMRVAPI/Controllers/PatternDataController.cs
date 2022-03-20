using DMRVAPI.Repositories.DataModel;
using DMRVAPI.Repositories.Service;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DMRVAPI.Controllers
{
    [Route("v01/patterns")]
    [EnableCors("WebAPI")]
    [ApiController]
    public class PatternDataController : ControllerBase
    {
        private readonly ILogger<PatternDataController> _logger;
        private readonly IMariaDbPatternService _patternDb;

        public PatternDataController(ILogger<PatternDataController> logger, IMariaDbPatternService mariaDbPatternService)
        {
            _patternDb = mariaDbPatternService;
            _logger = logger;
        }

        [HttpGet]
        public async Task<IEnumerable<PatternDataModel>> Get()
        {
            return await _patternDb.GetList();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<PatternDataModel>> Get(ushort id)
        {
            var result = await _patternDb.Get(id);
            if (result != default)
                return Ok(result);
            else
                return NotFound();
        }

        /*
        [HttpPost]
        public async Task<ActionResult<SongDataModel>> Insert(SongDataModel dto)
        {
            return BadRequest();

            if (dto.id != null)
            {
                return BadRequest("Id cannot be set for insert action.");
            }

            var id = await _patternDb.Insert(dto);
            if (id != default)
                return CreatedAtRoute("FindOne", new { id }, dto);
            else
                return BadRequest();
        }

        [HttpPut]
        public async Task<ActionResult<SongDataModel>> Update(SongDataModel dto)
        {
            return BadRequest();

            if (dto.id == null)
            {
                return BadRequest("Id should be set for insert action.");
            }

            var result = await _patternDb.Update(dto);
            if (result > 0)
                return NoContent();
            else
                return NotFound();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<SongDataModel>> Delete(int id)
        {
            return BadRequest();

            var result = await _patternDb.Delete(id);
            if (result > 0)
                return NoContent();
            else
                return NotFound();
        }
        */
    }
}
