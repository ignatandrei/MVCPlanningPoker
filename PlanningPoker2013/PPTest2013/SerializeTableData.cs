using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PPObjects;
using PPObjectsStore;

namespace PPTest2013
{
    [TestClass]
    public class SerializeTableData
    {
        string participant1 = "Part1";
        string participant2 = "Part2";

        public TableData createTable()
        {
            var td =new TableFactory().CreateTable("Moderator");
            td.Table.AddParticipant(participant1);
            td.Table.AddParticipant(participant2);
            return td;
        }

        [TestMethod]
        public void SaveTable()
        {
            
            SaveTableAsync().GetAwaiter().GetResult();


        }

        async Task SaveTableAsync()
        {
            var td = createTable();
            var rd = await td.Table.StartRound("Round1");
            rd.AddCardChoice(1, participant1);
            rd.AddCardChoice(2, participant2);
            rd = await td.Table.StartRound("Round2");
            rd.AddCardChoice(3, participant1);
            rd.AddCardChoice(5, participant2);

            var rep = new TableDataRepository(Environment.CurrentDirectory);
            var nr = rep.Save(td).Result;
            Assert.IsTrue(nr > 0, "must have saved 1 record at least");
        }

        [TestMethod]
        public void SaveAndRetrieveTable()
        {
            SaveAndRetrieveTableAsync().GetAwaiter().GetResult();
        }

        async Task SaveAndRetrieveTableAsync()
        {

            var td = createTable();
            var rd =await td.Table.StartRound("Round1");
            rd.AddCardChoice(1, participant1);
            rd.AddCardChoice(2, participant2);
            rd =await td.Table.StartRound("Round2");
            rd.AddCardChoice(3, participant1);
            rd.AddCardChoice(5, participant2);

            var rep = new TableDataRepository(Environment.CurrentDirectory);
            var nr = rep.Save(td).Result;

           var tdNew= rep.Retrieve(td.Table.Id).Result;

            Assert.AreEqual(td.ModeratorKey, tdNew.ModeratorKey);
            Assert.AreEqual(td.Table.Id, tdNew.Table.Id);
            Assert.AreEqual(td.Table.Rounds.Count, tdNew.Table.Rounds.Count);
            var r = td.Table.Rounds[0];
            var rNew = tdNew.Table.Rounds[0];
            Assert.AreEqual(r.Name, rNew.Name);
            Assert.AreEqual(r.ParticipantChoices(participant1).Count(), rNew.ParticipantChoices(participant1).Count());

        }
    }
}
