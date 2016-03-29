using System.Runtime;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PPObjects;

namespace PPTest2013
{
    [TestClass]
    public class UseCase1CreateTable
    {
        [TestMethod]
        public void UseCase1RightPath()
        {
            var ModeratorName = "ignat andrei";
            var roundName = "UseCase1 - Create Table";
            var table = new TableFactory().CreateTable(ModeratorName).Table;
            table.AddDuration(1);
            table.AddDuration(2);
            table.AddDuration(3);
            table.StartRound(roundName);
            
            Assert.AreNotEqual(0,table.Id.Length);
            Assert.AreEqual(true,table.CanAddParticipant);
            Assert.AreEqual(ModeratorName,table.ModeratorName);
            Assert.AreEqual(1,table.Rounds.Count);
            Assert.AreEqual(roundName, table.Rounds[0].Name);


        }
    }
}
