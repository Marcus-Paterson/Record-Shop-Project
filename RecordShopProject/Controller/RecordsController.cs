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
        public IActionResult GetAllRecords()
        {
            var records = _recordService.GetAllRecords();
            return Ok(records);
        }

        [HttpGet("{id}")]
        public IActionResult GetRecordById(int id)
        {
            var recordId = _recordService.GetRecordById(id);
            if (recordId == null)
            {
                return NotFound();
            }
            return Ok(recordId);
        }

        [HttpPost]
        public IActionResult AddRecord(Record newRecord)
        {
            if (newRecord == null)
            {
                return BadRequest("Record cannot be null");
            }

            var addedRecord = _recordService.AddRecord(newRecord);
            return CreatedAtAction(nameof(GetRecordById), new { id = addedRecord.RecordId }, addedRecord);
        }
    }
}
