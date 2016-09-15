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
        const int CARDSIZE_X = 140;
        const int CARDSIZE_Y = 190;

        public enum Suit
        {
            Clubs,
            Hearts,
            Diamonds,
            Spades
        }

        Suit cardSuit;
        public Suit CardSuit
        {
            get;
            set;
        }
        int cardId;

        int parentDeckId;

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
            cardSuit = suit;
            this.cardId = cardId;
            this.parentDeckId = parentDeckId;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public Rectangle DrawFrom()
        {
            return frameRect;
        }
    }
}
