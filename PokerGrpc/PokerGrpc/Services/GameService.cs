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
            Player player = new Player {
                name = request.Gplayer.Name,
                isRoomOwner = false,
                Hand = null,
                bestCombo = null,
                action = 0,
                wallet = 1000
            };

            //TODO - generage random pin of x digits
            // note: must be unique (not same as another game)
            int gamePin = 888;

            //PokerGame(Player, int blind, int gamePin)
            PokerGame lobby = new PokerGame(player, 5, gamePin);
            
            // ***
            // response stuff
            GPlayer gPlayer = new GPlayer
            {
                Action = 0,
                BestCombo = "0",
                Hand = "0",
                IsRoomOwner = true,
                Name = player.name,
                Wallet = player.wallet,
            };
            //TODO include all info the players need
            GameLobby gameLobby = new GameLobby
            {
                GamePin = 888,
                ToAct = gPlayer,
                TableCards = lobby.GetTableCards(),
                Pot = 0,
                Bet = 0,
                Blind = 20
            };
            gameLobby.Gplayers.Add(gPlayer);
            gameLobby.Gplayers.Add(gPlayer);
            // ***

            StorageSingleton.Instance.currentGames.Add(lobby);
            Console.WriteLine(StorageSingleton.Instance.currentGames);
            return Task.FromResult(gameLobby);
        }

        public override Task<GameLobby> JoinGame(JoinGameRequest request, ServerCallContext context)
        {
            PokerGame lobby;

            try {
                lobby = StorageSingleton.Instance.currentGames.Find(game => game.gamePin.Equals(request.GamePin));
                // TODO also check if poker table not full
            } catch (ArgumentNullException e) {
                // game with pin not found
                // returning empty GameLobby
                return Task.FromResult(new GameLobby { });
            }

            lobby = StorageSingleton.Instance.currentGames.Find(game => game.gamePin.Equals(request.GamePin));

            Player player = new Player
            {
                action = request.Gplayer.Action,
                isRoomOwner = false,
                wallet = 0,
                name = request.Gplayer.Name,
                bestCombo = request.Gplayer.BestCombo,
                Hand = null,
            };
            

            lobby.AddPlayer(player);
            GPlayer gPlayer = new GPlayer
            {
                Action = 0,
                BestCombo = "0",
                Hand = "0",
                IsRoomOwner = false,
                Name = player.name,
                Wallet = player.wallet,
            };
            GameLobby gameLobby = new GameLobby
            {
                GamePin = 888,
                ToAct = gPlayer,
                TableCards = lobby.GetTableCards(),
                Pot = 0,
                Bet = 0,
                Blind = 20
            };
            foreach (var participant in lobby.players)
            {
                if (participant != null) {
                    GPlayer gParticipant = new GPlayer
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
               
            }
            return Task.FromResult(gameLobby);
        }
    }
}
