using DMRVAPI.Repositories.DataModel;
using DMRVAPI.Repositories.Service;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;

namespace DMRVAPI.Controllers
{
    [Route("v01/songs")]
    [EnableCors("WebAPI")]
    [ApiController]
    public class SongDataController : ControllerBase
    {
        private readonly ILogger<SongDataController> _logger;
        private readonly IMariaDbSongService _songDb;

        public SongDataController(ILogger<SongDataController> logger, IMariaDbSongService mariaDbSongService)
        {
            _songDb = mariaDbSongService;
            _logger = logger;
        }

        [HttpGet]
        public async Task<IEnumerable<SongDataModel>> Get()
        {
            return await _songDb.GetList();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<SongDataModel>> Get(ushort id)
        {
            var result = await _songDb.Get(id);
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

            var id = await _songDb.Insert(dto);
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

            var result = await _songDb.Update(dto);
            if (result > 0)
                return NoContent();
            else
                return NotFound();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<SongDataModel>> Delete(int id)
        {
            return BadRequest();

            var result = await _songDb.Delete(id);
            if (result > 0)
                return NoContent();
            else
                return NotFound();
        }
        */
    }
}
