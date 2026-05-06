using RecordShopProject.DataModels;
using System.Text.Json;

namespace RecordShopProject.Repository
{
    public interface IRecordsRepository
    { 
        List<Record> GetAllRecords();
        Record GetRecordById(int id);
        Record AddRecord(Record newRecord);
        Record EditRecord(int id,Record updatedRecord);
    }

    public class RecordsRepository : IRecordsRepository
    {
        private readonly RecordShopDBContext _context;
        public RecordsRepository(RecordShopDBContext context)
        {
            _context = context;
        }
        public List<Record> GetAllRecords()
        {
            return _context.Records.ToList();
        }

        public Record GetRecordById(int id) 
        {
            return _context.Records.FirstOrDefault(repo => repo.RecordId == id);
        }

        public Record AddRecord(Record newRecord)
        {
            _context.Records.Add(newRecord);
            _context.SaveChanges();
            return newRecord;
        }

        public Record EditRecord(int id, Record updatedRecord) 
        {
            var record = _context.Records.FirstOrDefault(repo => repo.RecordId == id);
            if (record == null) return null;
            record.Title = updatedRecord.Title;
            record.Artist = updatedRecord.Artist;
            record.Genre = updatedRecord.Genre;
            record.Year = updatedRecord.Year;
            record.Price = updatedRecord.Price;
            record.Stock = updatedRecord.Stock;
            _context.SaveChanges();
            return record;
        }
    }
}
