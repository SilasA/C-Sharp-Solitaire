using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace CS_Solitare
{
    class Deck : Drawable
    {
        public List<Card> cardList { get; set; }

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
            cardList = new List<Card>();
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
        /// Initializes a card.
        /// </summary>
        /// <param name="card">Card to initialize</param>
        public void Initialize(ref Card card)
        {
            card.Initialize(new Vector2(location.X, location.Y + card.UpperCard.GetRectLocation().Y + padding), card.DrawFrom(), Id);
        }

        /// <summary>
        /// Adds a card to the end of the card list.
        /// </summary>
        /// <param name="card">Card to add</param>
        /// <param name="inList">If adding a batch of cards</param>
        public void AppendCard(Card card, bool inList = false)
        {
            card.MoveTo(this);
            cardList.Add(new Card(card));
            Initialize(ref card);
            if (inList) UpdateCardPointers();
        }

        /// <summary>
        /// Adds a list of cards to the end of the card list.
        /// </summary>
        /// <param name="collection">Collection to add</param>
        public void AppendList(IEnumerable<CardData> collection)
        {
            foreach (CardData card in collection)
            {
                AppendCard(new Card(card), true);
            }
            UpdateCardPointers();
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
        public virtual bool IsValidMove(Card cardToMove, Card cardMoveTo)
        {
            return !cardToMove.Covered && !cardMoveTo.Covered;
        }

        /// <summary>
        /// Called for logical operations.
        /// </summary>
        /// <param name="gameTime">Time state of the game</param>
        public override void Update(GameTime gameTime)
        {
            for (int i = 0; i < cardList.Count; i++)
                if (cardList[i].ParentDeckId != Id)
                    cardList.RemoveAt(i);
        }

        /// <summary>
        /// Called to draw textures to the screen.
        /// </summary>
        /// <param name="spriteBatch">Used to draw textures to the screen</param>
        /// <param name="texture">Texture to draw from</param>
        public override void Draw(SpriteBatch spriteBatch, Texture2D texture)
        {
            foreach (Card card in cardList)
                spriteBatch.Draw(texture, card.GetRectLocation(), card.DrawFrom(), Color.White);
        }

        /// <summary>
        /// Sets the card's pointers to its current parent and child cards.
        /// </summary>
        private void UpdateCardPointers()
        {
            for (int i = 0; i < cardList.Count; i++)
            {
                // If in-range, set the upper and lower card references
                cardList[i].UpperCard = i > 0 ? cardList[i - 1] : null;
                cardList[i].LowerCard = i < cardList.Count - 1 ? cardList[i + 1] : null;
            }
        }
    }
}
