using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PPMessagingMediatR;

namespace PPTest2013
{
    public partial class PPMessagingVerifier
    {
        public const int MaxParticipants = 3;
        [TestClass]
        public class PpMessagingAddParticipant
        {
            [TestMethod]
            public void AddMultipleParticipants()
            {

               
                var q= AddMultipleParticipantsAsync().Result;
                Assert.IsNotNull(q);


            }

            
            internal static async Task<MessageTableData> AddMultipleParticipantsAsync()
            {
                var mt = new MessageTableData("Moderator");
                var td = mt.tableData;
                Assert.AreEqual(0, td.Table.Participants.Count);
                Assert.AreEqual(1, mt.receiveMessageParticipants.Count); //moderator receiving messages
                await td.Table.AddParticipant(participant1);


                Assert.AreEqual(1, td.Table.Participants.Count);
                Assert.AreEqual(1, mt.receiveMessageParticipants[0].messagesAdded.Count);
                Assert.AreEqual(2, mt.receiveMessageParticipants.Count); //moderator + participant 1
                Assert.AreEqual(0, mt.receiveMessageParticipants[1].messagesAdded.Count);
                await td.Table.AddParticipant(participant2);

                Assert.AreEqual(MaxParticipants, mt.receiveMessageParticipants.Count); //moderator + participant 1+ participant 2

                Assert.AreEqual(2, td.Table.Participants.Count);
                Assert.AreEqual(2, mt.receiveMessageParticipants[0].messagesAdded.Count); //moderator

                Assert.AreEqual(1, mt.receiveMessageParticipants[1].messagesAdded.Count); //participant 1
                Assert.AreEqual(0, mt.receiveMessageParticipants[2].messagesAdded.Count); //participant 2
                return mt;
            }
        }
    }
}
