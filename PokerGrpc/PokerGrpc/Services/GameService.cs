using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Grpc.Core;
using Microsoft.Extensions.Logging;
using PokerGrpc.Models;
using PokerGrpc.Singelton;

namespace PokerGrpc.Services
{
    public class GameService : Game.GameBase
    {
        private readonly ILogger<GameService> _logger;
        public GameService(ILogger<GameService> logger)
        {
            _logger = logger;
        }

        public override Task<GameLobby> CreateNewGame(NewGameRequest request, ServerCallContext context)
        {
            Player player = new Player
            {
                name = request.Gplayer.Name,
                isRoomOwner = false,
                Hand = null,
                bestCombo = null,
                lastAction = -1,
                wallet = request.Gplayer.Wallet
            };
            PokerGame lobby = new PokerGame(player, request.SmallBlind, request.GamePin, request.MaxBuyin, request.MinBuyin, 6);
            //Console.WriteLine(request);
            /* logging for debug
            Console.WriteLine(lobby.gamePin);
            Player xd = lobby.players[0];
            Console.WriteLine(lobby.minBuyin);
            Console.WriteLine(lobby.maxBuyin);
            Console.WriteLine(lobby.blind);
            Console.WriteLine(xd.name);
            Console.WriteLine(xd.wallet);
            */
            // lobby.players.ElementAt(0).currentBetter = true;

            GPlayer gPlayer = new GPlayer
            {
                Name = player.name,
                Wallet = player.wallet,
                IsRoomOwner = player.isRoomOwner,
                Hand = "0",
                BestCombo = "x",
                Action = -1,
                Bet = 0
            };
            GameLobby gameLobby = new GameLobby
            {
                GamePin = lobby.gamePin,
                ToAct = gPlayer.Name,
                TableCards = "0",
                Pot = 0,
                Bet = 0,
                Blind = request.SmallBlind,
                MaxBuyin = request.MaxBuyin,
                MinBuyin = request.MinBuyin

            };
            gameLobby.Gplayers.Add(gPlayer);

            foreach (PokerGame game in StorageSingleton.Instance.currentGames)
            {
                if (game.gamePin == lobby.gamePin)
                {
                    return Task.FromResult(new GameLobby { });
                }
            }
            StorageSingleton.Instance.currentGames.Add(lobby);
            Console.WriteLine(StorageSingleton.Instance.currentGames);
            return Task.FromResult(gameLobby);

        }

        /*
         * 
         * 
         * 
        TODO IMPORTANT
        require unique names for new ppl
        + make sure there is enough room
        -> change PokerGame.AddPlayer to return a bool mby
        */

        public override Task<GameLobby> JoinGame(JoinGameRequest request, ServerCallContext context)
        {
            PokerGame lobby;
            Player player = null;
            if (StorageSingleton.Instance.currentGames.Find(game => game.gamePin.Equals(request.GamePin)) == null)
            {
                Console.WriteLine("Lobby not found.");
                return Task.FromResult(new GameLobby { });
            }

            lobby = StorageSingleton.Instance.currentGames.Find(game => game.gamePin.Equals(request.GamePin));
            foreach (Player lobbyPlayer in lobby.players)
            {
                if (lobbyPlayer != null && lobbyPlayer.name.Equals(request.Gplayer.Name, StringComparison.OrdinalIgnoreCase))
                {
                    player = lobbyPlayer;
                    break;
                }
            }
            if (player == null)
            {
                Console.WriteLine("Player ", request.Gplayer.Name, " not already in table.");
                player = new Player
                {
                    name = request.Gplayer.Name,
                    wallet = request.Gplayer.Wallet,
                    isRoomOwner = false,
                    Hand = null,
                    bestCombo = null,
                    lastAction = -1
                };

                if (!lobby.AddPlayer(player))
                {
                    Console.WriteLine("Table ", request.GamePin, " full and ", request.Gplayer.Name, " not already playing.");
                    return Task.FromResult(new GameLobby { });
                }
                Console.WriteLine("New player: ", player.name, " added to table ", lobby.gamePin, ".");
            }

            GameLobby gameLobby = new GameLobby
            {
                GamePin = request.GamePin,
                ToAct = lobby.toAct.name, // this will always return the room owner, but the client should receive an updated version in next message
                TableCards = lobby.GetCards(lobby.tableCards),
                Pot = lobby.pot,
                Bet = lobby.bet,
                Blind = lobby.blind
            };
            foreach (Player participant in lobby.players)
            {
                if (participant != null)
                {
                    GPlayer gParticipant = new GPlayer
                    {
                        Action = participant.lastAction,
                        BestCombo = "0",
                        Hand = "0",
                        IsRoomOwner = false,
                        Name = participant.name,
                        Wallet = participant.wallet,
                        Bet = participant.bet
                    };
                    gameLobby.Gplayers.Add(gParticipant);
                }
            }
            return Task.FromResult(gameLobby);
        }

        public override async Task StartStream(JoinGameRequest request, IServerStreamWriter<GameLobby> responseStream, ServerCallContext context)
        {
            PokerGame pokerGame;
            if (StorageSingleton.Instance.currentGames.Find(game => game.gamePin.Equals(request.GamePin)) == null) return;

            pokerGame = StorageSingleton.Instance.currentGames.Find(game => game.gamePin.Equals(request.GamePin));

            //if the player is not first in the players list, this fucks up :/
            /*foreach (Player player in pokerGame.players) {
                if (player != null)
                {
                    Console.WriteLine(player.name + "requesting player " + request.Gplayer.Name);
                }
                if (player != null && player.name.Equals(request.Gplayer.Name)) {
                    break;
                }
                Console.WriteLine("Should not go here");
                await Task.FromResult(await JoinGame(request, context));
            }
            */
            Console.WriteLine("pokergamePin" + pokerGame.gamePin);
            Player lastBetter;
            if (pokerGame.playersPlaying != null && pokerGame.playersPlaying.Where(p => p.currentBetter).Any())
            {
                lastBetter = pokerGame.playersPlaying.Find(p => p.currentBetter);
            }
            else
            {
                lastBetter = pokerGame.players.ElementAt(0);
            }

            int lastTableCardsCount = pokerGame.tableCards.Count();
            int lastPlayersCount = pokerGame.players.Where(p => p == null).Count();


            Player currentBetter;
            int tableCardsCount;
            int playersCount;
            state currentState;

            //TODO bug with the line below (awai responsestream...)
            // just wrote something random
            await responseStream.WriteAsync(PokerGameToGameLobby(pokerGame, request.Gplayer.Name));

            while (true)
            {
                Console.WriteLine(pokerGame.state);
                Console.WriteLine("To act=", pokerGame.toAct);
                if (pokerGame.playersPlaying != null && pokerGame.playersPlaying.Where(p => p.currentBetter).Any())
                {
                    currentBetter = pokerGame.playersPlaying.Find(p => p.currentBetter);
                }
                else
                {
                    currentBetter = pokerGame.players.ElementAt(0);
                }

                tableCardsCount = pokerGame.tableCards.Count();
                currentState = pokerGame.state;
                playersCount = pokerGame.players.Where(p => p == null).Count();
                //Console.WriteLine(currentState);
                if (!(lastBetter.name).Equals(currentBetter.name, StringComparison.OrdinalIgnoreCase) || !lastTableCardsCount.Equals(tableCardsCount) || !playersCount.Equals(lastPlayersCount))
                {
                    await responseStream.WriteAsync(PokerGameToGameLobby(pokerGame, request.Gplayer.Name));
                    lastBetter = currentBetter;
                    lastTableCardsCount = tableCardsCount;
                    lastPlayersCount = playersCount;
                }
                else if (currentState == state.Showdown)
                {
                    Console.WriteLine("No change in game state");
                    int j = 0;
                    for (int i = 0; i < pokerGame.players.Length; i++)
                    {
                        if (pokerGame.players[i] != null)
                        {
                            j++;
                        }
                    }
                    Console.WriteLine(j + " players at table with pin: " + pokerGame.gamePin + ".");


                    // return the game with winner etc
                    GameLobby lobby = new GameLobby
                    {
                        Pot = pokerGame.pot,
                        TableCards = pokerGame.GetCards(pokerGame.tableCards),
                        Winner = pokerGame.winner.name,
                    };

                    //add code to reset the game here.

                }
                else
                {
                    // Console.WriteLine("No change in game state");
                }

                await Task.Delay(1000); //gotta look bussy
                //Console.WriteLine("The bets are: " + pokerGame.bet+ " and " + pokergame2.bet);
            }
        }

        public override Task<ActionResponse> Action(ActionRequest request, ServerCallContext context)
        {
            ActionResponse badActionResponse = new ActionResponse { Success = false };
            PokerGame lobby;
            if (StorageSingleton.Instance.currentGames.Find(game => game.gamePin.Equals(request.GamePin)) == null)
            {
                return Task.FromResult(badActionResponse);
            }
            else
            {
                lobby = StorageSingleton.Instance.currentGames.Find(game => game.gamePin.Equals(request.GamePin));
            }
            if (lobby.state == state.PreGame)
            {
                return Task.FromResult(badActionResponse);
            }
            /*
            foreach (Player player2 in lobby.players) {
                if (player2 != null) {
                    Console.WriteLine(player2.name + player2.lastAction);
                    Console.WriteLine("firsBetter?" + player2.firstToBet.ToString() + player2.currentRoundFirstToBet.ToString() + player2.currentBetter.ToString());
                }
                
            }
            foreach (Player player2 in lobby.playersPlaying) {
                if (player2 != null) {
                    Console.WriteLine(player2.name + player2.lastAction);
                    Console.WriteLine("firsBetter?" + player2.firstToBet.ToString() + player2.currentRoundFirstToBet.ToString() + player2.currentBetter.ToString());
                }
                
            }
             */

            Player player = null;
            foreach (Player lobbyPlayer in lobby.players)
            {
                if (lobbyPlayer != null && lobbyPlayer.name.Equals(request.Name, StringComparison.OrdinalIgnoreCase))
                {
                    player = lobbyPlayer;
                    break;
                }
            }

            if (player == null)
            {
                Console.WriteLine("cant find player:'", request.Name, "' in lobby.");
                return Task.FromResult(badActionResponse);
            }

            if (!player.currentBetter)
            {
                //player not found or not the players turn to act -> return false
                Console.WriteLine("player.currentBetter=", player.currentBetter);
                return Task.FromResult(badActionResponse);
            }
            int actionId = request.Action;
            switch (actionId)
            {
                case 0:
                    // action: fold
                    lobby.Fold(player);
                    break;
                case 1:
                    // action: check;
                    if (!lobby.Check(player))
                    {
                        Console.WriteLine("Player='", request.Name, "' tried to check but not accepted");
                        return Task.FromResult(badActionResponse);
                    }
                    Console.WriteLine("Player='", request.Name, "' check Accepted");
                    break;
                case 2:
                    // action: bet (== raise)
                    if (!lobby.PlaceBet(player, request.Bet))
                    {
                        Console.WriteLine("Player='", request.Name, "' tried to bet but not accepted");
                        return Task.FromResult(badActionResponse);
                    }
                    Console.WriteLine("Player='", request.Name, "' bet Accepted");
                    break;
                case 3:
                    // action: call
                    if (!lobby.Call(player))
                    {
                        Console.WriteLine("Player='", request.Name, "' tried to call but not accepted");
                        return Task.FromResult(badActionResponse);
                    }
                    Console.WriteLine("Player='", request.Name, "' call Accepted");
                    break;
                default:
                    Console.WriteLine("Player='", request.Name, "' default case?");
                    return Task.FromResult(badActionResponse);
            }
            //player.lastAction = actionId;
            lobby.UpdateStateAsync();
            return Task.FromResult(new ActionResponse { Success = true });
        }
        public override Task<StartGameResponse> StartGame(StartGameRequest request, ServerCallContext context)
        {
            StartGameResponse badRequest = new StartGameResponse { Success = false };
            PokerGame lobby;
            if (StorageSingleton.Instance.currentGames.Find(game => game.gamePin.Equals(request.Gamepin)) == null)
            {
                return Task.FromResult(badRequest);
            }
            else
            {
                lobby = StorageSingleton.Instance.currentGames.Find(game => game.gamePin.Equals(request.Gamepin));
            }
            foreach (Player player in lobby.players)
            {
                if (player != null && player.name == request.PlayerName)
                {
                    if (player.isRoomOwner)
                    {
                        lobby.NewRound();
                        lobby.UpdateStateAsync();
                        return Task.FromResult(new StartGameResponse { Success = true });
                    }
                }
            }
            return Task.FromResult(badRequest);
        }

        public GameLobby PokerGameToGameLobby(PokerGame pokerGame, string playerName)
        {
            Player playerToAct = new Player();
            if (pokerGame.playersPlaying != null && pokerGame.playersPlaying.Where(p => p.currentBetter).Any())
            {
                playerToAct = pokerGame.playersPlaying.Find(p => p.currentBetter);
            }
            else
            {
                foreach (Player plr in pokerGame.players)
                {
                    if (plr != null && plr.currentBetter)
                    {
                        playerToAct = plr;
                        break;
                    }
                }
                playerToAct = pokerGame.players[0];
            }
            if (playerToAct.name == null)
            {
                playerToAct.name = "unknown";
            }

            GameLobby gamelobby = new GameLobby
            {
                GamePin = pokerGame.gamePin,
                ToAct = playerToAct.name,
                TableCards = pokerGame.GetCards(pokerGame.tableCards),
                Pot = pokerGame.pot,
                Bet = pokerGame.bet,
                Blind = pokerGame.blind,
                MaxBuyin = pokerGame.maxBuyin,
                MinBuyin = pokerGame.minBuyin,
            };
            foreach (Player player in pokerGame.players)
            {
                if (player != null)
                {
                    GPlayer gPlayer = PlayerToGPlayer(player, pokerGame, playerName);
                    gamelobby.Gplayers.Add(gPlayer);
                }
            }
            return gamelobby;
        }

        public GPlayer PlayerToGPlayer(Player player, PokerGame pokerGame, String playerName)
        {
            string hand;
            if (player.Hand == null)
            {
                hand = "0";
            }
            else if (player.name.Equals(playerName))
            {
                hand = "x";
            }
            else
            {
                hand = pokerGame.GetCards(player.Hand);
            }
            GPlayer gParticipant = new GPlayer
            {
                Action = player.lastAction,
                BestCombo = "0",
                Hand = hand,
                IsRoomOwner = player.isRoomOwner,
                Name = player.name,
                Wallet = player.wallet,
                Bet = player.bet
            };
            return gParticipant;
        }
    }
}