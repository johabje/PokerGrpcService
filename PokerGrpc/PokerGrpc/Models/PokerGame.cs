using System;
using System.Collections.Generic;
using System.Linq;

namespace PokerGrpc.Models
{
     public enum state
    {
        PreGame,
        PF,
        Floop,
        Turn,
        River,
        Showdown
    }

    public class PokerGame
    {
        public int gamePin;
        public Deck deck;

        //public List<Player> players = new  List<Player>();
        // using array instead for static size
        public Player[] players;
        public List<Player> playersPlaying;
        public state state = state.PreGame;
        public Player toAct;
        public List<Card> tableCards;
        public double pot;
        public double bet;
        //make blind final?
        public int blind;


        /*
         * ehhhh har hatt ekstremt tunnelsyn i natt og ser nå at det ble mye surr
         * 
         * holdem() og bettinground() er helt ubrukelig atm
         *      er vel bedre om vi bare godkjenner/avslår bets som blir sendt inn,
         *      og oppdaterer klientene ved godkjenning/timeout?
         *      
         *      typ     PlaceBet()
         *                  if ok or timeout:
         *                      set next better, min amount etc
         *                      update clients
         *                      
         * og btw handrankingen er ikke testa, men bør være quick fix om feil
         *          (var ikke sikker på reglene i enkelte tilfeller tho)
         *                  
         */

        

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
            this.pot = 0;
            this.bet = 0;
            roomOwner.isRoomOwner = true;
            roomOwner.firstToBet = true;
            this.toAct = roomOwner;
            AddPlayer(roomOwner);


            // NewGame();
            // ^testing purposes - (generate table cards for response)

        }

        /*
         * PokerGame.StartGame when owner clicks start or after a set time
         */
        public void UpdateState()
        {

            switch (state)
            {
                case state.PreGame:

                    //deal cards to players?
                    break;
                case state.PF:
                    if (RoundFinished())
                    {
                        this.state = state.Floop;
                        DealTableCards(3);
                    }
                    // some other game mode
                    break;
                case state.Floop:
                    if (RoundFinished())
                    {
                        this.state = state.Turn;
                        DealTableCards(1);
                    }
                    break;

                case state.Turn:
                    if (RoundFinished())
                    {
                        this.state = state.River;
                        DealTableCards(1);
                    }
                    break;

                case state.River:
                    if (RoundFinished())
                    {

                    }
                    break;
                case state.Showdown:
                    // some other game mode
                    break;

            }
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
                    // and the object wont be null
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

        public bool RoundFinished() {
            // REname or whatever
            // TODO remember to reset player.lastAction every round (pre flop, flop, etc)
            if(playersPlaying.Where(p => p.lastAction.Equals(null)).Any()) {
                // someone has not taken an action yet: round not finished. Continue to next player
                return false;
            } else {
                // check = 1;
                if (playersPlaying.Where(p => p.lastAction.Equals(1)).Count() >= playersPlaying.Count - 1) {
                // count of active players - 1 HAS CHECKED, round is over
                    return true;
                } else {
                    return false;
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
        public void NewRound()
        {
            // new list of players active this round
            playersPlaying = new List<Player>();

            //clears hand of all players and adds them to the playing list
            foreach (Player player in players) {
                if (player != null) {
                    player.Hand = new List<Card>();
                    playersPlaying.Add(player);
                }
            }

            MoveDealerButton();

            //clears table of cards
            this.tableCards.Clear();
            
            // deck.cardStack = stack of cards randomized
            this.deck = new Deck();
        }

        public void MoveDealerButton() {
            // moves dealer button basically
            Player lastFirstBetter = playersPlaying.Find(p => p.firstToBet);
            int indexLastFirstBetter = playersPlaying.IndexOf(lastFirstBetter);
            int indexNextFirstBetter = indexLastFirstBetter + 1;
            if (indexNextFirstBetter == playersPlaying.Count) {
                indexNextFirstBetter = 0;
            }
            playersPlaying[indexNextFirstBetter].firstToBet = true;
            lastFirstBetter.firstToBet = false;

            // just making sure, probably duplicate somewhere like in BettingRound or smthing
            foreach (Player player in playersPlaying) {
                player.currentRoundFirstToBet = false;
                player.currentBetter = false;
            }
            playersPlaying[indexNextFirstBetter].currentRoundFirstToBet = true;
            playersPlaying[indexNextFirstBetter].currentBetter = true;
        }

        public Boolean PlaceBet(Player player, float bet) {
            /*
             * 1. bet is from correct user
             *      table
             *      the users turn to act
             * mby just implement login?
             * 2. assert bet is within valid range 
             *      (potential_minBet? <= bet <= player.wallet)
             * 3. do some stuff if all-in
             * 
             * save total bet for each player (both for e.g. flop and for the whole round)
            */

            // for testing stream:
            int minBet = 0;
            this.bet += bet;

            if (minBet <= bet) {
                if (bet >= player.wallet)
                {
                    bet = (float)player.wallet;
                    player.wallet = 0;

                    //place the bet
                    return true;
                }
                player.wallet -= bet;

                return true;
            }


            // if ok
            //NextBetter(player);

            return false;
        }

        public void NextBetter(Player player) {
            int nextPlayerIndex = playersPlaying.IndexOf(player) + 1;
            if (nextPlayerIndex >= playersPlaying.Count) {
                nextPlayerIndex = 0;
            }
            //check if round over??
            playersPlaying[nextPlayerIndex].currentBetter = true;
            player.currentBetter = false;
        }

        public void Fold(Player player) {
            int nextPlayerIndex = playersPlaying.IndexOf(player) + 1;

            // purpose of currentRoundFirstToBet:
            // if folding player were first to bet, move this role to the next in line
            // = if player 2 is supposed to bet first pre flop but folds, 
            //              player3 will take the role as first better rest of the game
            //                  if player 3 folds, player 4 .....
            if (player.currentRoundFirstToBet) {
                if (nextPlayerIndex >= playersPlaying.Count) {
                    nextPlayerIndex = 0;
                }
                // moving firstBetter role
                playersPlaying[nextPlayerIndex].currentRoundFirstToBet = true;
                player.currentRoundFirstToBet = false;
            }

            NextBetter(player);

            // remove players active this round
            playersPlaying.Remove(player);
        }

        public void BettingRound() {
            foreach (Player player in playersPlaying) {
                // asserts only the correct player has currentBetter true
                if (player.currentRoundFirstToBet) {
                    player.currentBetter = true;
                } else {
                    player.currentBetter = false;
                }
            }

            int activePpl = playersPlaying.Count();
            for (int i=0;i<activePpl;i++) {
                int currentBetterIndex = playersPlaying.IndexOf(playersPlaying.Find(p => p.currentBetter));
                // get action from player playersPlaying[currentBetterIndex]

                // in event of a RAISE:
                // set i = 0 and recalculate activePpl

                // if player folds, 

            }

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

        public override bool Equals(Object obj)
        {
            PokerGame pokerObj = obj as PokerGame;
            bool samePlayers = true;

            for (int i = 0; i < players.Length; i++)
            {
                if (players.ElementAt(i) != null && pokerObj.players.ElementAt(i) != null)
                {
                    if (!players.ElementAt(i).Equals(pokerObj.players.ElementAt(i)))
                    {
                        samePlayers = false;
                    }
                }
            }
            if (pokerObj == null)
                return false;
            else
                return bet.Equals(pokerObj.bet) &&
                    pot.Equals(pokerObj.pot) &&
                    blind.Equals(pokerObj.blind) &&
                    toAct.Equals(pokerObj.toAct) &&
                    samePlayers &&
                    tableCards.Equals(pokerObj.tableCards);
        }

        public PokerGame GetPokerGame(Guid playerId)
        {
            return null;
        }


    }
}
