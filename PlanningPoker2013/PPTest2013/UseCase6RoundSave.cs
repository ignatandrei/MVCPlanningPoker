using System.Linq;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PPObjects;

namespace PPTest2013
{
    public class UseCase6RoundSave
    {
        [TestClass]
        public class SaveRpund
        {
            string newParticipantName1 = "new participant";
            string newParticipantName2 = "other participant";
            string newParticipantName3 = "third participant";

            TableData createdTable()
            {
                string ModeratorName = "ignat andrei";
                //var roundName = "UseCase2 - Join Table";
                var td =new TableFactory().CreateTable(ModeratorName);
                td.Table.AddParticipant(newParticipantName1);
                td.Table.AddParticipant(newParticipantName2);
                td.Table.AddParticipant(newParticipantName3);
                return td;
            }

            [TestMethod]
            public void SaveSecondRound()
            {
                SaveSecondRoundAsync().GetAwaiter().GetResult();
            }
            async Task SaveSecondRoundAsync()
            {
                var ct = createdTable();
                var first =await ct.Table.StartRound("first round");
                Assert.AreEqual(1, ct.Table.Rounds.Count);
                first.AddCardChoice(Card.NotSure, newParticipantName1);

                first.AddCardChoice(Card.NotSure, newParticipantName2);
                first.AddCardChoice(Card.NotSure, newParticipantName3);

                var second = await ct.Table.StartRound("second");
                Assert.AreEqual(2, ct.Table.Rounds.Count);

                Assert.AreEqual(second.Name, ct.Table.CurrentRound().Name);
                var arr = ct.Table.MinMax().ToArray();
                
            }
        }



    }
}