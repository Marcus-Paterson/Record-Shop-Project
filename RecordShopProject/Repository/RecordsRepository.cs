using RecordShopProject.DataModels;
using System.Text.Json;

namespace RecordShopProject.Repository
{
    public interface IRecordsRepository
    { 
        List<Record> GetAllRecords();
        Record GetRecordById(int id);
        Record AddRecord(Record newRecord);
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
    }
}
