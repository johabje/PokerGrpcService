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
                Action = 0,
                BestCombo = "0",
                Hand = "0",
                IsRoomOwner = true,
                Name = player.name,
                Wallet = player.wallet,
            };
            GameLobby gameLobby = new GameLobby
            {
                GamePin = 888,
                ToAct = gPlayer,
                TableCards = "0",
                Pot = 0,
                Bet = 0,
                Blind = 20
            };
            gameLobby.Gplayers.Add(gPlayer);
            StorageSingleton.Instance.currentGames.Add(lobby);
            Console.WriteLine(StorageSingleton.Instance.currentGames);
            return Task.FromResult(gameLobby);
            
        }


        public override Task<GameLobby> JoinGame(JoinGameRequest request, ServerCallContext context)
        {
            Player player = new Player
            {
                action = request.Gplayer.Action,
                isRoomOwner = false,
                wallet = 0,
                name = request.Gplayer.Name,
                bestCombo = request.Gplayer.BestCombo,
                Hand = null,
            };
            PokerGame lobby = StorageSingleton.Instance.currentGames.Find(game => game.gamePin.Equals(request.GamePin));
            lobby.players.Add(player);
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
                TableCards = string.Join(", ", lobby.tableCards),
                Pot = 0,
                Bet = 0,
                Blind = 20
            };
            foreach (var participant in lobby.players)
            {
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
            return Task.FromResult(gameLobby);
        }

        public override async Task StartStream(JoinGameRequest request, IServerStreamWriter<GameLobby> responseStream, ServerCallContext context)
        {
            PokerGame pokerGame = StorageSingleton.Instance.currentGames.Find(game => game.gamePin.Equals(request.GamePin));
            while (true)
            {
                PokerGame pokergame2 = StorageSingleton.Instance.currentGames.Find(game => game.gamePin.Equals(request.GamePin));
                if (pokerGame == pokergame2)
                {
                    await responseStream.WriteAsync(PokerGameToGameLobby(pokergame2));
                    pokerGame = pokergame2;
                }
                await Task.Delay(1000); //gotta look bussy
            }
        }

        public GameLobby PokerGameToGameLobby(PokerGame pokerGame)
        {
            GameLobby gamelobby = new GameLobby
            {
                GamePin = 888,
                ToAct = PlayerToGPlayer(pokerGame.toAct),
                TableCards = string.Join(", ", pokerGame.tableCards),
                Pot = 0,
                Bet = 0,
                Blind = 20
            };
            foreach (var player in pokerGame.players)
            {
                GPlayer gPlayer = PlayerToGPlayer(player);
                gamelobby.Gplayers.Add(gPlayer);
            }
            return gamelobby;
        }

        public GPlayer PlayerToGPlayer(Player player)
        {
            GPlayer gParticipant = new GPlayer
            {
                Action = 0,
                BestCombo = "0",
                Hand = "0",
                IsRoomOwner = false,
                Name = player.name,
                Wallet = player.wallet,
            };
            return gParticipant;
        }
    }
}
