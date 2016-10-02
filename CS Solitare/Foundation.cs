using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;

namespace CS_Solitare
{
    /// <summary>
    /// 
    /// </summary>
    class Foundation : Deck
    {
        /// <summary>
        /// Construct deck from ID
        /// </summary>
        /// <param name="id">ID number</param>
        public Foundation(int id) : 
            base(id, new Rectangle(
                Game1.WINDOW_WIDTH - (Card.CARDSIZE_X * (id / 10 - 1)) + (PADDING * (id / 10)),
                PADDING, Card.CARDSIZE_X, Card.CARDSIZE_Y), 0)
        {
        }

        /// <summary>
        /// Checks if cards are both uncovered, the same suit, and correct descending value.
        /// </summary>
        /// <param name="cardToMove">The card or parent card to move</param>
        /// <param name="cardMoveTo">The target card</param>
        /// <returns></returns>
        public override bool IsValidMove(CardData cardToMove, CardData cardMoveTo)
        {
            return
                !base.IsValidMove(cardToMove, cardMoveTo) &&
                cardToMove.CardSuit == cardMoveTo.CardSuit &&
                cardToMove.CardId == cardMoveTo.CardId + 1;
        }
    }
}
