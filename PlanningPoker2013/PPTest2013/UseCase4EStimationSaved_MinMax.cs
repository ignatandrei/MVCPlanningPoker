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
        public class MinMaxHighlights
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
            public void NoChoosenCards()
            {
                var td = createdTable();
                var rd = td.Table.StartRound("first").Result;
                Assert.AreEqual(0, rd.MinMax().Count());

            }
            [TestMethod]
            public void ChoosenCardsNotSure()
            {
                var td = createdTable();
                var rd = td.Table.StartRound("first").Result;
                rd.AddCardChoice(Card.NotSure,newParticipantName1);
                rd.AddCardChoice(Card.NotSure, newParticipantName2);
                rd.AddCardChoice(Card.NotSure, newParticipantName3);
                Assert.AreEqual(0, rd.MinMax().Count());

            }
            [TestMethod]
            public void ChoosenCardsDecimalValues()
            {
                var td = createdTable();
                var rd = td.Table.StartRound("first").Result;
                rd.AddCardChoice(1, newParticipantName1);
                rd.AddCardChoice(2, newParticipantName2);
                rd.AddCardChoice(3, newParticipantName3);
                Assert.AreEqual(1, rd.MinMax().First().Value);
                Assert.AreEqual(3, rd.MinMax().Last().Value);
                Assert.AreEqual(2, rd.MinMax().Count());

            }
            [TestMethod]
            public void ChoosenCardsSpecialValues()
            {
                var td = createdTable();
                var rd = td.Table.StartRound("first").Result;
                rd.AddCardChoice(1, newParticipantName1);
                rd.AddCardChoice(Card.NotSure, newParticipantName2);
                rd.AddCardChoice(3, newParticipantName3);
                Assert.AreEqual(1, rd.MinMax().First().Value);
                Assert.AreEqual(3, rd.MinMax().Last().Value);
                Assert.AreEqual(2, rd.MinMax().Count());

            }

        }
    }
}