using System;
using System.Collections.Generic;
using System.Linq;

namespace PPObjects
{
    

    public class Round
    {
        
         
        private  List<Dictionary<string, Card>> previousChoices;
        [Obsolete("please use AddCardChoice")]
        public Dictionary<string, Card> cardsChoice { get; set; }
        private IReadOnlyList<Card> cards;
        //public  IReadOnlyList<String> Participants { get; set; }
        [Obsolete("use Table Start Round",true)]
        public Round():this(new List<String>(),new List<Card>(),""){
            }
        internal Round(IReadOnlyList<String>  Participants, IReadOnlyList<Card> cards, string Name)
        {
            previousChoices=new List<Dictionary<string, Card>>();
            cardsChoice=new Dictionary<string, Card>();
            //this.Participants = Participants;
            foreach (var participant in Participants)
            {
                cardsChoice.Add(participant,Card.WithoutChoice);
            }
            this.cards = cards;
            this.Name = Name;
        }

        public void StartNewEstimation()
        {
            var prev = new Dictionary<string, Card>();
            foreach (var card in cardsChoice)
            {
                prev.Add(card.Key,card.Value);
                
            }
            var keys = cardsChoice.Keys.ToArray();
            foreach (var key in keys)
            {
                cardsChoice[key] = Card.WithoutChoice;
            }
            previousChoices.Add(prev);
        }
        public void AddCardChoice(Card c, string participantName)
        {            
            if (!c.Equals(Card.WithoutChoice))
            {
                if (!cards.Contains(c))
                {
                    throw new PPCardNotAllowedException();
                }
            }
            cardsChoice[participantName] = c;
            OnCardChoosen(new MessageCard(){ParticipantName = participantName, Card = c});
        }
        public string Name { get; set; }

        public bool AllParticipantsHaveSelectedCard()
        {
            return !cardsChoice.ContainsValue(Card.WithoutChoice);
        }

        public IEnumerable<KeyValuePair<string, Card>> MinMax()
        {
            if (!AllParticipantsHaveSelectedCard())
                yield break ;

            var values = cardsChoice.Where(it => it.Value.Value.HasValue);
            
            if(!values.Any())
                yield break;
            
            yield return values.OrderBy(it=>it.Value.Value).First();

            yield return values.OrderBy(it=>it.Value.Value).Last();
            


        } 
        public IEnumerable<KeyValuePair<string, Card>> SelectedValues()
        {
            return cardsChoice.Where(it => it.Value.Equals(Card.WithoutChoice));
        }

        public IEnumerable<Card> ParticipantChoices(string participantName)
        {

            foreach (var previousChoice in previousChoices)
            {
                yield return previousChoice[participantName];
            }
            yield return cardsChoice[participantName];

        }

        public event EventHandler<MessageCard> CardChoosen;


        public virtual void OnCardChoosen(MessageCard e)
        {
            var handler = CardChoosen;
            if (handler != null) handler(this, e);

        }
    }
}