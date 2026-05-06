using RecordShopProject.DataModels;
using RecordShopProject.Repository;

namespace RecordShopProject.Service
{
    public interface IRecordsService
    {
        List<Record> GetAllRecords();
        Record GetRecordById(int id);
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


    }
}
