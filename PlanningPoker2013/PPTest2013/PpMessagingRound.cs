using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PPMessagingMediatR;

namespace PPTest2013
{
    public partial class PPMessagingVerifier
    {
        [TestClass]
        public class PpMessagingRound
        {
            [TestMethod]
            public void StartRound()
            {


                var td = StartRoundAsync().Result;
                Assert.IsNotNull(td);




            }

            public static async Task<MessageTableData> StartRoundAsync()
            {
                var td =await PpMessagingAddParticipant.AddMultipleParticipantsAsync();
                
                var messagesParticipants =td.receiveMessageParticipants.ToDictionary(it => it.NameParticipant, it => it.messagesAdded.Count);
                Assert.AreEqual(MaxParticipants, messagesParticipants.Keys.Count);
                await td.tableData.Table.StartRound("login");

                var messagesParticipantsNew = td.receiveMessageParticipants.ToDictionary(it => it.NameParticipant, it => it.messagesAdded.Count);
                foreach (var i in messagesParticipants)
                {
                    Assert.AreEqual(i.Value+1,messagesParticipantsNew[i.Key]);
                }
                return td;
            }

            [TestMethod]
            public void ShowCardsForRound()
            {
                ShowCardsForRoundAsync().GetAwaiter().GetResult();
            }
            async Task ShowCardsForRoundAsync()
            {


                var td= await PpMessagingSelectCards.SelectCardAsync(int.MaxValue);
                var messagesParticipants = td.receiveMessageParticipants.ToDictionary(it => it.NameParticipant, it => it.messagesAdded.Count);
                Assert.AreEqual(MaxParticipants, messagesParticipants.Keys.Count);
                
                await td.tableData.Table.ShowCardsForCurrentRound();


                var messagesParticipantsNew = td.receiveMessageParticipants.ToDictionary(it => it.NameParticipant, it => it.messagesAdded.Count);
                foreach (var i in messagesParticipants)
                {
                    //each receive message from all others
                    Assert.AreEqual(i.Value + MaxParticipants-1, messagesParticipantsNew[i.Key]);
                }

            }
            
        }
    }
}
