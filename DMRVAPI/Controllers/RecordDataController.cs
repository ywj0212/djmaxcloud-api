using DMRVAPI.Repositories.DataModel;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DMRVAPI.Controllers
{
    [Route("v01/records")]
    [EnableCors("WebAPI")]
    [ApiController]
    public class RecordDataController : ControllerBase
    {
        private readonly ILogger<SongDataController> _logger;
        private readonly IMariaDbService _mariaDbTestService;

        public RecordDataController(ILogger<SongDataController> logger, IMariaDbService mariaDbTestService)
        {
            _mariaDbTestService = mariaDbTestService;
            _logger = logger;
        }

        [HttpPost]
        public ActionResult AddRecord(uint user_id, uint steam_id)
        {
            return Ok();
        }
    }
}
