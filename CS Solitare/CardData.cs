using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CS_Solitare
{
    /// <summary>
    /// Contains all of the data per card.
    /// </summary>
    class CardData
    {
        public enum Suit
        {
            Clubs,
            Hearts,
            Diamonds,
            Spades
        }
        public enum Visibility
        {
            Invisible,
            Covered,
            Uncovered
        }

        public Visibility visibility { get; set; }
        public Suit CardSuit { get; private set; }
        public int CardId { get; private set; }

        public int parentDeckId { get; set; }
        public int currentIndex { get; private set; }

        public bool Covered => visibility == Visibility.Covered;
        public bool Invisible => visibility == Visibility.Invisible;
        public bool Uncovered => visibility == Visibility.Uncovered;

        public int parentCard { get; set; }
        public int childCard { get; set; }

        /// <summary>
        /// Initialization Constructor.
        /// </summary>
        /// <param name="suit">Suit of card</param>
        /// <param name="cardId">Card value 1-13</param>
        /// <param name="parentDeckId">Id of the containing deck</param>
        /// <param name="currentIndex">Location in datum list</param>
        public CardData(Suit suit, int cardId, int currentIndex)
        {
            CardSuit = suit;
            CardId = cardId;
            this.currentIndex = currentIndex;
            visibility = Visibility.Invisible;
        }

        /// <summary>
        /// Checks if the card is of a black suit or not.
        /// </summary>
        /// <returns></returns>
        public bool IsBlack()
        {
            return CardSuit == Suit.Clubs || CardSuit == Suit.Spades;
        }

        /// <summary>
        /// 
        /// </summary>
        public void SetParentChild()
        {
            
        }
    }
}
