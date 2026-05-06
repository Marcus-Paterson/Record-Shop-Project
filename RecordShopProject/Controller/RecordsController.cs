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

        [HttpPut("{id}")]
        public IActionResult EditRecord(int id, [FromBody] Record updatedRecord)
        {
            if (updatedRecord == null)
                return BadRequest("Record cannot be null");

            var editedRecord = _recordService.EditRecord(id, updatedRecord);

            if (editedRecord == null)
            {
                return NotFound();
            }
            return Ok(editedRecord);
        }


        [HttpDelete("{id}")]
        public IActionResult DeleteRecord(int id)
        {
            var deletedRecord = _recordService.DeleteRecord(id);
            if (!deletedRecord)
            {
                return NotFound();
            }
            return NoContent();
        }
    }
}
