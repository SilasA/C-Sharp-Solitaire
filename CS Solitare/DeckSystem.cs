using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CS_Solitare
{
    class DeckSystem
    {
        List<Tableau> tableau;

        List<Foundation> foundation;

        Hand hand;
        Waste waste;

        public DeckSystem()
        {
            tableau = new List<Tableau>();
            foundation = new List<Foundation>();
        }

        public Deck FindDeckById()
        {
            return new Deck(); // Not implemented
        }

        public Card FindCardById()
        {
            return new Card(); // Not implemented
        }
    }
}
