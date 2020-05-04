using System;

namespace TicTacToe.Core
{
    public enum Player
    {
        X,
        O
    }
     
    public class TicTacToe
    {
        public const string PLAYER_MOVEMENT_IS_INVALID_EXCEPTION = "The chosen coordinates are invalid.";
        public const string PLAYER_TURN_IS_INVALID_EXCEPTION = "Player turn is invalid.";
        private Player _currentPlayer = Player.X;

        public bool HasWinner()
        {
            return false;
        }

        public void StartWith(Player startingPlayer) =>
            _currentPlayer = startingPlayer;

        public void Play(Player player, int x, int y)
        {
            if (_currentPlayer != player)
            {
                throw new ArgumentException(PLAYER_TURN_IS_INVALID_EXCEPTION);
            }

            if (y < 0 || y > 2)
            {
                throw new ArgumentException(PLAYER_MOVEMENT_IS_INVALID_EXCEPTION);
            }
        }
    }
}
