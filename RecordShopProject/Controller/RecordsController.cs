using Microsoft.AspNetCore.Mvc;
using RecordShopProject.DataModels;

namespace RecordShopProject.Controller
{
    [ApiController]
    [Route("api/[controller]")]

    public class RecordsController : ControllerBase
    {
        private readonly Service.IRecordsService _recordService;
        public RecordsController(Service.IRecordsService recordService)
        {
            _recordService = recordService;
        }
        [HttpGet]
        public ActionResult GetAllRecords()
        {
            var records = _recordService.GetAllRecords();
            return Ok(records);
        }
    }
}
