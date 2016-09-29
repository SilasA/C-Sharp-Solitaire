using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CS_Solitare
{
    /// <summary>
    /// 
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

        public Visibility visibility { get; private set; }
        public Suit CardSuit { get; private set; }
        public int CardId { get; private set; }

        public int parentDeckId { get; set; }

        public bool Covered => visibility == Visibility.Covered;
        public bool Invisible => visibility == Visibility.Invisible;
        public bool Uncovered => visibility == Visibility.Uncovered;

        Card upperCard;
        public Card UpperCard
        {
            get { return upperCard; }
            set { upperCard = value; }
        }
        Card lowerCard;
        public Card LowerCard
        {
            get { return lowerCard; }
            set { lowerCard = value; }
        }

        /// <summary>
        /// Initialization Constructor.
        /// </summary>
        /// <param name="suit">Suit of card</param>
        /// <param name="cardId">Card value 1-13</param>
        public CardData(Suit suit, int cardId, int parentDeckId)
        {
            CardSuit = suit;
            CardId = cardId;
        }

        /// <summary>
        /// Copy Constructor.
        /// </summary>
        /// <param name="copy">Copy</param>
        public CardData(CardData copy)
        {
            CardId = copy.CardId;
            CardSuit = copy.CardSuit;
        }

        /// <summary>
        /// Checks if the card is of a black suit or not.
        /// </summary>
        /// <returns></returns>
        public bool IsBlack()
        {
            return CardSuit == Suit.Clubs || CardSuit == Suit.Spades;
        }
    }
}
