using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

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
        private List<Card> selectedCards;

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
            selectedCards = new List<Card>();
            location = new Rectangle();
        }

        /// <summary>
        /// Copy Constructor.
        /// </summary>
        /// <param name="deck">Deck to copy</param>
        public Deck(Deck copy)
        {
            cardPadding = copy.cardPadding;
            cardList = new List<int>(copy.cardList);
            selectedCards = new List<Card>(copy.selectedCards);
            type = copy.type;
            Id = copy.Id;
            location = copy.location;
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
            MouseState state = Mouse.GetState();
            int idx = 0;
            foreach (int c in cardList)
            {
                CardData cd = DeckSystem.carddatum[DeckSystem.cards[c].dataIndex];
                if (DeckSystem.cards[c].Selected)
                {
                    DeckSystem.cards[c].CurrentLocation = new Vector2(state.X - Card.CARDSIZE_X / 2, state.Y - Card.CARDSIZE_Y / 2);
                }
                else
                {
                    DeckSystem.cards[c].OriginalLocation = new Vector2(location.X, location.Y + cardPadding * idx);
                }
                idx++;
            }
        }

        /// <summary>
        /// Called to draw textures to the screen. SpriteBatch.Begin() must be called prior to this.
        /// </summary>
        /// <param name="spriteBatch">Used to draw textures to the screen</param>
        /// <param name="texture">Texture to draw from</param>
        public override void Draw(SpriteBatch spriteBatch, Texture2D texture)
        {
            selectedCards.Clear();
            foreach (int c in cardList)
            {
                if (!DeckSystem.cards[c].Selected)
                    spriteBatch.Draw(
                        texture,
                        DeckSystem.cards[c].CurrentLocation,
                        null,
                        DeckSystem.carddatum[DeckSystem.cards[c].dataIndex].Invisible ? Card.CARDBACK_BLUE : DeckSystem.cards[c].frameRect,
                        Vector2.Zero,
                        0f,
                        null,
                        Color.White, 
                        SpriteEffects.None,
                        0f);
                else
                {
                    selectedCards.Add(DeckSystem.cards[c]);
                }
            }
        }

        /// <summary>
        /// Called to draw the selected texture to the screen.
        /// </summary>
        /// <param name="spriteBatch">Used to draw textures to the screen</param>
        /// <param name="texture">Texture to draw from</param>
        public override void DrawSelected(SpriteBatch spriteBatch, Texture2D texture)
        {
            foreach (Card card in selectedCards)
            {
                spriteBatch.Draw(
                    texture,
                    card.CurrentLocation,
                    null,
                    DeckSystem.carddatum[card.dataIndex].Invisible ? Card.CARDBACK_BLUE : card.frameRect,
                    Vector2.Zero,
                    0f,
                    null,
                    Color.White,
                    SpriteEffects.None,
                    1f);
            }
        }

        /// <summary>
        /// Uncovers the top card in the deck in deriving classes.
        /// </summary>
        public virtual void UncoverTop()
        {
            if (cardList.Count > 0)
                DeckSystem.carddatum[cardList[Top()]].visibility = CardData.Visibility.Uncovered;
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
        public void AddCard(int card, bool visible = true)
        {
            DeckSystem.cards[card].OriginalLocation = CalculateNewCardPosition();
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

            DeckSystem.carddatum[cardList[Top()]].visibility = visible ? CardData.Visibility.Uncovered : CardData.Visibility.Invisible;
        }

        /// <summary>
        /// Removes a card from the deck.
        /// </summary>
        /// <param name="card">Card to remove</param>
        /// <returns>The id of the card removed</returns>
        public int RemoveCard(int card)
        {
            int idx = cardList.IndexOf(card);
            if (idx - 1 >= 0)
            {
                if (DeckSystem.carddatum[cardList[idx]].parentCard != -1)
                    DeckSystem.carddatum[DeckSystem.carddatum[cardList[idx]].parentCard].childCard = 
                        DeckSystem.carddatum[cardList[idx]].childCard;
                if (idx <= Top())
                {
                    if (DeckSystem.carddatum[cardList[idx]].childCard != -1)
                        DeckSystem.carddatum[DeckSystem.carddatum[cardList[idx]].childCard].parentCard =
                            DeckSystem.carddatum[cardList[idx]].parentCard;
                }
                else
                    DeckSystem.carddatum[DeckSystem.carddatum[cardList[idx]].childCard].parentCard = -1;
                if (DeckSystem.carddatum[cardList[idx]].childCard != -1)
                    DeckSystem.carddatum[DeckSystem.carddatum[cardList[idx]].childCard].parentCard = -1;
                DeckSystem.carddatum[cardList[idx]].parentDeckId = 0;
            }
            cardList.Remove(card);
            return card;
        }
    }
}
