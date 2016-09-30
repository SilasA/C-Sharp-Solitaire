using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace CS_Solitare
{
    /// <summary>
    /// 
    /// </summary>
    class Card
    {
        // Dimensions
        public const int CARDSIZE_X = 140;
        public const int CARDSIZE_Y = 190;

        public Vector2 currentLocation { get; private set; }
        public Vector2 originalLocation { get; private set; }
        public Rectangle frameRect { get; private set; }

        public int dataIndex { get; private set; }

        /// <summary>
        /// Full constructor for all data.
        /// </summary>
        /// <param name="loc">Location of the card on-screen</param>
        /// <param name="frameRect">Location of the card sprite in texture</param>
        /// <param name="dataIndex">Index location of data card</param>
        public Card(Vector2 loc, Rectangle frameRect, int dataIndex)
        {
            currentLocation = loc;
            originalLocation = loc;
            this.frameRect = frameRect;
            this.dataIndex = dataIndex;
        }

        /// <summary>
        /// Returns the card to where it was before selection.
        /// </summary>
        public void ReturnToOrigin()
        {
            currentLocation = originalLocation;
        }

        /// <summary>
        /// Gets the location of the card sprite in the texture.
        /// </summary>
        /// <returns></returns>
        public Rectangle DrawFrom()
        {
            return frameRect;
        }

        /// <summary>
        /// Checks if the provided Coordinates are within the cards bounds and the card is selectable.
        /// </summary>
        /// <param name="pos">Coordinates</param>
        /// <returns>If within card</returns>
        public bool Contains(Vector2 pos)
        {
            CardData cd = DeckSystem.cards[dataIndex];
            if (cd.Covered && !cd.Invisible)
                return pos.X >= currentLocation.X && pos.X <= currentLocation.X + Deck.padding &&
                    pos.Y >= currentLocation.Y && pos.Y <= currentLocation.Y + Deck.padding;
            else if (!cd.Covered && !cd.Invisible)
                return pos.X >= currentLocation.X && pos.X <= currentLocation.X + CARDSIZE_X &&
                    pos.Y >= currentLocation.Y && pos.Y <= currentLocation.Y + CARDSIZE_Y;
            else return false;
        }
    }
}