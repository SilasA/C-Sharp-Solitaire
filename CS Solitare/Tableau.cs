using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CS_Solitare
{
    class Tableau : Deck
    {

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        public Tableau(int id) :
            base(id)
        {

        }

        /// <summary>
        /// Checks if the two cards are the same color, the correct value, and both uncovered.
        /// </summary>
        /// <param name="cardToMove">The card or parent card to move</param>
        /// <param name="cardMoveTo">The target card</param>
        /// <returns></returns>
        public override bool IsValidMove(Card cardToMove, Card cardMoveTo)
        {
            return
                cardToMove.IsBlack() != cardMoveTo.IsBlack() &&
                cardMoveTo.cardId + 1 == cardMoveTo.cardId &&
                base.IsValidMove(cardToMove, cardMoveTo);
        }
    }
}
