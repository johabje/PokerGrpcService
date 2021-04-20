using System;
using System.Collections.Generic;

namespace PokerGrpc.Models
{
    public class Deck
    {
        const int numberOfCards = 52;
        private List<Card> cards { get; set; }

        public Stack<Card> cardStack { get; }

        public Random rand = new Random();



        public Deck()
        {
            //mby have a singleton for a complete deck instead of generating?
            // ^if yes, use a copy of the singleton list when randomizing stack
            this.cards = new List<Card>(GenerateDeck());
            this.cardStack =  new Stack<Card>(GenerateRandomizedStack(cards));

        }

        public List<Card> GenerateDeck() {
            List<Card> completeDeck = new List<Card>();
            char[] suits = { 'H', 'K', 'S', 'R' };
            foreach (char suit in suits) {
                for (int i = 2; i < 15; i++) {
                    // remember to convert numbers to ace, king, etc
                    Card card = new Card(suit, i);
                    completeDeck.Add(card);
                }
            }
            return completeDeck;
        }


        public Stack<Card> GenerateRandomizedStack(List<Card> cards) {
            int cardsLeft = cards.Count;
            Stack<Card> stack = new Stack<Card>();
            while (cardsLeft > 0) {
                int nextCardIndex = rand.Next(cardsLeft);
                stack.Push(cards[nextCardIndex]);
                cards.RemoveAt(nextCardIndex);
                cardsLeft = cards.Count;
            }
            return stack;
        }


        public Card DealCardSingle() {
            Card card = cardStack.Pop();
            return card;
        }

        /* might not need
        public List<Card> DealCardMultiple(int cardCount) {
            List<Card> cardsToDeal = new List<Card>();
            for (int i = 0; i < cardCount; i++) {
                cardsToDeal.Add(cardStack.Pop());
            }
            return cardsToDeal;
        }
        */

    }
}
