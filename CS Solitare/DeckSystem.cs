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

        /// <summary>
        /// 
        /// </summary>
        public DeckSystem()
        {
            tableau = new List<Tableau>();
            foundation = new List<Foundation>();
        }

        /// <summary>
        /// Checks if a card move is valid through the target deck.
        /// </summary>
        /// <param name="cardToMove">The card or parent card to move</param>
        /// <param name="cardMoveTo">The target card</param>
        /// <returns></returns>
        public bool IsValidMove(Card cardToMove, Card cardMoveTo)
        {
            return 
                FindDeckById(cardMoveTo.parentDeckId).IsValidMove(cardToMove, cardMoveTo);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Deck FindDeckById(int id)
        {
            foreach (Deck deck in tableau)
                if (deck.Id == id) return deck;
            foreach (Deck deck in foundation)
                if (deck.Id == id) return deck;
            if (hand.Id == id) return hand;
            if (waste.Id == id) return waste;
            return null;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Card FindCardById(int id)
        {
            return new Card(); // Not implemented
        }
    }
}
