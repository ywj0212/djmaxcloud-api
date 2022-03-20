using DMRVAPI.Repositories.DataModel;
using DMRVAPI.Repositories.Service;
using Microsoft.AspNetCore.Authorization;
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
        private readonly ILogger<RecordDataController> _logger;
        private readonly IMariaDbRecordService _recordDb;

        public RecordDataController(ILogger<RecordDataController> logger, IMariaDbRecordService mariaDbRecordService)
        {
            _recordDb = mariaDbRecordService;
            _logger = logger;
        }
    }
}
