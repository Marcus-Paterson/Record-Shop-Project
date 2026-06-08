using RecordShopProject.DataModels;
using RecordShopProject.Repository;

namespace RecordShopProject.Service
{
    public interface IRecordsService
    {
        Task<List<Record>> GetAllRecords();
        Task<Record> GetRecordById(int id);
        Task<Record> AddRecord(Record newRecord);
        Task<Record> EditRecord(int id, Record updatedRecord);
        Task<bool> DeleteRecord(int id);
    }
    public class RecordsService : IRecordsService
    {
        private readonly IRecordsRepository _recordRepository;
        public RecordsService(IRecordsRepository repository)
        {
            _recordRepository = repository;
        }
        public async Task<List<Record>> GetAllRecords()
        {
            return await _recordRepository.GetAllRecords();
        }

        public async Task<Record> GetRecordById(int id)
        {
            if (id <= 0)
            {
                throw new ArgumentException("Invalid record ID");
            }
            return await _recordRepository.GetRecordById(id);
        }

        public async Task<Record> AddRecord(Record newRecord)
        {
            return await _recordRepository.AddRecord(newRecord);
        }

        public async Task<Record> EditRecord(int id, Record updatedRecord)
        {
            return await _recordRepository.EditRecord(id, updatedRecord);
        }

        public async Task<bool> DeleteRecord(int id)
        {
            return await _recordRepository.DeleteRecord(id);
        }
    }
}
