using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;

namespace CS_Solitare
{
    /// <summary>
    /// The derivative deck for the hand.
    /// </summary>
    class Hand : Deck
    {
        /// <summary>
        /// Default Constructor.
        /// </summary>
        /// <param name="id">ID number</param>
        public Hand(int id) : 
            base(id, new Rectangle(PADDING, PADDING, Card.CARDSIZE_X, Card.CARDSIZE_Y), 0)
        {
        }

        /// <summary>
        /// Checks if there are no more cards to transfer to the waste.
        /// </summary>
        /// <returns></returns>
        public bool IsEmpty()
        {
            return cardList.Count == 0;
        }

        /// <summary>
        /// Moves to the hand from anywhere are invalid moves.
        /// </summary>
        /// <param name="cardToMove">The card or parent card to move</param>
        /// <param name="cardMoveTo">The target card</param>
        /// <returns>false</returns>
        public override bool IsValidMove(CardData cardToMove, CardData cardMoveTo)
        {
            return false;
        }
    }
}
