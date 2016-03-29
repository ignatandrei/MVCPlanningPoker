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
        [TestClass]
        public class PpMessagingBootParticipant
        {
            [TestMethod]
            public void BootParticipant()
            {


                BootParticipantAsync().GetAwaiter().GetResult();


            }

            async Task BootParticipantAsync()
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
                await td.Table.BootParticipant(td.ModeratorKey, participant1);

                Assert.AreEqual(1, mt.receiveMessageParticipants.Count); //moderator 

                Assert.AreEqual(0, td.Table.Participants.Count);
                Assert.AreEqual(2, mt.receiveMessageParticipants[0].messagesAdded.Count); //moderator

            }
        }
    }
}
