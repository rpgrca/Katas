using System;

namespace Go.Core.UnitTests
{
    public class Board
    {
        private const int BOARD_SIZE = 19;
        private readonly PositionStatus[,] _positionStatusMatrix = new PositionStatus[BOARD_SIZE, BOARD_SIZE];
        private readonly StoneColor[,] _stoneColorMatrix = new StoneColor[BOARD_SIZE, BOARD_SIZE];

        public PositionStatus GetPositionStatus(int x, int y)
        {
            return _positionStatusMatrix[x, y];
        }

        public void AddStone(StoneColor stoneColor, int x, int y)
        {
            _positionStatusMatrix[x, y] = PositionStatus.Filled;
            _stoneColorMatrix[x, y] = stoneColor;

            CheckStonesAroundPositionAndRemoveIfNeeded(x, y);
        }

        private void CheckStonesAroundPositionAndRemoveIfNeeded(int x, int y)
        {
            RemoveSurroundedStone(x, y - 1);
            RemoveSurroundedStone(x, y + 1);
            RemoveSurroundedStone(x + 1, y);
            RemoveSurroundedStone(x - 1, y);
        }

        private void RemoveSurroundedStone(int x, int y)
        {
            try
            {
                var surroundedOnLeft = x == 0 || _stoneColorMatrix[x - 1, y] == StoneColor.White;
                var surroundedOnBottom = _stoneColorMatrix[x, y + 1] == StoneColor.White;
                var surroundedOnRight = _stoneColorMatrix[x + 1, y] == StoneColor.White;
                var surroundedOnTop = y == 0 || _stoneColorMatrix[x, y - 1] == StoneColor.White;

                if (surroundedOnLeft &&
                    surroundedOnRight &&
                    surroundedOnTop &&
                    surroundedOnBottom)
                {
                    _positionStatusMatrix[x, y] = PositionStatus.Empty;
                }
            }
            catch (IndexOutOfRangeException)
            {
                
            }
        }

        public static StoneColor GetWinner()
        {
            return StoneColor.White;
        }
    }
}