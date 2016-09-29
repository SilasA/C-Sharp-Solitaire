using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace CS_Solitare
{
    /// <summary>
    /// 
    /// </summary>
    class Deck : Drawable
    {
        // List of card indices in master list
        public List<int> cardList { get; private set; }

        public static int padding = 0;

        public enum DeckType
        {
            Hand,
            Waste,
            Tableau,
            Foundation
        }
        protected DeckType type;

        // IDs: Tableau = *0
        //      Foundation = *00
        //      Hand = 1000
        //      Waste = 2000
        public int Id { get; set; }   

        /// <summary>
        /// Initializes objects.
        /// </summary>
        public Deck()
        {
            cardList = new List<int>();
            location = new Rectangle();
        }

        /// <summary>
        /// Construct Deck with an ID then call the default constructor.
        /// </summary>
        /// <param name="id">Assigned ID</param>
        public Deck(int id) : this()
        {
            Id = id;
            if (Id >= 10 && Id <= 90) type = DeckType.Tableau;
            else if (Id >= 100 && Id <= 900) type = DeckType.Foundation;
            else if (Id == 1000) type = DeckType.Hand;
            else if (Id == 2000) type = DeckType.Waste;
        }

        /// <summary>
        /// Return the location of a card if it were to be added to the top of the deck.
        /// </summary>
        /// <returns>Default deck location</returns>
        public virtual Vector2 CalculateNewCardPosition()
        {
            return new Vector2(location.X, location.Y);
        }

        /// <summary>
        /// Checks if both cards are uncovered.
        /// </summary>
        /// <param name="cardToMove">The card in motion</param>
        /// <param name="cardMoveTo">The target card</param>
        /// <returns></returns>
        public virtual bool IsValidMove(CardData cardToMove, CardData cardMoveTo)
        {
            return !cardToMove.Covered && !cardMoveTo.Covered;
        }

        /// <summary>
        /// Called for logical operations.
        /// </summary>
        /// <param name="gameTime">Time state of the game</param>
        public override void Update(GameTime gameTime)
        {

        }

        /// <summary>
        /// Called to draw textures to the screen.
        /// </summary>
        /// <param name="spriteBatch">Used to draw textures to the screen</param>
        /// <param name="texture">Texture to draw from</param>
        public override void Draw(SpriteBatch spriteBatch, Texture2D texture)
        {

        }
    }
}
