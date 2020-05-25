using System;
using Xunit;

namespace Go.Core.UnitTests
{
    public class UnitTest1
    {
        [Fact]
        public void GivenANewGame_WhenAskingStatusOfAnyPosition_ThenItIsEmpty()
        {
            var board = CreateBoard();

            var result = board.GetPositionStatus(1, 1);
            
            Assert.Equal(PositionStatus.Empty, result);
        }

        private static Board CreateBoard()
        {
            return new Board();
        }

        [Fact]
        public void GivenAGameWithAStoneSetAtx1y1_WhenAskingForStatus_ThenItsFilled()
        {
            var board = CreateBoard();

            board.AddStone(Stone.Black, 1, 1);
            var result = board.GetPositionStatus(1, 1);
            Assert.Equal(PositionStatus.Filled, result);
        }

        [Fact]
        public void GivenAGameWithAStoneSetAtx1y1_WhenAskingForStatusAtx1y2_ThenItsEmpty()
        {
            var board = CreateBoard();
            
            board.AddStone(Stone.Black, 1, 1);
            var status = board.GetPositionStatus(1, 2);
            
            Assert.Equal(PositionStatus.Empty, status);

        }
    }

    public enum Stone
    {
        Black
    }

    public class Board
    {
        private const int BOARD_SIZE = 19;
        private readonly PositionStatus[,] _positionStatusMatrix = new PositionStatus[BOARD_SIZE, BOARD_SIZE];

        public PositionStatus GetPositionStatus(int x, int y)
        {
            return _positionStatusMatrix[x - 1 , y - 1];
        }

        public void AddStone(Stone stone, int x, int y)
        {
            _positionStatusMatrix[x - 1, y - 1] = PositionStatus.Filled;
        }
    }

    public enum PositionStatus
    {
        Empty,
        Filled
    }
}
