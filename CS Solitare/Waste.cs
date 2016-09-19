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
        /// 
        /// </summary>
        /// <param name="id"></param>
        public Waste(int id) :
            base(id)
        {

        }

        public override bool IsValidMove(Card cardToMove, Card cardMoveTo)
        {
            // Check if the parent deck is the hand
            return cardToMove.parentDeckId == 1000;
        }
    }
}
