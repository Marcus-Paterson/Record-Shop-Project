using RecordShopProject.DataModels;
using System.Text.Json;

namespace RecordShopProject.Repository
{
    public interface IRecordsRepository
    { 
        List<Record> GetAllRecords();
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


    }
}
