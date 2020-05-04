using System;
using System.Collections.Generic;
using System.Linq;

namespace TicTacToe.Core
{
    public enum Player
    {
        X = 1,
        O = 2
    }

    public enum Result
    {
        Draw = 0,
        XWon = Player.X,
        OWon = Player.O,
    }
    
    public class TicTacToe
    {
        private enum PlayerSquare
        {
            N = 0,
            X = Player.X,
            O = Player.O
        }
        
        public const string PLAYER_MOVEMENT_IS_INVALID_EXCEPTION = "The chosen coordinates are invalid.";
        public const string PLAYER_TURN_IS_INVALID_EXCEPTION = "Player turn is invalid.";
        
        private Player _currentPlayer = Player.X;
        private readonly PlayerSquare[] _board = { PlayerSquare.N, PlayerSquare.N, PlayerSquare.N,
              PlayerSquare.N, PlayerSquare.N, PlayerSquare.N,
              PlayerSquare.N, PlayerSquare.N, PlayerSquare.N };

        private readonly List<int[]> _winningCombinations = new List<int[]>
        {
            new [] { 0, 1, 2 }, new [] { 3, 4, 5 }, new [] { 6, 7, 8 },
            new [] { 0, 3, 6 }, new [] { 1, 4, 7 }, new [] { 2, 5, 8 },
            new [] { 0, 4, 8 }, new [] { 2, 4, 6 }
        };

        public Result GetResult() =>
            _winningCombinations
                .Where(p =>
                    _board[p[0]] == _board[p[1]] &&
                    _board[p[1]] == _board[p[2]] &&
                    _board[p[0]] != PlayerSquare.N)
                .Select(p => PlayerToResult(_board[p[0]]))
                .FirstOrDefault();

        private static Result PlayerToResult(PlayerSquare square) =>
            square switch
            {
                PlayerSquare.X => Result.XWon,
                PlayerSquare.O => Result.OWon,
                _ => Result.Draw
            };

        public void StartWith(Player startingPlayer) =>
            _currentPlayer = startingPlayer;

        public void Play(Player player, int x, int y)
        {
            if (_currentPlayer != player)
            {
                throw new ArgumentException(PLAYER_TURN_IS_INVALID_EXCEPTION);
                
            }

            var movement = y * 3 + x;
            if (x < 0 || x > 2 || y < 0 || y > 2 || _board[movement] != PlayerSquare.N)
            {
                throw new ArgumentException(PLAYER_MOVEMENT_IS_INVALID_EXCEPTION);
            }

            if (GetResult() != Result.Draw)
            {
                throw new ArgumentException(PLAYER_MOVEMENT_IS_INVALID_EXCEPTION);
            }

            _board[movement] = (PlayerSquare)player;
            _currentPlayer = _currentPlayer == Player.O ? Player.X : Player.O;
        }
    }
}
