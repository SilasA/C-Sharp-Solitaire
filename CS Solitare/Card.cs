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
    /// Contains drawing data for cards.
    /// </summary>
    class Card
    {
        // Dimensions
        public const int CARDSIZE_X = 140;
        public const int CARDSIZE_Y = 190;
        // Card backs
        public readonly static Rectangle CARDBACK_BLUE = new Rectangle(CARDSIZE_X * 13, CARDSIZE_Y * 0, CARDSIZE_X, CARDSIZE_Y);
        public readonly static Rectangle CARDBACK_GREEN = new Rectangle(CARDSIZE_X * 13, CARDSIZE_Y * 1, CARDSIZE_X, CARDSIZE_Y);
        public readonly static Rectangle CARDBACK_RED = new Rectangle(CARDSIZE_X * 13, CARDSIZE_Y * 2, CARDSIZE_X, CARDSIZE_Y);
        public readonly static Rectangle CARDBACK_TRANS = new Rectangle(CARDSIZE_X * 13, CARDSIZE_Y * 3, CARDSIZE_X, CARDSIZE_Y);

        public Vector2 currentLocation { get; set; }

        private Vector2 originalLoc;
        public Vector2 originalLocation
        {
            get { return originalLoc; }
            set
            {
                originalLoc = value;
                currentLocation = value;
            }
        }

        public Rectangle frameRect { get; private set; }

        public int dataIndex { get; private set; }

        public bool padded { get; set; }
        public bool selected { get; set; }

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
        /// Checks if the provided Coordinates are within the cards bounds and the card is selectable.
        /// </summary>
        /// <param name="pos">Coordinates</param>
        /// <returns>If within card</returns>
        public bool Contains(Vector2 pos)
        {
            CardData cd = DeckSystem.carddatum[dataIndex];
            if (cd.Covered && !cd.Invisible)
                return pos.X >= currentLocation.X && pos.X <= currentLocation.X + (padded ? Tableau.padding : 0) &&
                    pos.Y >= currentLocation.Y && pos.Y <= currentLocation.Y + (padded ? Tableau.padding : 0);
            else if (!cd.Covered && !cd.Invisible)
                return pos.X >= currentLocation.X && pos.X <= currentLocation.X + CARDSIZE_X &&
                    pos.Y >= currentLocation.Y && pos.Y <= currentLocation.Y + CARDSIZE_Y;
            else return false;
        }
    }
}