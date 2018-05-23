using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Pente;

namespace PenteTests
{
    [TestClass]
    public class BoardTests
    {
        private readonly Piece[] successfulCapture = { Piece.P1, Piece.EMPTY, Piece.EMPTY, Piece.P1 };
        private Vec2 start = new Vec2(9, 9);

        #region Initialize Tests
        [TestMethod]
        public void BoardInitialize_Players_Test()
        {
            Board board = new Board();
            Player p1 = new Player();
            Player p2 = new Player();

            board.Initialize(p1, p2, 19);

            Assert.IsTrue((p1 == board.p1) && (p2 == board.p2));
        }

        [TestMethod]
        public void BoardInitialize_Grid_Test()
        {
            Board board = new Board();
            Player p1 = new Player();
            Player p2 = new Player();

            CheckProperInitialization(board, p1, p2, 19);
            CheckProperInitialization(board, p1, p2, 9);
            CheckProperInitialization(board, p1, p2, 31);
        }

        private static void CheckProperInitialization(Board board, Player p1, Player p2, int size)
        {
            board.Initialize(p1, p2, size);

            int xLength = board.Grid.GetLength(0);
            int yLength = board.Grid.GetLength(1);

            for (int i = 0; i < xLength; i++)
            {
                for (int j = 0; j < yLength; j++)
                {
                    Assert.AreEqual((i == xLength / 2 && j == yLength / 2) ? Piece.P1 : Piece.EMPTY, board.Grid[i, j]);
                }
            }
        }

        [TestMethod]
        public void BoardInitialize_Grid_ReInit_Test()
        {
            Board board = new Board();
            Player p1 = new Player();
            Player p2 = new Player();

            board.Initialize(p1, p2, 19);

            board.Grid.Set(start, Piece.P2);

            CheckProperInitialization(board, p1, p2, 19);

            board.Grid.Set(start, Piece.P2);

            CheckProperInitialization(board, p1, p2, 11);

            board.Grid.Set(start, Piece.P2);

            CheckProperInitialization(board, p1, p2, 27);
        }

        [TestMethod]
        public void BoardInitialize_Grid_Dimensions_Test()
        {
            Board board = new Board();
            Player p1 = new Player();
            Player p2 = new Player();

            for (int i = 9; i < 40; i += 2)
            {
                board.Initialize(p1, p2, i);
                Assert.AreEqual(i, board.Grid.GetLength(0));
                Assert.AreEqual(i, board.Grid.GetLength(1));
            }
        }

        [TestMethod]
        public void BoardInitialize_TurnCount_Test()
        {
            Board board = new Board();
            Player p1 = new Player();
            Player p2 = new Player();


            board.turnCount = 5;

            board.Initialize(p1, p2, 19);
            Assert.AreEqual(1, board.turnCount);


            board.turnCount = int.MaxValue;

            board.Initialize(p1, p2, 9);
            Assert.AreEqual(1, board.turnCount);


            board.turnCount = int.MinValue;

            board.Initialize(p1, p2, 39);
            Assert.AreEqual(1, board.turnCount);
        }
        #endregion

        #region ValidPlacement Tests
        [TestMethod]
        public void PlacePieceTest()
        {
            Board board = new Board();
            Player p1 = new Player();
            Player p2 = new Player();

            board.Initialize(p1, p2, 19);

            board.PlacePiece(new Vec2(0, 0));
            Assert.AreEqual(board.Grid.Get(new Vec2(0, 0)), Piece.P2);
        }

        [TestMethod]
        public void PlacePieceCenterTest()
        {
            Board board = new Board();
            Player p1 = new Player();
            Player p2 = new Player();

            board.Initialize(p1, p2, 19);

            board.PlacePiece(new Vec2(9, 9));
            Assert.AreEqual(board.Grid.Get(new Vec2(9, 9)), Piece.P1);
        }

        [TestMethod]
        public void IsValidPlacementTrue()
        {
            Board board = new Board();
            Player p1 = new Player();
            Player p2 = new Player();

            board.Initialize(p1, p2, 19);

            bool thingy = board.IsValidPlacement(new Vec2(0, 0));

            Assert.IsTrue(thingy);

        }

        [TestMethod]
        public void IsValidPlacementFalse()
        {
            Board board = new Board();
            Player p1 = new Player();
            Player p2 = new Player();

            board.Initialize(p1, p2, 19);

            bool thingy = board.IsValidPlacement(new Vec2(9, 9));

            Assert.IsFalse(thingy);

        }

        [TestMethod]
        public void IsValidPlacementTournament()
        {
            Board board = new Board();
            Player p1 = new Player();
            Player p2 = new Player();

            board.Initialize(p1, p2, 19);
            board.turnCount = 2;

            bool thingy = board.IsValidPlacement(new Vec2(9, 8));

            Assert.IsFalse(thingy);

        }
        #endregion

        #region Capture Checks
        public delegate bool CheckForDelegate(Vec2 position);

        public void BoardTestCheckForPatternMethod(ref Board board, Vec2 direction, Piece[] pattern, Piece playerPiece, CheckForDelegate checkForDelegate)
        {
            Player p1 = new Player();
            Player p2 = new Player();

            pattern = pattern.GetPatternFor(playerPiece);

            board.Initialize(p1, p2, 19);
            board.turnCount = playerPiece == Piece.P1 ? 0 : 1;

            for (int i = 0; i < pattern.Length; i++)
            {
                board.Grid.Set(start + direction * i, pattern[i]);
            }

            Assert.IsTrue(checkForDelegate(start));
        }

        private void PatternExists(Board board, Vec2 direction, Piece[] patternToFind, Piece playerPiece)
        {
            patternToFind = patternToFind.GetPatternFor(playerPiece);

            for (int i = 0; i < patternToFind.Length; i++)
            {
                Assert.AreEqual(patternToFind[i], board.Grid.Get(start + direction * i));
            }
        }

        [TestMethod]
        public void BoardCheckForCapture_CapturesAll_P1_Test()
        {
            Board board = new Board();

            Player p1 = new Player();
            Player p2 = new Player();

            board.Initialize(p1, p2, 19);

            board.turnCount = 2;

            Piece[] pattern = board.Capture.GetPatternFor(Piece.P1);

            foreach (Vec2 direction in board.directions)
            {
                for (int i = 0; i < board.Capture.Length; i++)
                {
                    board.Grid.Set(start + direction * i, pattern[i]);

                    board.Grid.Set(start - direction * i, pattern[i]);
                }
            }

            bool test = board.CheckForCapture(start);

            foreach (Vec2 direction in board.directions)
            {
                PatternExists(board, direction, successfulCapture, Piece.P1);

                PatternExists(board, -direction, successfulCapture, Piece.P1);
            }
        }

        [TestMethod]
        public void BoardCheckForCapture_CapturesAll_P2_Test()
        {
            Board board = new Board();

            Player p1 = new Player();
            Player p2 = new Player();

            board.Initialize(p1, p2, 19);

            Piece[] pattern = board.Capture.GetPatternFor(Piece.P2);

            foreach (Vec2 direction in board.directions)
            {
                for (int i = 0; i < board.Capture.Length; i++)
                {
                    board.Grid.Set(start + direction * i, pattern[i]);

                    board.Grid.Set(start - direction * i, pattern[i]);
                }
            }

            board.CheckForCapture(start);

            foreach (Vec2 direction in board.directions)
            {
                PatternExists(board, direction, successfulCapture, Piece.P2);

                PatternExists(board, -direction, successfulCapture, Piece.P2);
            }
        }

        [TestMethod]
        public void BoardCheckForCapture_South_P1_Test()
        {
            Board board = new Board();

            Vec2 direction = new Vec2 { x = 0, y = 1 };
            BoardTestCheckForPatternMethod(ref board, direction, board.Capture, Piece.P1, board.CheckForCapture);

            PatternExists(board, direction, successfulCapture, Piece.P1);
        }

        [TestMethod]
        public void BoardCheckForCapture_North_P1_Test()
        {
            Board board = new Board();
            
            Vec2 direction = new Vec2 { x = 0, y = -1 };
            BoardTestCheckForPatternMethod(ref board, direction, board.Capture, Piece.P1, board.CheckForCapture);

            PatternExists(board, direction, successfulCapture, Piece.P1);
        }

        [TestMethod]
        public void BoardCheckForCapture_East_P1_Test()
        {
            Board board = new Board();
            
            Vec2 direction = new Vec2 { x = 1, y = 0 };
            BoardTestCheckForPatternMethod(ref board, direction, board.Capture, Piece.P1, board.CheckForCapture);

            PatternExists(board, direction, successfulCapture, Piece.P1);
        }

        [TestMethod]
        public void BoardCheckForCapture_West_P1_Test()
        {
            Board board = new Board();

            Vec2 direction = new Vec2 { x = -1, y = 0 };
            BoardTestCheckForPatternMethod(ref board, direction, board.Capture, Piece.P1, board.CheckForCapture);

            PatternExists(board, direction, successfulCapture, Piece.P1);
        }

        [TestMethod]
        public void BoardCheckForCapture_SouthEast_P1_Test()
        {
            Board board = new Board();
            
            Vec2 direction = new Vec2 { x = 1, y = 1 };
            BoardTestCheckForPatternMethod(ref board, direction, board.Capture, Piece.P1, board.CheckForCapture);

            PatternExists(board, direction, successfulCapture, Piece.P1);
        }

        [TestMethod]
        public void BoardCheckForCapture_NorthWest_P1_Test()
        {
            Board board = new Board();
            
            Vec2 direction = new Vec2 { x = -1, y = -1 };
            BoardTestCheckForPatternMethod(ref board, direction, board.Capture, Piece.P1, board.CheckForCapture);

            PatternExists(board, direction, successfulCapture, Piece.P1);
        }

        [TestMethod]
        public void BoardCheckForCapture_NorthEast_P1_Test()
        {
            Board board = new Board();
            
            Vec2 direction = new Vec2 { x = 1, y = -1 };
            BoardTestCheckForPatternMethod(ref board, direction, board.Capture, Piece.P1, board.CheckForCapture);

            PatternExists(board, direction, successfulCapture, Piece.P1);
        }

        [TestMethod]
        public void BoardCheckForCapture_SouthWest_P1_Test()
        {
            Board board = new Board();

            Vec2 direction = new Vec2 { x = -1, y = 1 };
            BoardTestCheckForPatternMethod(ref board, direction, board.Capture, Piece.P1, board.CheckForCapture);

            PatternExists(board, direction, successfulCapture, Piece.P1);
        }

        [TestMethod]
        public void BoardCheckForCapture_South_P2_Test()
        {
            Board board = new Board();

            Vec2 direction = new Vec2 { x = 0, y = 1 };
            BoardTestCheckForPatternMethod(ref board, direction, board.Capture, Piece.P2, board.CheckForCapture);

            PatternExists(board, direction, successfulCapture, Piece.P2);
        }

        [TestMethod]
        public void BoardCheckForCapture_North_P2_Test()
        {
            Board board = new Board();

            Vec2 direction = new Vec2 { x = 0, y = -1 };
            BoardTestCheckForPatternMethod(ref board, direction, board.Capture, Piece.P2, board.CheckForCapture);

            PatternExists(board, direction, successfulCapture, Piece.P2);
        }

        [TestMethod]
        public void BoardCheckForCapture_East_P2_Test()
        {
            Board board = new Board();

            Vec2 direction = new Vec2 { x = 1, y = 0 };
            BoardTestCheckForPatternMethod(ref board, direction, board.Capture, Piece.P2, board.CheckForCapture);

            PatternExists(board, direction, successfulCapture, Piece.P2);
        }

        [TestMethod]
        public void BoardCheckForCapture_West_P2_Test()
        {
            Board board = new Board();

            Vec2 direction = new Vec2 { x = -1, y = 0 };
            BoardTestCheckForPatternMethod(ref board, direction, board.Capture, Piece.P2, board.CheckForCapture);

            PatternExists(board, direction, successfulCapture, Piece.P2);
        }

        [TestMethod]
        public void BoardCheckForCapture_SouthEast_P2_Test()
        {
            Board board = new Board();

            Vec2 direction = new Vec2 { x = 1, y = 1 };
            BoardTestCheckForPatternMethod(ref board, direction, board.Capture, Piece.P2, board.CheckForCapture);

            PatternExists(board, direction, successfulCapture, Piece.P2);
        }

        [TestMethod]
        public void BoardCheckForCapture_NorthWest_P2_Test()
        {
            Board board = new Board();

            Vec2 direction = new Vec2 { x = -1, y = -1 };
            BoardTestCheckForPatternMethod(ref board, direction, board.Capture, Piece.P2, board.CheckForCapture);

            PatternExists(board, direction, successfulCapture, Piece.P2);
        }

        [TestMethod]
        public void BoardCheckForCapture_NorthEast_P2_Test()
        {
            Board board = new Board();

            Vec2 direction = new Vec2 { x = 1, y = -1 };
            BoardTestCheckForPatternMethod(ref board, direction, board.Capture, Piece.P2, board.CheckForCapture);

            PatternExists(board, direction, successfulCapture, Piece.P2);
        }

        [TestMethod]
        public void BoardCheckForCapture_SouthWest_P2_Test()
        {
            Board board = new Board();

            Vec2 direction = new Vec2 { x = -1, y = 1 };
            BoardTestCheckForPatternMethod(ref board, direction, board.Capture, Piece.P2, board.CheckForCapture);

            PatternExists(board, direction, successfulCapture, Piece.P2);
        }
        #endregion

        #region Win Checks
        [TestMethod]
        public void BoardCheckForWin_FiveCaptures_P1_Test()
        {
            Board board = new Board();
            Player p1 = new Player();
            Player p2 = new Player();

            board.Initialize(p1, p2, 19);
            board.p1.Captures = 5;

            Assert.IsTrue(board.CheckForWin(start));
        }

        [TestMethod]
        public void BoardCheckForWin_FiveCaptures_P2_Test()
        {
            Board board = new Board();
            Player p1 = new Player();
            Player p2 = new Player();

            board.Initialize(p1, p2, 19);
            board.turnCount = 1;
            board.p2.Captures = 5;

            Assert.IsTrue(board.CheckForWin(start));
        }

        [TestMethod]
        public void BoardCheckForWin_South_P1_Test()
        {
            Board board = new Board();
            
            Vec2 direction = new Vec2 { x = 0, y = 1 };
            BoardTestCheckForPatternMethod(ref board, direction, board.Win, Piece.P1, board.CheckForWin);
        }

        [TestMethod]
        public void BoardCheckForWin_North_P1_Test()
        {
            Board board = new Board();
            
            Vec2 direction = new Vec2 { x = 0, y = -1 };
            BoardTestCheckForPatternMethod(ref board, direction, board.Win, Piece.P1, board.CheckForWin);
        }

        [TestMethod]
        public void BoardCheckForWin_East_P1_Test()
        {
            Board board = new Board();
            
            Vec2 direction = new Vec2 { x = 1, y = 0 };
            BoardTestCheckForPatternMethod(ref board, direction, board.Win, Piece.P1, board.CheckForWin);
        }

        [TestMethod]
        public void BoardCheckForWin_West_P1_Test()
        {
            Board board = new Board();

            Vec2 direction = new Vec2 { x = -1, y = 0 };
            BoardTestCheckForPatternMethod(ref board, direction, board.Win, Piece.P1, board.CheckForWin);
        }

        [TestMethod]
        public void BoardCheckForWin_SouthEast_P1_Test()
        {
            Board board = new Board();
            
            Vec2 direction = new Vec2 { x = 1, y = 1 };
            BoardTestCheckForPatternMethod(ref board, direction, board.Win, Piece.P1, board.CheckForWin);
        }

        [TestMethod]
        public void BoardCheckForWin_NorthWest_P1_Test()
        {
            Board board = new Board();

            Vec2 direction = new Vec2 { x = -1, y = -1 };
            BoardTestCheckForPatternMethod(ref board, direction, board.Win, Piece.P1, board.CheckForWin);
        }

        [TestMethod]
        public void BoardCheckForWin_NorthEaast_P1_Test()
        {
            Board board = new Board();
            
            Vec2 direction = new Vec2 { x = 1, y = -1 };
            BoardTestCheckForPatternMethod(ref board, direction, board.Win, Piece.P1, board.CheckForWin);
        }

        [TestMethod]
        public void BoardCheckForWin_SouthWest_P1_Test()
        {
            Board board = new Board();

            Vec2 direction = new Vec2 { x = -1, y = 1 };
            BoardTestCheckForPatternMethod(ref board, direction, board.Win, Piece.P1, board.CheckForWin);
        }

        [TestMethod]
        public void BoardCheckForWin_South_P2_Test()
        {
            Board board = new Board();

            Vec2 direction = new Vec2 { x = 0, y = 1 };
            BoardTestCheckForPatternMethod(ref board, direction, board.Win, Piece.P2, board.CheckForWin);
        }

        [TestMethod]
        public void BoardCheckForWin_North_P2_Test()
        {
            Board board = new Board();

            Vec2 direction = new Vec2 { x = 0, y = -1 };
            BoardTestCheckForPatternMethod(ref board, direction, board.Win, Piece.P2, board.CheckForWin);
        }

        [TestMethod]
        public void BoardCheckForWin_East_P2_Test()
        {
            Board board = new Board();

            Vec2 direction = new Vec2 { x = 1, y = 0 };
            BoardTestCheckForPatternMethod(ref board, direction, board.Win, Piece.P2, board.CheckForWin);
        }

        [TestMethod]
        public void BoardCheckForWin_West_P2_Test()
        {
            Board board = new Board();

            Vec2 direction = new Vec2 { x = -1, y = 0 };
            BoardTestCheckForPatternMethod(ref board, direction, board.Win, Piece.P2, board.CheckForWin);
        }

        [TestMethod]
        public void BoardCheckForWin_SouthEast_P2_Test()
        {
            Board board = new Board();

            Vec2 direction = new Vec2 { x = 1, y = 1 };
            BoardTestCheckForPatternMethod(ref board, direction, board.Win, Piece.P2, board.CheckForWin);
        }

        [TestMethod]
        public void BoardCheckForWin_NorthWest_P2_Test()
        {
            Board board = new Board();

            Vec2 direction = new Vec2 { x = -1, y = -1 };
            BoardTestCheckForPatternMethod(ref board, direction, board.Win, Piece.P2, board.CheckForWin);
        }

        [TestMethod]
        public void BoardCheckForWin_NorthEaast_P2_Test()
        {
            Board board = new Board();

            Vec2 direction = new Vec2 { x = 1, y = -1 };
            BoardTestCheckForPatternMethod(ref board, direction, board.Win, Piece.P2, board.CheckForWin);
        }

        [TestMethod]
        public void BoardCheckForWin_SouthWest_P2_Test()
        {
            Board board = new Board();

            Vec2 direction = new Vec2 { x = -1, y = 1 };
            BoardTestCheckForPatternMethod(ref board, direction, board.Win, Piece.P2, board.CheckForWin);
        }
        #endregion

        #region Vec2 and Extension Tests
        [TestMethod]
        public void Vec2_Addition()
        {
            Vec2 v1 = new Vec2(1, 2);
            Vec2 v2 = new Vec2(3, 4);

            Vec2 sum = v1 + v2;

            Assert.AreEqual(4, sum.x);
            Assert.AreEqual(6, sum.y);
        }

        [TestMethod]
        public void Vec2_Subtraction()
        {
            Vec2 v1 = new Vec2(1, 2);
            Vec2 v2 = new Vec2(3, 4);

            Vec2 difference = v1 - v2;

            Assert.AreEqual(-2, difference.x);
            Assert.AreEqual(-2, difference.y);
        }

        [TestMethod]
        public void Vec2_Multiplication()
        {
            Vec2 v1 = new Vec2(1, 2);

            Vec2 product = v1 * 2;

            Assert.AreEqual(2, product.x);
            Assert.AreEqual(4, product.y);
        }

        [TestMethod]
        public void Vec2_Division()
        {
            Vec2 v1 = new Vec2(2, 4);

            Vec2 product = v1 / 2;

            Assert.AreEqual(1, product.x);
            Assert.AreEqual(2, product.y);
        }

        [TestMethod]
        public void Vec2_Negative()
        {
            Vec2 v1 = new Vec2(1, 2);

            Vec2 result = -v1;

            Assert.AreEqual(-1, result.x);
            Assert.AreEqual(-2, result.y);
        }

        [TestMethod]
        public void Vec2_ClampMin()
        {
            Vec2 v1 = new Vec2(1, 5);

            Vec2 result = v1.Clamp(2, 6);

            Assert.AreEqual(2, result.x);
            Assert.AreEqual(5, result.y);
        }

        [TestMethod]
        public void Vec2_ClampMax()
        {
            Vec2 v1 = new Vec2(1, 5);

            Vec2 result = v1.Clamp(0, 4);

            Assert.AreEqual(1, result.x);
            Assert.AreEqual(4, result.y);
        }

        [TestMethod]
        public void PieceGetPattern()
        {
            Piece[] pieces = new Piece[5];
            Piece[] test = new Piece[5];
            for (int i = 0; i < pieces.Length; i++)
            {
                pieces[i] = Piece.P1;
                test[i] = Piece.P2;
            }

            pieces = pieces.GetPatternFor(Piece.P2);

            for(int i = 0; i < pieces.Length; i++)
            {
                Assert.AreEqual(test[i], pieces[i]);
            }

        }

        [TestMethod]
        public void PieceGetPatternOther()
        {
            Piece[] pieces = new Piece[5];
            Piece[] test = new Piece[5];
            for (int i = 0; i < pieces.Length; i++)
            {
                pieces[i] = Piece.P1;
                test[i] = Piece.P1;
            }

            pieces = pieces.GetPatternFor(Piece.P1);

            for (int i = 0; i < pieces.Length; i++)
            {
                Assert.AreEqual(test[i], pieces[i]);
            }
        }

        [TestMethod]
        public void PieceMult()
        {
            Piece pieces = Piece.P1;
            Piece test = Piece.P2;

pieces =             pieces.Mult(Piece.P2);

            Assert.AreEqual(test, pieces);

        }

        [TestMethod]
        public void PieceMultOther()
        {
            Piece pieces = Piece.P1;
            Piece test = Piece.P1;

            pieces = pieces.Mult(Piece.P1);

            Assert.AreEqual(test, pieces);

        }

        #endregion
    }
}