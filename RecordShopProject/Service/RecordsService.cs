using RecordShopProject.DataModels;
using RecordShopProject.Repository;

namespace RecordShopProject.Service
{
    public interface IRecordsService
    {
        List<Record> GetAllRecords();
        Record GetRecordById(int id);
        Record AddRecord(Record newRecord);
        Record EditRecord(int id, Record updatedRecord);
        Record DeleteRecord(int id);
    }
    public class RecordsService : IRecordsService
    {
        private readonly IRecordsRepository _recordRepository;
        public RecordsService(IRecordsRepository repository)
        {
            _recordRepository = repository;
        }
        public List<Record> GetAllRecords()
        {
            return _recordRepository.GetAllRecords();
        }

        public Record GetRecordById(int id)
        {
            return _recordRepository.GetRecordById(id);
        }

        public Record AddRecord(Record newRecord)
        {
            return _recordRepository.AddRecord(newRecord);
        }

        public Record EditRecord(int id, Record updatedRecord)
        {
            return _recordRepository.EditRecord(id, updatedRecord);
        }

        public Record DeleteRecord(int id)
        {
                return _recordRepository.DeleteRecord(id);
        }


    }
}
