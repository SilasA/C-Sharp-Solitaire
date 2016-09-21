using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CS_Solitare
{
    class CardData
    {
        // Dimensions
        public const int CARDSIZE_X = 140;
        public const int CARDSIZE_Y = 190;

        public enum Suit
        {
            Clubs,
            Hearts,
            Diamonds,
            Spades
        }

        public Suit CardSuit
        {
            get;
            set;
        }
        public int CardId { get; set; }

        /// <summary>
        /// Initialization Constructor.
        /// </summary>
        /// <param name="suit">Suit of card</param>
        /// <param name="cardId">Card value 1-13</param>
        public CardData(Suit suit, int cardId)
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
