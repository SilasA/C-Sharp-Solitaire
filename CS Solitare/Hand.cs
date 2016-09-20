using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CS_Solitare
{
    class Hand : Deck
    {

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        public Hand(int id) : 
            base(id)
        {

        }

        /// <summary>
        /// Moves to the hand from anywhere are invalid moves.
        /// </summary>
        /// <param name="cardToMove">The card or parent card to move</param>
        /// <param name="cardMoveTo">The target card</param>
        /// <returns>false</returns>
        public override bool IsValidMove(Card cardToMove, Card cardMoveTo)
        {
            return false;
        }
    }
}
