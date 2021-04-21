using System;
using System.Collections.Generic;
using System.Linq;

namespace PokerGrpc.Models
{
    public class PokerGame
    {
        public int gamePin;
        public Deck deck;

        //public List<Player> players = new  List<Player>();
        // using array instead for static size
        public Player[] players;

        public Player toAct;
        public List<Card> tableCards;
        public double pot;
        public double bet;
        //make blind final?
        public int blind;
        

        // new table
        public PokerGame(Player roomOwner, int blind, int gamePin, int maxPlayers = 6)
        {
            players = new Player[maxPlayers];
            for (int i =0;i<players.Length;i++) {
                players[i] = null;
            }

            //maybe get gamePin from a singleton?
            //Random rnd = new Random();
            //this.gamePin = rnd.Next(9999);
            this.gamePin = gamePin;

            this.blind = blind;
            this.tableCards = new List<Card>();

            roomOwner.isRoomOwner = true;
            AddPlayer(roomOwner);

            // NewGame();
            // ^testing purposes - (generate table cards for response)

        }

        // deal int cardCount amount of cards
        public void DealTableCards(int cardCount = 1) {
            for (int i = 0; i < cardCount; i++) {
                this.tableCards.Add(deck.DealCardSingle());
            }
        }

        // deal same amount of cards to all players
        public void DealPlayerCards(int cardCount = 1) {
            for (int i = 0; i < cardCount; i++) {
                foreach (Player player in players) {
                    //TODO maybe give players a boolean for if-active 
                    // in current round or not; some may sit out a round
                    // and consequently the object wont be null
                    if (player != null) {
                        player.Hand.Add(deck.DealCardSingle());
                    }
                }
            }
        }

        //currently unused (not needed in holdem)
        public void DealSinglePlayer(Player player, int cardCount = 1) {
            foreach (Player player2 in players) {
                if (player == player2) {
                    player.Hand.Add(deck.DealCardSingle());
                    break;
                }
            }
        }

        // inserting player into first available spot
        public void AddPlayer(Player player) {
            for (int i = 0; i < players.Length; i++) {
                if (players[i] == null) {
                    players[i] = player;
                    break;
                }
            }
        }

        public Player GameOver()
        {
            //determine vinner, return the vinner, distriubute the pot

            // dispose stuff?

            if (this.deck != null) {
                this.deck = null;
            }

            return null;

        }

        // new game/round/whatever on a table
        public void NewGame()
        {
            //clears hand of all players
            foreach (Player player in players) {
                if (player != null) {
                    player.Hand = new List<Card>();
                }
            }

            //clears table of cards
            this.tableCards.Clear();
            // deck.cardStack = stack of cards randomized
            this.deck = new Deck();

            //select blinds


            /* TODO - delete, has been removed to gameloop
            //pop 3 cards into this.tableCards from deck.cardStack
            DealTableCards(3);
            */

            /*
             * 1. rules for min-bet and min-raise?
             *
            */

        }

        public Boolean PlaceBet(Player player, float bet) {
            /*
             * 1. bet is from correct user
             *      table
             *      the users turn to act
             * mby just implement login?
             * 2. assert if bet is within a valid range 
             *      (potential_minBet? <= bet <= player.wallet)
             * 3. do some stuff if all-in
            */


            int minBet = 0;
            if (bet > player.wallet || bet < minBet) {
                return false;
            }


            bool ok = true;
            if (ok) {
                return true;
            } else {
                return false;
            }
        }

        public void BettingRound() {

            //save total bet for each player
            //end: move bets to pot
        }

        // string of table cards separated by ','
        public String GetTableCards() {
            if (tableCards == null || tableCards.Count < 1) {
                return "";
            } else {
                List<String> tableList = new List<String>();
                foreach (Card card in tableCards) {
                    tableList.Add(card.rank.ToString() + card.suit.ToString());
                }
                return String.Join(",", tableList);
            }
            
        }

        public void RemovePlayer(Player player) {

        }


    }
}
