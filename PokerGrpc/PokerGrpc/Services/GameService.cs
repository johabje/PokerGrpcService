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
            Player player = new Player();
            player.name = request.Gplayer.Name;
            player.isRoomOwner = false;
            player.Hand = null;
            player.bestCombo = null;
            player.action = 0;
            player.wallet = 0;
            PokerGame lobby = new PokerGame(player, 1);
            
            lobby.gamePin = 888;
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
                MaxBuyin=request.MaxBuyin,
                MinBuyin=request.MinBuyin

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


        public override Task<GameLobby> JoinGame(JoinGameRequest request, ServerCallContext context)
        { 
            PokerGame lobby;
            Player player = null;
            if (StorageSingleton.Instance.currentGames.Find(game => game.gamePin.Equals(request.GamePin)) == null) {
                Console.WriteLine("Lobby not found.");
                return Task.FromResult(new GameLobby { });
            }

            lobby = StorageSingleton.Instance.currentGames.Find(game => game.gamePin.Equals(request.GamePin));
            foreach (Player lobbyPlayer in lobby.players) {
                if (lobbyPlayer != null && lobbyPlayer.name.Equals(request.Gplayer.Name, StringComparison.OrdinalIgnoreCase)) {
                    player = lobbyPlayer;
                    break;
                }
            }
            if (player == null) {
                Console.WriteLine("Player ", request.Gplayer.Name, " not already in table.");
                player = new Player {
                    name = request.Gplayer.Name,
                    wallet = request.Gplayer.Wallet,
                    isRoomOwner = false,
                    Hand = null,
                    bestCombo = null,
                    lastAction = -1
                };

                if (!lobby.AddPlayer(player)) {
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
                    GPlayer gParticipant = new GPlayer {
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
            if (pokerGame.playersPlaying != null && pokerGame.playersPlaying.Where(p => p.currentBetter).Any()) {
                lastBetter = pokerGame.playersPlaying.Find(p => p.currentBetter);
            } else {
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
                GamePin = 888,
                ToAct = gPlayer,
                TableCards = string.Join(", ", lobby.tableCards),
                Pot = 0,
                Bet = 0,
                Blind = 20
            };
            foreach (Player player in pokerGame.players)
            {
                if (player != null)
                {
                    Action = 0,
                    BestCombo = "0",
                    Hand = "0",
                    IsRoomOwner = false,
                    Name = participant.name,
                    Wallet = participant.wallet,
                };
                gameLobby.Gplayers.Add(gParticipant);

            }
            return gamelobby;
        }

        public GPlayer PlayerToGPlayer(Player player, PokerGame pokerGame)
        {
            string hand;
            if (player.Hand == null)
            {
                hand = "0";
            }
            else
            {
                hand = pokerGame.GetCards(player.Hand);
            }
            GPlayer gParticipant = new GPlayer {
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
