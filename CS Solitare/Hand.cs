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

        public override bool IsValidMove(Card cardToMove, Card cardMoveTo)
        {
            return false;
        }
    }
}
