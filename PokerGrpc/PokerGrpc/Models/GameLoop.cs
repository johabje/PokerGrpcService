using System;
using System.Collections.Generic;

namespace PokerGrpc.Models
{
    public class GameLoop
    {
        const int numberOfCards = 52;
        private List<Card> cards { get; set; }

        public Stack<Card> cardStack { get; }

        public Random rand = new Random();



        public GameLoop()
        {
            /*
             * mby control the game from this
             */

        }

    }
}
