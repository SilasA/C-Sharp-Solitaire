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
    /// Class that manages all of the decks of cards.
    /// </summary>
    class DeckSystem : Drawable
    {
        public static List<CardData> carddatum;
        public static List<Card> cards;

        Tableau[] tableau;
        Foundation[] foundation;
        Hand hand;
        Waste waste;

        /// <summary>
        /// Initializes the decks with new IDs.
        /// </summary>
        public DeckSystem()
        {
            tableau = new Tableau[7];
            foundation = new Foundation[4];
            for (int i = 0; i < tableau.Count(); i++)
                tableau[i] = new Tableau((i + 1) * 10, i + 1);
            for (int i = 0; i < foundation.Count(); i++)
                foundation[i] = new Foundation(i * 100);
            hand = new Hand(1000);
            waste = new Waste(2000);

            carddatum = new List<CardData>();
            cards = new List<Card>();

            Random rn = new Random();
            int currentDeckId = 00;
            int idx = 0;
            for (int s = 0; s < 4; s++)
            {
                for (int i = 0; i < 13; i++)
                {
                    currentDeckId = idx >= tableau.Count() ? 1000 : rn.Next(1, tableau.Count() + 1) * 10;
                    carddatum.Add(new CardData((CardData.Suit)s, i, currentDeckId));
                    if (tableau[idx >= tableau.Count() ? 0 : idx].IsAtStartLimit) idx++;
                    FindDeckById(currentDeckId).cardList.Add(cards[cards.Count - 1].dataIndex);

                }
            }
        }

        /// <summary>
        /// Checks if a card move is valid through the target deck.
        /// </summary>
        /// <param name="cardToMove">The card or parent card to move</param>
        /// <param name="cardMoveTo">The target card</param>
        /// <returns></returns>
        public bool IsValidMove(CardData cardToMove, CardData cardMoveTo)
        {
            return 
                FindDeckById(cardMoveTo.parentDeckId).IsValidMove(cardToMove, cardMoveTo);
        }

        /// <summary>
        /// Returns the deck with the given Id if there is one.
        /// </summary>
        /// <param name="id">Id number</param>
        /// <returns></returns>
        public Deck FindDeckById(int id)
        {
            foreach (Deck deck in tableau)
                if (deck.Id == id) return deck;
            foreach (Deck deck in foundation)
                if (deck.Id == id) return deck;
            if (hand.Id == id) return hand;
            if (waste.Id == id) return waste;
            return null;
        }

        /// <summary>
        /// Called for logical operations.
        /// </summary>
        /// <param name="gameTime">Time state of the game</param>
        public override void Update(GameTime gameTime)
        {
            MouseState state = Mouse.GetState();

            base.Update(gameTime);
        }

        /// <summary>
        /// Called to draw textures to the screen.
        /// </summary>
        /// <param name="spriteBatch">Used to draw textures to the screen</param>
        /// <param name="texture">Texture to draw from</param>
        public override void Draw(SpriteBatch spriteBatch, Texture2D texture)
        {
            foreach (Deck deck in tableau)
                deck.Draw(spriteBatch, texture);
            foreach (Deck deck in foundation)
                deck.Draw(spriteBatch, texture);
            hand.Draw(spriteBatch, texture);
            waste.Draw(spriteBatch, texture);
        }

        /// <summary>
        /// Moves the top card of the hand to the waste.
        /// </summary>
        private void MoveOneToWaste()
        {
        }

        /// <summary>
        /// Transfers all waste cards back to the hand.
        /// </summary>
        private void MoveAllToHand()
        {
        }

        /// <summary>
        /// Checks if LeftButton is pressed but was not before.
        /// </summary>
        /// <param name="state">State of the mouse</param>
        /// <returns></returns>
        private bool IsLeftClicked(MouseState state)
        {
            return state.LeftButton == ButtonState.Pressed &&
                Game1.oldState.LeftButton == ButtonState.Released;
        }
    }
}
