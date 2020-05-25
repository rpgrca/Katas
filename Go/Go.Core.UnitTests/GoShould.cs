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
    }

    public enum Stone
    {
        Black
    }

    public class Board
    {
        private PositionStatus _positionStatus = PositionStatus.Empty;

        public PositionStatus GetPositionStatus(int x, int y)
        {
            return _positionStatus;
        }

        public void AddStone(Stone stone, int x, int y)
        {
            _positionStatus = PositionStatus.Filled;
        }
    }

    public enum PositionStatus
    {
        BlackStone,
        Empty,
        Filled
    }
}
