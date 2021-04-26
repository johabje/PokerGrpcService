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
                lastAction = -1,
                wallet = request.Gplayer.Wallet
            };
            PokerGame lobby = new PokerGame(player, 1, request.GamePin, 6);

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
                GamePin = lobby.gamePin,
                ToAct = gPlayer.Name,
                TableCards = "0",
                Pot = 0,
                Bet = 0,
                Blind = 20
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
            Player player = new Player
            {
                name = request.Gplayer.Name,
                isRoomOwner = false,
                Hand = null,
                bestCombo = null,
                lastAction = -1,
                wallet = request.Gplayer.Wallet,
            };

            PokerGame lobby;
            try {
                lobby = StorageSingleton.Instance.currentGames.Find(game => game.gamePin.Equals(request.GamePin));
            } catch {
                return Task.FromResult(new GameLobby { });
            }

            if (!lobby.AddPlayer(player))
            {
                //maybe throw permmission denied error?
                return Task.FromResult(new GameLobby { });
            }

            /*
            GPlayer gPlayer = new GPlayer
            {
                Action = -1,
                BestCombo = "0",
                Hand = "0",
                IsRoomOwner = false,
                Name = player.name,
                Wallet = player.wallet,
            };
            */

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
                    };
                    gameLobby.Gplayers.Add(gParticipant);
                }
            }
            return Task.FromResult(gameLobby);
        }

        public override async Task StartStream(JoinGameRequest request, IServerStreamWriter<GameLobby> responseStream, ServerCallContext context)
        {
            await Task.Delay(1000);
            PokerGame pokerGame = StorageSingleton.Instance.currentGames.Find(game => game.gamePin.Equals(request.GamePin));
            //Console.WriteLine("pokergamePin" + pokerGame.gamePin);
            Player lastBetter = pokerGame.playersPlaying.Find(p => p.currentBetter);
            int lastTableCardsCount = pokerGame.tableCards.Count;

            Player currentBetter;
            int tableCardsCount;
            state currentState;

            await responseStream.WriteAsync(PokerGameToGameLobby(pokerGame, request.Gplayer.Name));
            
            while (true)
            {
                currentBetter = pokerGame.playersPlaying.Find(p => p.currentBetter);
                tableCardsCount = pokerGame.tableCards.Count;
                currentState = pokerGame.state;
                //Console.WriteLine(currentState);
                if (!lastBetter.Equals(currentBetter) || !lastTableCardsCount.Equals(tableCardsCount))
                {
                    await responseStream.WriteAsync(PokerGameToGameLobby(pokerGame, request.Gplayer.Name));
                    lastBetter = currentBetter;
                    lastTableCardsCount = tableCardsCount;
                }
                else if (currentState == state.Showdown)
                {


                    // return the game with winner etc
                    GameLobby lobby = new GameLobby
                    {
                        Pot = pokerGame.pot,
                        TableCards = pokerGame.GetCards(pokerGame.tableCards),
                        Winner = pokerGame.winner.name, 
                    };

                    //add code to reset the game here.

                }
                else { 
                   // Console.WriteLine("No change in game state");
                }
                
                await Task.Delay(1000); //gotta look bussy
                //Console.WriteLine("The bets are: " + pokerGame.bet+ " and " + pokergame2.bet);
            }
        }

        public override Task<ActionResponse> Action(ActionRequest request, ServerCallContext context)
        { 
            ActionResponse badActionResponse = new ActionResponse {Success = false};
            PokerGame lobby;
            try {
                lobby = StorageSingleton.Instance.currentGames.Find(game => game.gamePin.Equals(request.GamePin));
            } catch {
                return Task.FromResult(badActionResponse);
            }

            Player player;
            try {
                player = lobby.playersPlaying.Find(p => p.name.Equals(request.Name));
            } catch {
                return Task.FromResult(badActionResponse);
            }

            if (player == null || !player.currentBetter) {
                //player not found or not the players turn to act -> return false
                return Task.FromResult(badActionResponse);
            }
            int actionId = request.Action;
            switch (actionId) {
                case 0:
                    // action: fold
                    lobby.Fold(player);
                    break;
                case 1:
                    // action: check;
                    if (!lobby.Check(player)) {
                        return Task.FromResult(badActionResponse);
                    }
                    break;
                case 2:
                    // action: bet (== raise)
                    if (!lobby.PlaceBet(player, request.Bet)) {
                        return Task.FromResult(badActionResponse);
                    }
                    break;
                case 3:
                    // action: call
                    if(!lobby.Call(player)) {
                        return Task.FromResult(badActionResponse);
                    }
                    break;
                default:
                    return Task.FromResult(badActionResponse);
            }
            player.lastAction = actionId;
            lobby.UpdateStateAsync();
            return Task.FromResult(new ActionResponse { Success = true });
        }
        public override Task<StartGameResponse> StartGame(StartGameRequest request, ServerCallContext context)
        {
            StartGameResponse badActionResponse = new StartGameResponse { Success = false };
            PokerGame lobby;
            try
            {
                lobby = StorageSingleton.Instance.currentGames.Find(game => game.gamePin.Equals(request.Gamepin));
            }
            catch
            {
                return Task.FromResult(badActionResponse);
            }

            foreach (Player player in lobby.players)
            {
                if (player != null && player.name == request.PlayerName)
                {
                    if (player.isRoomOwner)
                    {
                        lobby.NewRound();
                        return Task.FromResult(new StartGameResponse { Success = true });
                    }
                }
            }
            return Task.FromResult(badActionResponse);

        }

        public GameLobby PokerGameToGameLobby(PokerGame pokerGame, string playerName)
        {
            Player playerToAct = pokerGame.playersPlaying.Find(p => p.currentBetter);
            GameLobby gamelobby = new GameLobby {
                GamePin = pokerGame.gamePin,
                ToAct = playerToAct.name,
                TableCards = pokerGame.GetCards(pokerGame.tableCards),
                Pot = pokerGame.pot,
                Bet = pokerGame.bet,
                Blind = pokerGame.blind
            };
            foreach (Player player in pokerGame.players)
            {
                if (player != null)
                {
                    GPlayer gPlayer = PlayerToGPlayer(player, pokerGame);
                    if (gPlayer.Name != playerName)
                    {
                        gPlayer.Hand = "x";
                    }
                    gamelobby.Gplayers.Add(gPlayer);
                }
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
                Action = 0,
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
