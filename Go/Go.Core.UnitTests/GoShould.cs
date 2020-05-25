using System;
using Xunit;

namespace Go.Core.UnitTests
{
    public class UnitTest1
    {
        [Fact]
        public void GivenANewGame_WhenAskingStatusOfAnyPosition_ThenItIsEmpty()
        {
            Board board = new Board();

            PositionStatus result = board.GetPositionStatus(1, 1);
            
            Assert.Equal(PositionStatus.Empty, result);
        }
    }

    public class Board
    {
        public PositionStatus GetPositionStatus(int x, int y)
        {
            return PositionStatus.Empty;
        }
    }

    public enum PositionStatus
    {
        BlackStone,
        Empty
    }
}
