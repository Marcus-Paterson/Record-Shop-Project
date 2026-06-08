using Microsoft.EntityFrameworkCore;
using RecordShopProject.DataModels;
using System.Text.Json;

namespace RecordShopProject.Repository
{
    public interface IRecordsRepository
    {
        Task<List<Record>> GetAllRecords();
        Task<Record> GetRecordById(int id);
        Task<Record> AddRecord(Record newRecord);
        Task<Record> EditRecord(int id, Record updatedRecord);
        Task<bool> DeleteRecord(int id);
    }

    public class RecordsRepository : IRecordsRepository
    {
        private readonly RecordShopDBContext _context;

        public RecordsRepository(RecordShopDBContext context)
        {
            _context = context;
        }

        public async Task<List<Record>> GetAllRecords()
        {
            return await _context.Records.ToListAsync();
        }

        public async Task<Record?> GetRecordById(int id)
        {
            return await _context.Records.FirstOrDefaultAsync(r => r.RecordId == id);
        }

        public async Task<Record> AddRecord(Record newRecord)
        {
            await _context.Records.AddAsync(newRecord);
            await _context.SaveChangesAsync();
            return newRecord;
        }

        public async Task<Record?> EditRecord(int id, Record updatedRecord)
        {
            var record = await _context.Records.FirstOrDefaultAsync(r => r.RecordId == id);
            if (record == null) return null;

            record.Title = updatedRecord.Title;
            record.Artist = updatedRecord.Artist;
            record.Genre = updatedRecord.Genre;
            record.Year = updatedRecord.Year;
            record.Price = updatedRecord.Price;
            record.Stock = updatedRecord.Stock;

            await _context.SaveChangesAsync();
            return record;
        }

        public async Task<bool> DeleteRecord(int id)
        {
            var record = await _context.Records.FirstOrDefaultAsync(r => r.RecordId == id);
            if (record == null) return false;

            _context.Records.Remove(record);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
