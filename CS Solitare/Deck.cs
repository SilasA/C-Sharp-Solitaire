using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace CS_Solitare
{
    class Deck
    {
        protected List<Card> cardList;

        public enum DeckType
        {
            Hand,
            Waste,
            Tableau,
            Foundation
        }
        protected DeckType type;

        protected int id;

        protected Rectangle location;

        /// <summary>
        /// 
        /// </summary>
        public Deck()
        {
            cardList = new List<Card>();
            location = new Rectangle();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        public Deck(int id)
        {
            this.id = id;
        }

        /// <summary>
        /// 
        /// </summary>
        public void AppendCard(Card card)
        {
            cardList.Add(card);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="gameTime"></param>
        public virtual void Update(GameTime gameTime)
        {

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="spriteBatch"></param>
        public virtual void Draw(SpriteBatch spriteBatch, Texture2D texture)
        {

        }
    }
}
