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
        public async Task<IActionResult> GetAllRecords()
        {
            var records = await _recordService.GetAllRecords();
            return Ok(records);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetRecordById(int id)
        {
            var recordId = _recordService.GetRecordById(id);
            if (recordId == null)
            {
                return NotFound();
            }
            return Ok(recordId);
        }

        [HttpPost]
        public async Task<IActionResult> AddRecord(Record newRecord)
        {
            if (newRecord == null)
            {
                return BadRequest("Record cannot be null");
            }

            var addedRecord = await _recordService.AddRecord(newRecord);

            return CreatedAtAction(nameof(GetRecordById), new { id = addedRecord.RecordId }, addedRecord);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> EditRecord(int id, [FromBody] Record updatedRecord)
        {
            if (updatedRecord == null)
                return BadRequest("Record cannot be null");

            var editedRecord = await _recordService.EditRecord(id, updatedRecord);

            if (editedRecord == null)
            {
                return NotFound();
            }
            return Ok(editedRecord);
        }


        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteRecord(int id)
        {
            var deletedRecord = await _recordService.DeleteRecord(id);
            if (!deletedRecord)
            {
                return NotFound();
            }
            return NoContent();
        }
    }
}
