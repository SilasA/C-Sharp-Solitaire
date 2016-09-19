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
        /// 
        /// </summary>
        /// <param name="cardToMove"></param>
        /// <param name="cardMoveTo"></param>
        /// <returns></returns>
        public override bool IsValidMove(Card cardToMove, Card cardMoveTo)
        {
            if (cardToMove.IsBlack() == cardMoveTo.IsBlack()) return false;
            return true;
        }
    }
}
