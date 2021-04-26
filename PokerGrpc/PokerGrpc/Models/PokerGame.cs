using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

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
        public Player winner = null; 

        //public List<Player> players = new  List<Player>();
        // using array instead for static size
        public Player[] players;
        public List<Player> playersPlaying;
        public state state;
        public Player toAct;
        public List<Card> tableCards;
        public float pot;
        public float bet;
        //make blind final?
        public int blind;
        public bool startNewRound = false;


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
            this.tableCards = new List<Card>();
            roomOwner.isRoomOwner = true;
            roomOwner.firstToBet = true;
            roomOwner.currentRoundFirstToBet = true;
            roomOwner.currentBetter = true;
            this.toAct = roomOwner;
            AddPlayer(roomOwner);
            
            this.state = state.PreGame;
        }

        /*
         * PokerGame.StartGame when owner clicks start or after a set time
         */
        public async Task UpdateStateAsync()
        {
            /*
            would be nice to just have all functionality related to whos turn it is in the end of this
            */

            switch (state)
            {
                case state.PreGame:
                    // deal 2 cards to all players
                    
                    if (startNewRound) {
                        this.state = state.PF;
                        DealPlayerCards(2);
                        StartBettingRound();
                        startNewRound = false;
                    }
                    break;
                case state.PF:
                    if (RoundFinished())
                    {
                        this.state = state.Floop;
                        DealTableCards(3);
                        StartBettingRound();
                    }
                    // some other game mode
                    break;
                case state.Floop:
                    if (RoundFinished())
                    {
                        this.state = state.Turn;
                        DealTableCards(1);
                        StartBettingRound();
                    }
                    break;

                case state.Turn:
                    if (RoundFinished())
                    {
                        this.state = state.River;
                        DealTableCards(1);
                        StartBettingRound();
                    }
                    break;

                case state.River:
                    if (RoundFinished())
                    {
                        // TODO end the game somewhere
                        GameOver();
                    }
                    break;
                case state.Showdown:
                    await Task.Delay(2000000);
                    NewRound();
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
        public Boolean AddPlayer(Player player) {
            for (int i = 0; i < players.Length; i++) {
                if (players[i] == null) {
                    players[i] = player;
                    return true;
                }
            }
            return false;

        }

        public bool RoundFinished() {
            if(playersPlaying.Where(p => p.lastAction.Equals(-1)).Any()) {
                // someone has not taken an action yet: round not finished. Continue to next player
                return false;
            } else {
                if (playersPlaying.Where(p => p.bet.Equals(bet)).Count() == playersPlaying.Count())
                {
                    return true;
                }
                else {
                    return false;
                }
            }
        }


        public void GameOver()
        {
            
            //determine vinner, return the vinner, distriubute the pot
            
            if (playersPlaying.Count() == 1)
            {
                playersPlaying.ElementAt(0).wallet += pot;
                winner = playersPlaying.ElementAt(0);
            }
            else
            {
                foreach (Player player in playersPlaying) {
                var handScore = HandRanking.GetBestHand(player.Hand, tableCards);
                player.bestCombo = handScore.Item1.ToString();
                }
                int bestScore = playersPlaying.Min(c => Int32.Parse(c.bestCombo));
                int equalScore = playersPlaying.Where(c => c.bestCombo.Equals(bestScore.ToString())).Count();
                if (equalScore > 1)
                {
                    float prizePerPlayer = pot / equalScore;
                    foreach (Player player in playersPlaying)
                    {
                        Console.WriteLine("more than one winner, best score is" + player.bestCombo + "this player had " + GetCards(player.Hand) + "table cards were "+GetCards(this.tableCards));
                        winner = player; //change to list of players?
                        player.wallet += prizePerPlayer;
                    }
                }
                else
                {
                    winner = playersPlaying.Find(c => c.bestCombo.Equals(bestScore.ToString()));
                    winner.wallet += pot;
                    Console.WriteLine("the winner is: " + winner.name + " with hand " + GetCards(winner.Hand) + " best combo: " + winner.bestCombo + " table cardes: " + GetCards(this.tableCards));


                }
            }
            this.state = state.Showdown;
            UpdateStateAsync();


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
                    player.bet = 0;
                    player.lastAction = -1;
                }
            }
            this.state = state.PreGame;
            this.bet = 0;
            this.pot = 0;

            MoveDealerButton();

            //clears table of cards
            this.tableCards.Clear();
            
            // deck.cardStack = stack of cards randomized
            this.deck = new Deck();
            startNewRound = true;
            UpdateStateAsync();
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

        public bool Check(Player player) {
            if (player.bet >= this.bet && player.currentBetter) {
                NextBetter(player);
                return true;
            } else {
                return false;
            }
        }

        public bool Call(Player player) {
            // TODO implement ALL-IN logic
            float newBet = this.bet - player.bet;
            if (player.wallet >= newBet) {
                player.wallet -= newBet;
                player.bet += newBet;
                pot += newBet;
                NextBetter(player);
                return true;
            } else {
                return false;
            }
        }

        public bool PlaceBet(Player player, float raise) {
            if (player.wallet < raise || player.bet + raise < this.bet) {
                return false;
            } else {
                //TODO implement ALL-IN logic
                player.wallet -= raise;
                player.bet += raise;

                pot += raise;

                //updates new max bet
                this.bet = player.bet;

                NextBetter(player);
                return true;
            }
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
            //Set player hand to null?

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
            if (playersPlaying.Count == 1) {
                // all but one has folded:
                // -> GamerOver() = distribute pot, send to players, wait, start new game
                GameOver();
            } else {
                UpdateStateAsync();
            }
            
        }

        public void StartBettingRound() {

            this.bet = 0;

            foreach (Player player in playersPlaying) {
                // asserts only the correct player has currentBetter true
                if (player.currentRoundFirstToBet) {
                    player.currentBetter = true;
                } else {
                    player.currentBetter = false;
                }
                player.lastAction = -1;
                player.bet = 0;
            }
            if (this.state == state.PF)
            {
                foreach (Player player in playersPlaying)
                {
                    if (player.currentBetter)
                    {
                        if(!PlaceBet(player, this.blind))
                        {
                            Fold(player);
                        }

                        Console.WriteLine("Smallblind: " + player.name);
                        break;
                    }
                    
                }
           
                foreach(Player player in playersPlaying)
                {
                    if (player.currentBetter)
                    {
                        if (!PlaceBet(player, this.blind))
                        {
                            Fold(player);
                        }
                        Console.WriteLine("Bigblind: " + player.name);
                        break;
                    }
                    
                    
                }
            }


            //TODO end: move bets to pot
        }

        // string of table cards separated by ','
        public String GetCards(List<Card> cards) {
            if (cards == null || cards.Count < 1) {
                return "";
            } else {
                List<String> cardList = new List<String>();
                foreach (Card card in cards) {
                    cardList.Add(card.rank.ToString() + card.suit.ToString());
                }
                return String.Join(",", cardList);
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
