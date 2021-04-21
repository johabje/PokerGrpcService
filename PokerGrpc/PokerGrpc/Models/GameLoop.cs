using System;
using System.Collections.Generic;

namespace PokerGrpc.Models
{
    public class GameLoop
    {

        public PokerGame pokerTable;



        public GameLoop(Player player, int gamePin, int blind = 5, int maxPlayers = 6, int gameMode = 0)
        {
            // PokerGame(Player roomOwner, int blind, int gamePin, int maxPlayers = 6)
            this.pokerTable = new PokerGame(player, blind, gamePin, maxPlayers);

            switch (gameMode) {
                /* 
                 * 0 = default = holdem, assign an int to another game mode
                 */
                case 1:
                    // some other game mode
                    break;
                default:
                    HoldEm();
                    break;
            }

            

            

        }

        private void HoldEm() {
            // when tableowner send a request to start the game (or all seats taken):
            pokerTable.NewGame();

            bool playing = true;
            while (playing) {
                pokerTable.DealPlayerCards(2);
                // betting round

                //deal 3 table cards
                pokerTable.DealTableCards(3);

                // betting round
                //deal 1 table card
                pokerTable.DealTableCards();

                // betting round
                //deal final table card
                pokerTable.DealTableCards();

                // final betting round
                pokerTable.GameOver();

                //TODO PokerGame.cs: bettingRound and placeBet
                //      Player.cs: check what values are needed
                //          (how to id each player?)
            }

        }



    }
}
