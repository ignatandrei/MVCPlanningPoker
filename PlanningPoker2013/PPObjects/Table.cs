using System;
using System.Linq;
using System.Linq.Expressions;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PPObjects
{
    public class Table
    {
        public readonly static List<Card> defaultCards1 = new List<Card>();
        static Table()
        {

            defaultCards1.Add(1);
            defaultCards1.Add(2);
            defaultCards1.Add(3);
            defaultCards1.Add(5);
            defaultCards1.Add(8);
            defaultCards1.Add(Card.NotSure);


        }
        [Obsolete("Use CreateTable from TableFactory",true)]
        public Table(): this("", null)
        {

        }
        public string ModeratorKey { get; protected internal set; }
        public readonly IReadOnlyList<Round> Rounds;
        public readonly IReadOnlyList<Card> Cards;
        protected internal Table(string moderatorName, List<Card> cards = null)
        {
            cards = cards ?? defaultCards1;
            Id = Guid.NewGuid().ToString("d");

            ModeratorName = moderatorName;
            Cards = cards;
            Rounds = new List<Round>();
            Participants = new List<string>();
            bannedParticipants = new List<string>();
            ModeratorKey = Id + moderatorName.GetHashCode().ToString();
        }
        public int? CurrentRoundNumber()
        {
            return (Rounds == null || Rounds.Count == 0) ? (int?)null : Rounds.Count;

        }
        public Round CurrentRound()
        {
            return (Rounds == null || Rounds.Count == 0) ? null : Rounds[Rounds.Count - 1] as Round;

        }
        public async Task<Round> ResetRound()
        {
            var c = CurrentRound();
            if (c == null)
                throw new PPNoCurrentRoundException();

            (Rounds as List<Round>).Remove(c);
            return await StartRound(c.Name);

        }
        public IEnumerable<Card> MinMax()
        {
            if (CurrentRound() == null)
            {
                yield break;
            }

            decimal cardMin = 0;
            decimal cardMax = 0;
            foreach (var round in Rounds)
            {
                var minMax = round.MinMax().ToArray();
                if (minMax.Length > 0)
                {
                    cardMin += minMax[0].Value;
                    cardMax += minMax[1].Value;
                }

            }
            yield return cardMin;
            yield return cardMax;



        }
        public async Task<Round> ShowCardsForRound(Round r)
        {
            foreach (var participantChoice in r.cardsChoice)
            {
                OnCardChoosen(new MessageCard(){ParticipantName = participantChoice.Key,Card=participantChoice.Value});
            }
            return r;
        }
        public async Task<Round> ShowCardsForCurrentRound()
        {
            return await ShowCardsForRound(CurrentRound());
        }
        public async Task<Round> StartRound(string roundName)
        {
            var c = CurrentRound();
            if (c != null )
            {
                if (!c.AllParticipantsHaveSelectedCard())
                {
                    throw new PPRoundNotFinishedException();
                }                
                //delete events
                {
                    c.CardChoosen -= r_CardChoosenHidden;
                    await ShowCardsForRound(c);
                }
                //this.SaveRound
            }
            var r = new Round(Participants, Cards, roundName);
            messageCards = new List<MessageCard>();
            r.CardChoosen += r_CardChoosenHidden;

            (Rounds as List<Round>).Add(r);
            OnMessageRound(new MessageRound(){RoundAction = RoundAction.StartRound,RoundName  = roundName});
            return r;
        }

        private List<MessageCard> messageCards;
        /// <summary>
        /// first time call - participant choose card and it is hidden
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void r_CardChoosenHidden(object sender, MessageCard e)
        {
            e.Card = Card.Hidden;
            OnCardChoosen(e);
        }
        public void DeleteRound(string moderatorKey, string roundName)
        {
            if (moderatorKey == ModeratorKey)
            {
                var tmpRound = (Rounds as List<Round>).Find(r => r.Name == roundName);
                (Rounds as List<Round>).Remove(tmpRound);
            }
            else
            {
                throw new PPSecurityExceptionModerator();
            }
        }
        public void AddDuration(int duration)
        {

        }



        public string Id { get;  set; }

        public bool CanAddParticipant
        {
            get
            {
                if (String.IsNullOrWhiteSpace(ModeratorName))
                    return false;

                if (Rounds == null)
                    return false;

                //if (Rounds.Count == 0)
                //    return false;

                return true;
            }

        }

        public string ModeratorName { get; private set; }


        public IReadOnlyList<String> Participants { get; private set; }
        List<String> bannedParticipants { get; set; }
        
        public async Task<bool> AddParticipant(string userName)
        {
            if (!CanAddParticipant)
            {
                return false;
            }

            if (bannedParticipants.Contains(userName))
            {
                throw new PPBannedUserException(userName);
            }

            (Participants as List<string>).Add(userName);
            OnAddedParticipant(new MessageParticipant(){ ParticipantName = userName,Action2Participant = ParticipantAction.Added});
            return true;


        }

        public async Task<bool> BootParticipant(string moderatorKey, string newParticipantName, bool permanently = false)
        {
            if (moderatorKey == ModeratorKey)
            {
                (Participants as List<string>).Remove(newParticipantName);
                if (permanently)
                    bannedParticipants.Add(newParticipantName);

                OnAddedParticipant(new MessageParticipant() { ParticipantName = newParticipantName, Action2Participant = ParticipantAction.Booted });

                return true;
            }
            else
            {
                throw new PPSecurityExceptionModerator();
            }
        }



        public event EventHandler<MessageParticipant> ParticipantMessage;
        protected virtual void OnAddedParticipant(MessageParticipant user)
        {
            var handler = ParticipantMessage;
            if (handler != null) handler(this, user);
        }
        

        public event EventHandler<MessageRound> MessageRound;

        protected virtual void OnMessageRound(MessageRound e)
        {
            var handler = MessageRound;
            if (handler != null) handler(this, e);
        }
        public event EventHandler<MessageCard> CardChoosen;
        protected virtual void OnCardChoosen(MessageCard e)
        {
            var handler = CardChoosen;
            if (handler != null) handler(this, e);
        }
    }

}