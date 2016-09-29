using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace CS_Solitare
{
    /// <summary>
    /// Deck derivative for the tableau.
    /// </summary>
    class Tableau : Deck
    {
        /// <summary>
        /// Default Constructor.
        /// </summary>
        /// <param name="id">ID number</param>
        public Tableau(int id) :
            base(id)
        {
        }

        /// <summary>
        /// Uncovers the top card of the deck.
        /// </summary>
        public void UncoverTop()
        {
            // Make top card.visibility = Visibility.Uncovered
        }

        /// <summary>
        /// Return the location of a card if it were to be added to the top of the deck.
        /// </summary>
        /// <returns></returns>
        public override Vector2 CalculateNewCardPosition()
        {
            return new Vector2(location.X, location.Y + cardList.Count * padding);
        }

        /// <summary>
        /// Checks if the two cards are the same color, the correct value, and both uncovered.
        /// </summary>
        /// <param name="cardToMove">The card or parent card to move</param>
        /// <param name="cardMoveTo">The target card</param>
        /// <returns></returns>
        public override bool IsValidMove(CardData cardToMove, CardData cardMoveTo)
        {
            return
                cardToMove.IsBlack() != cardMoveTo.IsBlack() &&
                cardMoveTo.CardId + 1 == cardMoveTo.CardId &&
                base.IsValidMove(cardToMove, cardMoveTo);
        }

        /// <summary>
        /// Updates padding size then calls base.
        /// </summary>
        /// <param name="gameTime">Time state of the game</param>
        public override void Update(GameTime gameTime)
        {
            // Adjust padding with card count
            padding = cardList.Count > 10 ? 15 : 20; // Temp values
            base.Update(gameTime);
        }

        /// <summary>
        /// Called to draw textures to the screen.
        /// </summary>
        /// <param name="spriteBatch">Used to draw textures to the screen</param>
        /// <param name="texture">Texture to draw from</param>
        public override void Draw(SpriteBatch spriteBatch, Texture2D texture)
        {
            base.Draw(spriteBatch, texture);
        }
    }
}
