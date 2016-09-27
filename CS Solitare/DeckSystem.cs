using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

// TODO: Deck system contains card data of parent decks, suits, ids, etc...
// Card is separate and will control draw location, etc...

namespace CS_Solitare
{
    /// <summary>
    /// Class that manages all of the decks of cards.
    /// </summary>
    class DeckSystem : Drawable
    {
        // Default full deck
        public static List<CardData> completeDeck;

        Tableau[] tableau;
        Foundation[] foundation;

        Hand hand;
        Waste waste;

        /// <summary>
        /// Initializes the decks with new IDs and calls Populate().
        /// </summary>
        public DeckSystem()
        {
            // Initialize decks
            tableau = new Tableau[7];
            for (int i = 0; i < tableau.Count(); i++)
            {
                tableau[i] = new Tableau((i + 1) * 10);
            }
            foundation = new Foundation[4];
            for (int i = 0; i < foundation.Count(); i++)
                foundation[i] = new Foundation(i * 100);
            hand = new Hand(1000);
            waste = new Waste(2000);

            completeDeck = new List<CardData>();

            Populate();
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
                FindDeckById(cardMoveTo.ParentDeckId).IsValidMove(cardToMove, cardMoveTo);
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
            // Call update for all decks
            foreach (Deck deck in tableau)
                deck.Update(gameTime);
            foreach (Deck deck in foundation)
                deck.Update(gameTime);
            hand.Update(gameTime);
            waste.Update(gameTime);

            MouseState state = Mouse.GetState();

            for (int i = 0; i < tableau.Count(); i++)
            {
                foreach (Card card in tableau[i].cardList)
                {
                    if (IsLeftClicked(state) && card.Contains(new Vector2(state.X, state.Y)))
                    {

                    }
                }
            }

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
        /// Populates the lists of cards with completeDeck.
        /// </summary>
        private void Populate()
        {
            // Populate list
            for (int s = 0; s < 4; s++)
                for (int i = 1; i <= 13; i++)
                    completeDeck.Add(new CardData((CardData.Suit)s, i));

            // Shuffle
            Shuffle(completeDeck, out completeDeck);

            // Disperse amongst decks
            int idx = 0;
            for (int i = 0; i < tableau.Count(); i++)
            {
                tableau[i].AppendList(completeDeck.GetRange(idx, i + 1));
                idx = i + 1;
            }
            hand.AppendList(completeDeck.GetRange(idx, completeDeck.Count - idx));
        }

        /// <summary>
        /// Shuffles a list of cards.
        /// </summary>
        /// <param name="list">List to shuffle</param>
        /// <param name="oList">List to recieve shuffled list</param>
        private void Shuffle(List<CardData> list, out List<CardData> oList)
        {
            Random ran = new Random();
            int n = list.Count;
            while (n > 1)
            {
                n--;
                int k = ran.Next(n + 1);
                CardData value = list[k];
                list[k] = list[n];
                list[n] = value;
            }
            oList = new List<CardData>(list);
        }

        /// <summary>
        /// Moves the top card of the hand to the waste.
        /// </summary>
        private void MoveOneToWaste()
        {
            waste.AppendCard(hand.cardList[hand.cardList.Count - 1]);
            waste.cardList[waste.cardList.Count - 1].Covered = false;
        }

        /// <summary>
        /// Transfers all waste cards back to the hand.
        /// </summary>
        private void MoveAllToHand()
        {
            hand.AppendList(hand.cardList);
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
