using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PPObjects;

namespace PPTest2013
{
    [TestClass]
    public class UseCase2JoinTable
    {
        [TestMethod]
        public void UseCase2RightPath()
        {
            var ModeratorName = "ignat andrei";
            string newParticipantName = "new participant";
            //var roundName = "UseCase2 - Join Table";
            var table =new TableFactory().CreateTable(ModeratorName).Table;
            var id = table.Id;
            table =new TableFactory().GetTable(id);
            Assert.IsNotNull(table);
            Assert.AreEqual(ModeratorName,table.ModeratorName);
            table.AddParticipant(newParticipantName);
            Assert.AreEqual(1,table.Participants.Count);
            Assert.AreEqual(newParticipantName,table.Participants.First());

        }
    }
}