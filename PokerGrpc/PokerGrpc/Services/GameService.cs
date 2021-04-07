using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Grpc.Core;
using Microsoft.Extensions.Logging;
using PokerGrpc.Models;

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
            GameLobby gameLobby = new GameLobby
            {
                GamePin = lobby.gamePin,
                ToAct = null,
                TableCards = null,
                Pot = 0,
                Bet = 0,
                Blind = lobby.blind
            };
            gameLobby.Gplayers.Add(new GPlayer
            {
                Action = 0,
                BestCombo = "0",
                Hand = "0",
                IsRoomOwner = player.isRoomOwner,
                Name = player.name,
                Wallet = player.wallet
            });
            return Task.FromResult(gameLobby);
        }
        public override Task<GameLobby> JoinGame(JoinGameRequest request, ServerCallContext context)
        {
            return null;
        }
    }
}
