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
        public class FirstRoundChoice
        {
            string newParticipantName = "new participant";

            TableData createdTable()
            {
                string ModeratorName = "ignat andrei";
                //var roundName = "UseCase2 - Join Table";
                var td =new TableFactory().CreateTable(ModeratorName);
                td.Table.AddParticipant(newParticipantName);
                return td;
            }

            [TestMethod]
            public void FirstRoundNoParticipantChoice()
            {
                var td = createdTable();
                var rd = td.Table.StartRound("first").Result;
                Assert.IsFalse(rd.AllParticipantsHaveSelectedCard());

            }

            [TestMethod]
            public void FirstRoundAllParticipantsHaveChosen()
            {
                var td = createdTable();
                var rd = td.Table.StartRound("first").Result;
                rd.AddCardChoice(1, newParticipantName);
                Assert.IsTrue(rd.AllParticipantsHaveSelectedCard());

            }
            [TestMethod]
            public void FirstRoundBadChoose()
            {
                var td = createdTable();
                var rd = td.Table.StartRound("first").Result;
                try
                {
                    rd.AddCardChoice(10000, newParticipantName);
                
                }
                catch (PPCardNotAllowedException)
                {
                    
                    return;//expecting the exception
                }
                Assert.IsTrue(false,"should not come here, but to the exception");

            }
        }
    }
}

