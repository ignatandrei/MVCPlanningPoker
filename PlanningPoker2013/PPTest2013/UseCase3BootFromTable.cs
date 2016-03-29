using System;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PPObjects;

namespace PPTest2013
{
    [TestClass]
    public class UseCase3BootFromTable
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
        public void UseCase3BootTemporary()
        {
            UseCase3BootTemporaryAsync().GetAwaiter().GetResult();
        }
        public async Task UseCase3BootTemporaryAsync()
        {
            var td = createdTable();
            await td.Table.BootParticipant(td.ModeratorKey, newParticipantName);
            Assert.AreEqual(0,td.Table.Participants.Count);
            await td.Table.AddParticipant(newParticipantName);
            Assert.AreEqual(1, td.Table.Participants.Count);
            
        }

        [TestMethod]
        public void UseCase3BootPermanently()
        {
            UseCase3BootPermanentlyAsync().GetAwaiter().GetResult();
        }

        async Task UseCase3BootPermanentlyAsync()
        {
            var td = createdTable();
            await td.Table.BootParticipant(td.ModeratorKey, newParticipantName, true);
            Assert.AreEqual(0, td.Table.Participants.Count);
            try
            {
                await td.Table.AddParticipant(newParticipantName);
            }
            catch (PPBannedUserException ex)
            {
                Assert.AreEqual(newParticipantName, ex.UserNameBaned);
                return;//expecting this exception    
            }
            Assert.IsTrue(false, "the add participant should be throwing an error");
        }
    }
}