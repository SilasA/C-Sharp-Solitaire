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
    /// Class that manages all of the decks of cards.
    /// </summary>
    class DeckSystem : Drawable
    {
        // Default full deck
        static List<CardData> completeDeck;

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
                tableau[i] = new Tableau(i * 10);
            }
            foundation = new Foundation[4];
            for (int i = 0; i < foundation.Count(); i++)
                foundation[i] = new Foundation(i * 100);
            hand = new Hand(1000);
            waste = new Waste(2000);

            Populate();
        }

        /// <summary>
        /// Checks if a card move is valid through the target deck.
        /// </summary>
        /// <param name="cardToMove">The card or parent card to move</param>
        /// <param name="cardMoveTo">The target card</param>
        /// <returns></returns>
        public bool IsValidMove(Card cardToMove, Card cardMoveTo)
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
            for (int i = 0; i < tableau.Count(); i++)
            {
                int idx = completeDeck.Count - i - 1;
                int count = completeDeck.Count - (completeDeck.Count - idx);
                tableau[i].AppendList(completeDeck.GetRange(idx, count));
            }
            hand.AppendList(completeDeck.GetRange(0, completeDeck.Count - 28));
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
