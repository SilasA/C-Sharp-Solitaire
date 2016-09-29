using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CS_Solitare
{
    class Waste : Deck
    {
        /// <summary>
        /// Default Constructor
        /// </summary>
        /// <param name="id">ID number</param>
        public Waste(int id) :
            base(id)
        {
        }

        /// <summary>
        /// Checks if the parent deck of the moving card is the hand.
        /// </summary>
        /// <param name="cardToMove">The card or parent card to move</param>
        /// <param name="cardMoveTo">The target card</param>
        /// <returns></returns>
        public override bool IsValidMove(CardData cardToMove, CardData cardMoveTo)
        {
            return cardToMove.parentDeckId == 1000;
        }
    }
}
