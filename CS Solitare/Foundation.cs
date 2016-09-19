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
        /// 
        /// </summary>
        /// <param name="cardToMove"></param>
        /// <param name="cardMoveTo"></param>
        /// <returns></returns>
        public override bool IsValidMove(Card cardToMove, Card cardMoveTo)
        {
            if (!base.IsValidMove(cardToMove, cardMoveTo)) return false;
            return true;
        }
    }
}
