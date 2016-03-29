using System.Collections.Generic;

namespace PPObjects
{
    public class TableFactory
    {
        static TableFactory()
        {
            tables=new Dictionary<string, Table>()  ;
        }

        protected static Dictionary<string,Table> tables; 
        public virtual TableData CreateTable(string moderatorName)
        {

            var t= new Table(moderatorName);
            tables.Add(t.Id,t);
            return new TableData() {ModeratorKey = t.ModeratorKey, Table = t};
        }

        public Table GetTable(string id)
        {
            return tables[id];
        }
    }
}