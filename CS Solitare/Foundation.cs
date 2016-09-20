using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CS_Solitare
{
    class Foundation : Deck
    {

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        public Foundation(int id) : 
            base(id)
        {

        }

        /// <summary>
        /// Checks if cards are both uncovered, the same suit, and correct descending value.
        /// </summary>
        /// <param name="cardToMove">The card or parent card to move</param>
        /// <param name="cardMoveTo">The target card</param>
        /// <returns></returns>
        public override bool IsValidMove(Card cardToMove, Card cardMoveTo)
        {
            return
                !base.IsValidMove(cardToMove, cardMoveTo) &&
                cardToMove.CardSuit == cardMoveTo.CardSuit &&
                cardToMove.cardId == cardMoveTo.cardId + 1;
        }
    }
}
