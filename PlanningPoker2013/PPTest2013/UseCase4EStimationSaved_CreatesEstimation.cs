using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PPObjects;

namespace PPTest2013
{
    
    public partial class UseCase4EstimationSaved
    {
        
        [TestClass]
        public class CreatesEstimation
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
            public void EstimationSaved()
            {
               
                EstimationSavedAsync().GetAwaiter().GetResult();
            }

            async Task EstimationSavedAsync()
            {
                var td = createdTable();
                var rd = await td.Table.StartRound("first");
                rd.AddCardChoice(1, newParticipantName1);
                rd.AddCardChoice(Card.NotSure, newParticipantName2);
                rd.AddCardChoice(3, newParticipantName3);

                rd.StartNewEstimation();
                rd.AddCardChoice(1, newParticipantName1);
                rd.AddCardChoice(3, newParticipantName2);

                var est = rd.ParticipantChoices(newParticipantName1).ToArray();
                Assert.AreEqual(1, est.First());
                Assert.AreEqual(1, est.Last());

                est = rd.ParticipantChoices(newParticipantName2).ToArray();
                Assert.AreEqual(Card.NotSure, est.First());
                Assert.AreEqual(3, est.Last());

                est = rd.ParticipantChoices(newParticipantName3).ToArray();
                Assert.AreEqual(3, est.First());
                Assert.AreEqual(Card.WithoutChoice, est.Last());

            }

        }
    }
}

