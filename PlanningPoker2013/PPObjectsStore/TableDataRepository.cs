using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SQLite;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using PPObjects;

namespace PPObjectsStore
{
    public class TableDataRepository : ITableDataRepository
    {
        private async Task<int> CreateDatabase(string dbName)
        {
            Console.WriteLine("create database");
            SQLiteConnection.CreateFile(dbName);
            using (var dbconn = new SQLiteConnection("Data Source=" + dbName))
            {
                await dbconn.OpenAsync();
                var cmd = dbconn.CreateCommand();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "create table TableData(id VARCHAR, ModeratorKey VARCHAR , json VARCHAR )";
                return await cmd.ExecuteNonQueryAsync();
            }
            
        }
        string _dbName;
        public TableDataRepository(string folderData)
        {
            _dbName = Path.Combine(folderData, "tabledata.sqlite");
            if (!File.Exists(_dbName))
            {
                var nrTables = CreateDatabase(_dbName).Result;

            }
        }
        public async Task<int> Save(TableData td)
        {
            using (var dbconn = new SQLiteConnection("Data Source=" + _dbName))
            {
                await dbconn.OpenAsync();
                var cmd = dbconn.CreateCommand();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "insert into TableData(id , ModeratorKey  , json  ) values(@id , @ModeratorKey , @json )";
                cmd.Parameters.Add("@id", DbType.String).Value = td.Table.Id;
                cmd.Parameters.Add("@ModeratorKey", DbType.String).Value = td.ModeratorKey;
                cmd.Parameters.Add("@json", DbType.String).Value = JsonConvert.SerializeObject(td);
                return await cmd.ExecuteNonQueryAsync();
            }
        }
        public async Task<TableData> Retrieve(string idTable)
        {
            using (var dbconn = new SQLiteConnection("Data Source=" + _dbName))
            {
                await dbconn.OpenAsync();
                var cmd = dbconn.CreateCommand();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "select  json  from TableData where id=@id ";
                cmd.Parameters.Add("@id", DbType.String).Value = idTable;
        
                var json= await cmd.ExecuteScalarAsync();
                return JsonConvert.DeserializeObject<TableData>(json.ToString());

            }
        }
        public IEnumerable<TableData> MyTables(string moderatorKey)
        {
            yield return null;
        }

    }
}
