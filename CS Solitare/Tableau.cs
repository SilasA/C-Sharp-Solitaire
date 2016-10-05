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
        public static int padding; 

        /// <summary>
        /// Default Constructor.
        /// </summary>
        /// <param name="id">ID number</param>
        /// <param name="startLimit">Max. amount of cards at initialization of deck</param>
        public Tableau(int id) :
            base(id, new Rectangle(Card.CARDSIZE_X * (id / 10 - 1) + PADDING * (id / 10), Card.CARDSIZE_Y + PADDING * 2, Card.CARDSIZE_X, Card.CARDSIZE_Y), 20)
        {
            padding = cardPadding;
        }

        /// <summary>
        /// Uncovers the top card of the deck.
        /// </summary>
        public override void UncoverTop()
        {
            base.UncoverTop();
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
            cardPadding = cardList.Count > 10 ? 15 : 20;
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
