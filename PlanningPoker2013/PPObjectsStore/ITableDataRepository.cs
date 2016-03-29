using System.Collections.Generic;
using System.Threading.Tasks;
using PPObjects;

namespace PPObjectsStore
{
    public interface ITableDataRepository
    {
        IEnumerable<TableData> MyTables(string moderatorKey);
        Task<TableData> Retrieve(string idTable);
        Task<int> Save(TableData td);
    }
}