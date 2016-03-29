using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PPObjects
{
    public class Card
    {
        internal Card(decimal? val)
        {
            this.Value = val;
            this.Name = val == null ? null : val.ToString();

        }
        public Card(decimal val)
            : this((decimal?)val)
        {

        }
        public decimal? Value { get; set; }
        public string Name { get; private set; }

        public static Card WithoutChoice = new Card(null) { Name = "WithoutChoice", Value = null };
        public static Card CoffeeCup = new Card(null) { Name = "CoffeeCup", Value = null };
        public static Card NotSure = new Card(null) { Name = "NotSure", Value = null };
        public static Card King = new Card(null) { Name = "King", Value = null };
        public static Card Hidden = new Card(null) { Name = "Hidden", Value = null };

        public override bool Equals(object obj)
        {
            var c = obj as Card;
            if (c == null)
                return false;

            if (this.Value != null)
                return this.Value == c.Value;

            return this.Name.Equals(c.Name);
        }

        public override string ToString()
        {
            if (this.Value != null)
                return this.Value.ToString();

            return this.Name;
        }

        public override int GetHashCode()
        {
            if (Value != null)
                return Value.GetHashCode();

            return Name.GetHashCode();
        }

        public static Card operator +(Card a, Card b)
        {
            if (a == null || a.Value == null)
                return b;

            if (b == null || b.Value == null)
                return a;

            return new Card(a.Value.Value + b.Value.Value);


        }

        public static implicit operator decimal(Card a)
        {
            if (a.Value == null)
                throw new ArgumentNullException("card have not a value");

            return a.Value.Value;

        }

        public static implicit operator Card(decimal a)
        {
            return new Card(a);
        }
    }
}
