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
    /// Class that manages all of the card decks.
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
            // Init Decks with Id
            tableau = new Tableau[7];
            foundation = new Foundation[4];
            for (int i = 0; i < tableau.Count(); i++)
                tableau[i] = new Tableau((i + 1) * 10);
            for (int i = 0; i < foundation.Count(); i++)
                foundation[i] = new Foundation((i + 1) * 100);
            hand = new Hand(1000);
            waste = new Waste(2000);

            // Init lists
            carddatum = new List<CardData>();
            cards = new List<Card>();
            List<int> tempCardList = new List<int>();
            for (int i = 0; i < 52; i++)
                tempCardList.Add(i);
            tempCardList = Shuffle(tempCardList);

            // Populate lists
            for (int s = 0; s < 4; s++)
            {
                for (int i = 0; i < 13; i++)
                {
                    carddatum.Add(new CardData((CardData.Suit)s, i, carddatum.Count));
                    cards.Add(
                        new Card(Vector2.Zero, 
                        new Rectangle(i * Card.CARDSIZE_X, s * Card.CARDSIZE_Y, Card.CARDSIZE_X, Card.CARDSIZE_Y),
                        carddatum.Count - 1));
                }
            }

            // Populate decks
            int idx = 0;
            for (int i = 0; i < 7; i++)
            {
                for (int j = 0; j < i + 1; j++)
                {
                    tableau[i].AddCard(tempCardList[idx], false);
                    idx++;
                }
                tableau[i].UncoverTop();
            }
            for (int i = idx; i < 52; i++)
                hand.AddCard(tempCardList[i], false);

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
        /// Adds a card data index to a deck's cardlist.
        /// Hopefully this is just temporary.
        /// </summary>
        /// <param name="id">Id of the deck</param>
        /// <param name="idx">Index to add</param>
        public void AddCardIdToDeck(int id, int idx)
        {
            for (int i = 0; i < tableau.Count(); i++)
                if (tableau[i].Id == id) tableau[i].AddCard(idx);
            for (int i = 0; i < foundation.Count(); i++)
                if (foundation[i].Id == id) foundation[i].AddCard(idx);
            if (hand.Id == id) hand.AddCard(idx);
            if (waste.Id == id) waste.AddCard(idx);
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

            if (IsLeftClicked(state))
            {
                // Hand/Waste
                if (hand.Contains(new Vector2(state.X, state.Y)))
                {
                    if (hand.IsEmpty())
                      MoveAllToHand();
                    else MoveOneToWaste();
                }

                for (int i = 0; i < cards.Count; i++)
                {
                    if (cards[i].Contains(new Vector2(state.X, state.Y)) &&
                        carddatum[i].parentDeckId != 1000)
                    {
                        cards[i].Selected = true;
                        break;
                    }
                }

            }
            else if (IsLeftReleased(state))
            {
                for (int i = 0; i < cards.Count; i++)
                {
                    if (cards[i].Contains(new Vector2(state.X, state.Y)) &&
                        carddatum[i].parentDeckId != 1000 &&
                        cards[i].Selected)
                    {
                        cards[i].Selected = false;
                        foreach (Card card in cards)
                        {
                            if (card.Contains(new Vector2(state.X, state.Y)))
                            {
                                int targetId = carddatum[card.dataIndex].parentDeckId;
                                // If target is in the tableau
                                if (targetId >= 10 && targetId <= 90)
                                {
                                    int sourceId = carddatum[cards[i].dataIndex].parentDeckId;
                                    if (sourceId == targetId) break;

                                    // Source is in the tableau
                                    if (sourceId >= 10 && targetId <= 90)
                                    {
                                        tableau[(targetId / 10) - 1].AddCard(tableau[(sourceId / 10) - 1].RemoveCard(card.dataIndex));
                                    }
                                    // Source is in the foundation
                                    else if (sourceId >= 100 && sourceId <= 900)
                                    {
                                        tableau[(targetId / 100) - 1].AddCard(foundation[(sourceId / 100) - 1].RemoveCard(card.dataIndex));
                                    }
                                    // Source is the waste
                                    else if (sourceId == 2000)
                                    {
                                        tableau[(targetId / 1000) - 1].AddCard(waste.RemoveCard(card.dataIndex));
                                    }
                                    break;
                                }
                                // If target is in the foundation
                                else if (targetId >= 100 && targetId <= 900)
                                {
                                    int sourceId = carddatum[cards[i].dataIndex].parentDeckId;
                                    if (sourceId == targetId) break;
                                    if (sourceId >= 10 && targetId <= 90)
                                    {
                                        foundation[(targetId / 10) - 1].AddCard(tableau[(sourceId / 10) - 1].RemoveCard(card.dataIndex));
                                    }
                                    else if (sourceId >= 100 && sourceId <= 900)
                                    {
                                        foundation[(targetId / 100) - 1].AddCard(foundation[(sourceId / 100) - 1].RemoveCard(card.dataIndex));
                                    }
                                    else if (sourceId == 2000)
                                    {
                                        foundation[(targetId / 1000) - 1].AddCard(waste.RemoveCard(card.dataIndex));
                                    }
                                    break;
                                }
                            }
                            else cards[i].ReturnToOrigin();

                        }
                    }
                }
            }

            foreach (Deck deck in tableau)
                deck.Update(gameTime);
            foreach (Deck deck in foundation)
                deck.Update(gameTime);
            hand.Update(gameTime);
            waste.Update(gameTime);

            base.Update(gameTime);
        }

        /// <summary>
        /// Called to draw textures to the screen.
        /// </summary>
        /// <param name="spriteBatch">Used to draw textures to the screen</param>
        /// <param name="texture">Texture to draw from</param>
        public override void Draw(SpriteBatch spriteBatch, Texture2D texture)
        {
            spriteBatch.Begin();
            foreach (Deck deck in tableau)
                deck.Draw(spriteBatch, texture);
            foreach (Deck deck in foundation)
                deck.Draw(spriteBatch, texture);
            hand.Draw(spriteBatch, texture);
            waste.Draw(spriteBatch, texture);
            spriteBatch.End();
        }

        /// <summary>
        /// Called to draw the selected texture to the screen.
        /// </summary>
        /// <param name="spriteBatch">Used to draw textures to the screen</param>
        /// <param name="texture">Texture to draw from</param>
        public override void DrawSelected(SpriteBatch spriteBatch, Texture2D texture)
        {
            spriteBatch.Begin();
            foreach (Deck deck in tableau)
                deck.DrawSelected(spriteBatch, texture);
            foreach (Deck deck in foundation)
                deck.DrawSelected(spriteBatch, texture);
            waste.DrawSelected(spriteBatch, texture);
            spriteBatch.End();
        }

        /// <summary>
        /// Moves the top card of the hand to the waste.
        /// </summary>
        private void MoveOneToWaste()
        {
            waste.AddCard(hand.RemoveCard(hand.cardList[hand.Top()]));
            waste.UncoverTop();
        }

        /// <summary>
        /// Transfers all waste cards back to the hand.
        /// </summary>
        private void MoveAllToHand()
        {
            for (int i = 0; i < waste.cardList.Count;)
                hand.AddCard(waste.RemoveCard(waste.cardList[waste.Top()]));
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

        /// <summary>
        /// Checks is LeftButton is released after it was pressed.
        /// </summary>
        /// <param name="state">State of the mouse</param>
        /// <returns></returns>
        private bool IsLeftReleased(MouseState state)
        {
            return state.LeftButton == ButtonState.Released &&
                Game1.oldState.LeftButton == ButtonState.Pressed;
        }

        /// <summary>
        /// Shuffles a list.
        /// </summary>
        /// <param name="list">List to shuffle</param>
        /// <returns></returns>
        private List<int> Shuffle(List<int> list)
        {
            Random rng = new Random();
            int n = list.Count;
            while (n > 1)
            {
                n--;
                int k = rng.Next(n + 1);
                int value = list[k];
                list[k] = list[n];
                list[n] = value;
            }
            return new List<int>(list);
        }
    }
}
