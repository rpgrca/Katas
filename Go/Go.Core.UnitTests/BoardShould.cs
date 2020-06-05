using Xunit;

namespace Go.Core.UnitTests
{
    public class BoardShould
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
         
        [Fact]
        public void GivenAStoneWithThreeOppositePiecesAroundx2y3_WhenAddingAFourthStoneRight_ThenItsRemoved()
        {
            var board = CreateBoard();
             
                                                    board.AddStone(StoneColor.White, 2, 2);
            board.AddStone(StoneColor.White, 1, 3); board.AddStone(StoneColor.Black, 2, 3);
                                                    board.AddStone(StoneColor.White, 2, 4);

            board.AddStone(StoneColor.White, 3, 3);
            var status = board.GetPositionStatus(2, 3);
            Assert.Equal(PositionStatus.Empty, status);
        }

        [Fact]
        public void Test1()
        {
            var board = CreateBoard();
            var result = Board.GetWinner();
            Assert.Equal(StoneColor.White, result);
        }
        
        
    }

    public enum StoneColor
    {
        Black,
        White
    }

    public enum PositionStatus
    {
        Empty,
        Filled
    }
}
