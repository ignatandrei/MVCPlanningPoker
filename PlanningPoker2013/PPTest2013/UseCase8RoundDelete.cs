using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PPObjects;

namespace PPTest2013
{
    [TestClass]
    public class UseCase8RoundDelete
    {
        string participant1 = "Part1";
        string participant2 = "Part2";
                
        public TableData createTable()
        {
            var td =new TableFactory().CreateTable("Moderator");
            td.Table.AddParticipant(participant1);
            td.Table.AddParticipant(participant2);
            return td;
        }

        [TestMethod]
        public void DeleteSavedRound()
        {
            
            DeleteSavedRoundAsync().GetAwaiter().GetResult();
        }

        async Task DeleteSavedRoundAsync()
        {
            #region creatingInitialData
            var td = createTable();
            var rd = await td.Table.StartRound("Round1");
            rd.AddCardChoice(1, participant1);
            rd.AddCardChoice(2, participant2);
            Assert.AreEqual(true, rd.AllParticipantsHaveSelectedCard());

            rd = await td.Table.StartRound("Round2");
            rd.AddCardChoice(3, participant1);
            rd.AddCardChoice(5, participant2);
            Assert.AreEqual(true, rd.AllParticipantsHaveSelectedCard());

            rd = await td.Table.StartRound("Round3");
            rd.AddCardChoice(8, participant1);
            rd.AddCardChoice(Card.NotSure, participant2);
            Assert.AreEqual(true, rd.AllParticipantsHaveSelectedCard());

            Assert.AreEqual(3, td.Table.Rounds.Count);
            #endregion


            td.Table.DeleteRound(td.ModeratorKey, "test"); //Attempting to delete a round that doesn't exist            
            Assert.AreEqual(3, td.Table.Rounds.Count); // No rounds were deleted
            bool DeleteNotModeratorSuccess = true;
            try
            {
                td.Table.DeleteRound(td.Table.Id + "NotModerator".GetHashCode(), "Round2");//Attempting to delete a round without being a Moderator
            }
            catch (PPSecurityExceptionModerator)
            {
                DeleteNotModeratorSuccess = false;
            }
            Assert.IsFalse(DeleteNotModeratorSuccess);

            td.Table.DeleteRound(td.ModeratorKey, "Round2"); //Attempting to delete an existing round
            Assert.AreEqual(2, td.Table.Rounds.Count);// 1 round was deleted

            //Check if Round1 and Round3 are intact
            Assert.AreEqual("Round1", td.Table.Rounds.ElementAt(0).Name = "Round1");
            Assert.AreEqual(true, td.Table.Rounds.ElementAt(0).ParticipantChoices(participant1).ElementAt(0).Value == 1);
            Assert.AreEqual(true, td.Table.Rounds.ElementAt(0).ParticipantChoices(participant2).ElementAt(0).Value == 2);

            Assert.AreEqual("Round3", td.Table.Rounds.ElementAt(0).Name = "Round3");
            Assert.AreEqual(true, td.Table.Rounds.ElementAt(1).ParticipantChoices(participant1).ElementAt(0).Value == 8);
            Assert.AreEqual(true, td.Table.Rounds.ElementAt(1).ParticipantChoices(participant2).ElementAt(0) == Card.NotSure);
        }
    }
}
