using System;
namespace PokerGrpc.Models
{
        
        public class Card
        {
            public char suit { get; private set; }
            public int rank { get; private set; }

            public Card(char cardSuit, int cardValue)
            {
                suit = cardSuit;
                rank = cardValue;
            }
        }

    }