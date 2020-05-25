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

            board.AddStone(StoneColor.Black, 1, 1);
            var result = board.GetPositionStatus(1, 1);
            Assert.Equal(PositionStatus.Filled, result);
        }

        [Fact]
        public void GivenAGameWithAStoneSetAtx1y1_WhenAskingForStatusAtx1y2_ThenItsEmpty()
        {
            var board = CreateBoard();
            
            board.AddStone(StoneColor.Black, 1, 1);
            var status = board.GetPositionStatus(1, 2);
            
            Assert.Equal(PositionStatus.Empty, status);
        }

        [Fact]
        public void GivenAStoneWithThreeOppositePiecesAroundx2y2_WhenAddingAFourthStoneBelow_ThenItsRemoved()
        {
            var board = CreateBoard();
            
                                                    board.AddStone(StoneColor.White, 2, 1);
            board.AddStone(StoneColor.White, 1, 2); board.AddStone(StoneColor.Black, 2, 2); board.AddStone(StoneColor.White, 3, 2);
                                                    board.AddStone(StoneColor.White, 2, 3);

            var status = board.GetPositionStatus(2, 2);
            Assert.Equal(PositionStatus.Empty, status);
        }
        
        [Fact]
        public void GivenAStoneWithThreeOppositePiecesAroundx2y3_WhenAddingAFourthStoneBelow_ThenItsRemoved()
        {
            var board = CreateBoard();
            
                                                    board.AddStone(StoneColor.White, 2, 2);
            board.AddStone(StoneColor.White, 1, 3); board.AddStone(StoneColor.Black, 2, 3); board.AddStone(StoneColor.White, 3, 3);
                                                    board.AddStone(StoneColor.White, 2, 4);

            var status = board.GetPositionStatus(2, 3);
            Assert.Equal(PositionStatus.Empty, status);
        }
        
        [Fact]
        public void GivenAStoneWithThreeOppositePiecesAroundx2y3_WhenAddingAFourthStoneAbove_ThenItsRemoved()
        {
            var board = CreateBoard();

            board.AddStone(StoneColor.White, 1, 3); board.AddStone(StoneColor.Black, 2, 3); board.AddStone(StoneColor.White, 3, 3);
                                                    board.AddStone(StoneColor.White, 2, 4);
 
             board.AddStone(StoneColor.White, 2, 2);
            var status = board.GetPositionStatus(2, 3);
            Assert.Equal(PositionStatus.Empty, status);
        }

         [Fact]
         public void GivenAStoneWithThreeOppositePiecesAroundx2y3_WhenAddingAFourthStoneLeft_ThenItsRemoved()
         {
             var board = CreateBoard();
 
                                                     board.AddStone(StoneColor.White, 2, 2);
                                                     board.AddStone(StoneColor.Black, 2, 3); board.AddStone(StoneColor.White, 3, 3);
                                                     board.AddStone(StoneColor.White, 2, 4);

             board.AddStone(StoneColor.White, 1, 3); 

             var status = board.GetPositionStatus(2, 3);
             Assert.Equal(PositionStatus.Empty, status);
         }
    }

    public enum StoneColor
    {
        Black,
        White
    }

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

            RemoveSurroundedStone(x, y - 1);
            RemoveSurroundedStone(x, y + 1);
            RemoveSurroundedStone(x + 1, y);
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
    }

    public enum PositionStatus
    {
        Empty,
        Filled
    }
}
