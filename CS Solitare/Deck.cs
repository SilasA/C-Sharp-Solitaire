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
    /// Container for cards.
    /// </summary>
    class Deck : Drawable
    {
        public int cardPadding;
        public const int PADDING = 20;

        // List of card indices in master list
        public List<int> cardList { get; private set; }

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
        /// <param name="location">Base location of the deck on-screen</param>
        /// <param name="padding">Card padding for the deck</param>
        public Deck(int id, Rectangle location, int padding) : this()
        {
            Id = id;
            if (Id >= 10 && Id <= 90) type = DeckType.Tableau;
            else if (Id >= 100 && Id <= 900) type = DeckType.Foundation;
            else if (Id == 1000) type = DeckType.Hand;
            else if (Id == 2000) type = DeckType.Waste;

            this.location = location;
            cardPadding = padding;
        }

        /// <summary>
        /// Return the location of a card if it were to be added to the top of the deck.
        /// </summary>
        /// <returns>Default deck location</returns>
        public Vector2 CalculateNewCardPosition()
        {
            return new Vector2(location.X, location.Y + cardList.Count * cardPadding);
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
            int idx = 0;
            foreach (int c in cardList)
            {
                CardData cd = DeckSystem.carddatum[DeckSystem.cards[c].dataIndex];
                if (DeckSystem.cards[c].selected)
                {

                }
                else
                {
                    DeckSystem.cards[c].originalLocation = new Vector2(location.X, location.Y + cardPadding * idx);
                }
                idx++;
            }
        }

        /// <summary>
        /// Called to draw textures to the screen.
        /// </summary>
        /// <param name="spriteBatch">Used to draw textures to the screen</param>
        /// <param name="texture">Texture to draw from</param>
        public override void Draw(SpriteBatch spriteBatch, Texture2D texture)
        {
            spriteBatch.Begin();
            foreach (int c in cardList)
            {
                spriteBatch.Draw(
                    texture,
                    DeckSystem.cards[c].currentLocation,
                    DeckSystem.carddatum[DeckSystem.cards[c].dataIndex].Invisible ? Card.CARDBACK_BLUE : DeckSystem.cards[c].frameRect,
                    Color.White);
            }
            spriteBatch.End();
        }

        /// <summary>
        /// Uncovers the top card in the deck in deriving classes.
        /// </summary>
        public virtual void UncoverTop()
        {
        }

        /// <summary>
        /// Returns the top card's index.
        /// </summary>
        /// <returns></returns>
        public int Top()
        {
            return cardList.Count - 1;
        }

        /// <summary>
        /// Adds a card to the deck.
        ///     - Sets position
        ///     - Sets deck Id
        ///     - Sets if it's padded
        ///     - Sets the current card's parent and child cards
        /// </summary>
        /// <param name="card">Card representation to add</param>
        public void AddCard(int card)
        {
            DeckSystem.cards[card].originalLocation = CalculateNewCardPosition();
            cardList.Add(card);
            DeckSystem.carddatum[cardList[Top()]].parentDeckId = Id;
            DeckSystem.cards[cardList[Top()]].padded = cardPadding != 0;

            // Set parent and child card if any
            if (Top() - 1 > 0)
            {
                DeckSystem.carddatum[card].parentCard = DeckSystem.cards[cardList[Top() - 1]].dataIndex;
                DeckSystem.carddatum[card].childCard = -1;
                DeckSystem.carddatum[cardList[Top() - 1]].childCard = card;
            }
            else
            {
                DeckSystem.carddatum[card].parentCard = -1;
                DeckSystem.carddatum[card].childCard = -1;
            }

        } 

        /// <summary>
        /// 
        /// </summary>
        /// <param name="card"></param>
        public void RemoveCard(int card)
        {
            
        }
    }
}
