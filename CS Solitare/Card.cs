using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace CS_Solitare
{
    /// <summary>
    /// 
    /// </summary>
    class Card : CardData
    {
        public bool Covered { get; set; }
        public bool Invisible { get; set; }
        public bool Selected { get; set; }
        public int ParentDeckId { get; set; }

        Vector2 currentLocation;
        Vector2 originalLocation;
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
        /// Copy Constructor.
        /// </summary>
        /// <param name="copy">Card to copy from</param>
        public Card(Card copy) :
            base(copy)
        {
            ParentDeckId = copy.ParentDeckId;
            currentLocation = copy.currentLocation;
            originalLocation = copy.originalLocation;
            frameRect = copy.frameRect;
            upperCard = copy.upperCard;
            lowerCard = copy.lowerCard;
        }

        /// <summary>
        /// Copy constructor purely for data.
        /// </summary>
        /// <param name="copy">Copy of data</param>
        public Card(CardData copy) :
            base(copy)
        {
        }

        /// <summary>
        /// Full constructor for all data.
        /// </summary>
        /// <param name="loc">Location of the card on-screen</param>
        /// <param name="frameRect">Location of the card sprite in texture</param>
        /// <param name="parentDeckId">ID number of parent deck</param>
        /// <param name="suit">Suit of the card</param>
        /// <param name="cardId">Card value 1-13</param>
        public Card(Vector2 loc, Rectangle frameRect, int parentDeckId, Suit suit, int cardId) :
            base(suit, cardId)
        {
            currentLocation = loc;
            originalLocation = loc;
            this.frameRect = frameRect;
            ParentDeckId = parentDeckId;
        }

        /// <summary>
        /// Initializer intended for use after Constructing the card with Card(CardData copy).
        /// </summary>
        /// <param name="loc">Location of the card on-screen</param>
        /// <param name="frameRect">Location of card sprite in texture</param>
        /// <param name="parentDeckId">ID number of parent deck</param>
        public void Initialize(Vector2 loc, Rectangle frameRect, int parentDeckId)
        {
            originalLocation = loc;
            currentLocation = originalLocation;
            this.frameRect = frameRect;
            ParentDeckId = parentDeckId;
            Covered = true;
        }

        /// <summary>
        /// Returns the Vector2 coordinates in a Rectangle.
        /// </summary>
        /// <returns></returns>
        public Rectangle GetRectLocation()
        {
            return new Rectangle((int)currentLocation.X, (int)currentLocation.Y, CARDSIZE_X, CARDSIZE_Y);
        }

        /// <summary>
        /// Returns the card to where it was before selection.
        /// </summary>
        public void ReturnToOrigin()
        {
            currentLocation = originalLocation;
        }

        /// <summary>
        /// Changes this card's ownership to a new deck.
        /// This is called in the parent deck's append card.
        /// </summary>
        /// <param name="pDeck">Deck to move to</param>
        public void MoveTo(Deck pDeck)
        {
            originalLocation = pDeck.CalculateNewCardPosition();
            currentLocation = originalLocation;
            ParentDeckId = pDeck.Id;
            FindFrameRect();
        }

        /// <summary>
        /// Gets the location of the card sprite in the texture.
        /// </summary>
        /// <returns></returns>
        public Rectangle DrawFrom()
        {
            return frameRect;
        }

        /// <summary>
        /// Checks if the provided Coordinates are within the cards bounds and the card is selectable.
        /// </summary>
        /// <param name="pos">Coordinates</param>
        /// <returns>If within card</returns>
        public bool Contains(Vector2 pos)
        {
            if (Covered && !Invisible)
                return pos.X >= currentLocation.X && pos.X <= currentLocation.X + Deck.padding &&
                    pos.Y >= currentLocation.Y && pos.Y <= currentLocation.Y + Deck.padding;
            else if (!Covered && !Invisible)
                return pos.X >= currentLocation.X && pos.X <= currentLocation.X + CARDSIZE_X &&
                    pos.Y >= currentLocation.Y && pos.Y <= currentLocation.Y + CARDSIZE_Y;
            else return false;
        }

        /// <summary>
        /// Finds the frame location in the texture using the CardId and CardSuit.
        /// </summary>
        private void FindFrameRect()
        {
            frameRect = new Rectangle(CARDSIZE_X * CardId - 1, CARDSIZE_Y * (int)CardSuit, CARDSIZE_X, CARDSIZE_Y);
        }
    }
}