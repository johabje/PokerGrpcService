using System;
using System.Collections.Generic;

namespace PokerGrpc.Models
{
    public class PokerGame
    {
        private Deck deck;
        private List<Player> players;
        private Player toAct;
        private List<Card> tableCards;
        private double pot;
        private double bet;
        private int blind;
        

        public PokerGame(List<Player> players, int blind)
        {
            this.deck = new Deck();
            deck.GenerateDeck();
            this.tableCards = new List<Card>();
            this.players = players;
            this.blind = blind;
        }

        public void dealFlop()
        {
            List<Card> flop = deck.DealFlop();
            this.tableCards.Add(flop[0]);
            this.tableCards.Add(flop[1]);
            this.tableCards.Add(flop[2]);
        }

        public void dealTurn()
        {
            this.tableCards.Add(deck.DealTurn());
        }

        public void dealRiver()
        {
            this.tableCards.Add(deck.DealTurn());
        }

        public Player GameOver()
        {
            //determine vinner, return the vinner, distriubute the pot
            return null;
        }

        public void NewGame()
        {
            this.deck = new Deck();
            this.deck.GenerateDeck();
            this.bet = 0;
            this.pot = 0;
            this.tableCards = new List<Card>();
        }
        
    }
}
