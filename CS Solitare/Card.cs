using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;

namespace CS_Solitare
{
    class Card
    {
        // Dimensions
        const int CARDSIZE_X = 140;
        const int CARDSIZE_Y = 190;

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
        int cardId;

        public bool Covered { get; set; }

        public int parentDeckId { get; set; }

        Rectangle currentRect;
        Rectangle originalRect;
        Rectangle frameRect;

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
        /// 
        /// </summary>
        public Card()
        {
        }

        /// <summary>
        /// Copy Constructor.
        /// </summary>
        /// <param name="copy">Card to copy from</param>
        public Card(Card copy)
        {
            CardSuit = copy.CardSuit;
            cardId = copy.cardId;
            parentDeckId = copy.parentDeckId;
            currentRect = copy.currentRect;
            originalRect = copy.originalRect;
            frameRect = copy.frameRect;
            upperCard = copy.upperCard;
            lowerCard = copy.lowerCard;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public bool IsBlack()
        {
            return CardSuit == Suit.Clubs || CardSuit == Suit.Spades;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="rect"></param>
        /// <param name="frameRect"></param>
        /// <param name="suit"></param>
        /// <param name="cardId"></param>
        /// <param name="parentDeckId"></param>
        public Card(Rectangle rect, Rectangle frameRect, Suit suit, int cardId, int parentDeckId)
        {
            currentRect = rect;
            originalRect = rect;
            this.frameRect = frameRect;
            CardSuit = suit;
            this.cardId = cardId;
            this.parentDeckId = parentDeckId;
        }

        /// <summary>
        /// Gets the location of the card sprite in the texture.
        /// </summary>
        /// <returns></returns>
        public Rectangle DrawFrom()
        {
            return frameRect;
        }
    }
}