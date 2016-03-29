using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PPMessagingMediatR;
using PPObjects;

namespace PPTest2013
{
    public partial class PPMessagingVerifier
    {
        [TestClass]
        public class PpMessagingSelectCards
        {
            [TestMethod]
            public void SelectCardOneParticipant()
            {


                var td = SelectCardAsync(1).Result;
                Assert.IsNotNull(td);

            }
            [TestMethod]
            public void SelectCardTwoParticipants()
            {


                var td = SelectCardAsync(2).Result;
                Assert.IsNotNull(td);

            }

            public static async Task<MessageTableData> SelectCardAsync(int nrParticipantsChooseCards)
            {
                var td = await PpMessagingRound.StartRoundAsync();
                
                
                var messagesParticipants= td.receiveMessageParticipants.ToDictionary(it => it.NameParticipant, it => it.messagesAdded.Count);
                var AllParticipants = td.tableData.Table.Participants.ToArray();
                if (nrParticipantsChooseCards > AllParticipants.Length)
                    nrParticipantsChooseCards = AllParticipants.Length;

                var participants = AllParticipants.Select(it => it).Take(nrParticipantsChooseCards).ToList();
                
                while (participants.Count > 0)
                {
                    td.tableData.Table.Rounds[0].AddCardChoice(Card.NotSure, participants[0]);
                    participants.Remove(participants[0]);
                }
                
                

                var messagesParticipantsNew = td.receiveMessageParticipants.ToDictionary(it => it.NameParticipant, it => it.messagesAdded.Count);
                foreach (var i in messagesParticipants)
                {
                    Assert.AreEqual(i.Value + nrParticipantsChooseCards, messagesParticipantsNew[i.Key]);
                }
                return td;
            }
            //[TestMethod]
            //public void EndRound()
            //{


            //    EndRoundAsync().GetAwaiter().GetResult();



            //}

        }
    }
}
