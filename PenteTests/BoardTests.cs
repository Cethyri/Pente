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

            board.Initialize(p1, p2);

            Assert.IsTrue((p1 == board.p1) && (p2 == board.p2));
        }

        [TestMethod]
        public void BoardInitialize_Grid_Test()
        {
            Board board = new Board();
            Player p1 = new Player();
            Player p2 = new Player();

            board.Initialize(p1, p2);

            for (int i = 0; i < board.Grid.GetLength(0); i++)
            {
                for (int j = 0; j < board.Grid.GetLength(1); j++)
                {
                    Assert.AreEqual(Piece.EMPTY, board.Grid[i, j]);
                }
            }
        }

        [TestMethod]
        public void BoardInitialize_Grid_ReInit_Test()
        {
            Board board = new Board();
            Player p1 = new Player();
            Player p2 = new Player();

            board.Grid.Set(start, Piece.P1);

            board.Initialize(p1, p2);

            for (int i = 0; i < board.Grid.GetLength(0); i++)
            {
                for (int j = 0; j < board.Grid.GetLength(1); j++)
                {
                    Assert.AreEqual(Piece.EMPTY, board.Grid[i, j]);
                }
            }
        }
        #endregion

        #region Capture Checks
        public delegate bool CheckForDelegate(Vec2 position);

        public bool BoardTestCheckForPatternMethod(ref Board board, Vec2 direction, Piece[] pattern, Piece playerPiece, CheckForDelegate checkForDelegate)
        {
            Player p1 = new Player();
            Player p2 = new Player();

            pattern = pattern.GetPatternFor(playerPiece);

            board.Initialize(p1, p2);
            board.currentPlayerPiece = playerPiece;

            for (int i = 0; i < pattern.Length; i++)
            {
                board.Grid.Set(start + direction * i, pattern[i]);
            }

            return checkForDelegate(start);
        }
        
        private void PatternExists(Board board, Vec2 direction, Piece[] patternToFind)
        {
            for (int i = 0; i < patternToFind.Length; i++)
            {
                Assert.AreEqual(patternToFind[i], board.Grid.Get(start + direction * i));
            }
        }

        [TestMethod]
        public void BoardCheckForCapture_Vertical_P1_Test()
        {
            Board board = new Board();

            //forwards
            Vec2 direction = new Vec2 { x = 0, y = 1 };
            BoardTestCheckForPatternMethod(ref board, direction, board.Capture, Piece.P1, board.CheckForCapture);

            PatternExists(board, direction, successfulCapture);

            //backwards
            BoardTestCheckForPatternMethod(ref board, -direction, board.Capture, Piece.P1, board.CheckForCapture);

            PatternExists(board, direction, successfulCapture);
        }

        [TestMethod]
        public void BoardCheckForCapture_Horizontal_P1_Test()
        {
            Board board = new Board();

            //forwards
            Vec2 direction = new Vec2 { x = 1, y = 0 };
            BoardTestCheckForPatternMethod(ref board, direction, board.Capture, Piece.P1, board.CheckForCapture);

            PatternExists(board, direction, successfulCapture);

            //backwards
            BoardTestCheckForPatternMethod(ref board, -direction, board.Capture, Piece.P1, board.CheckForCapture);

            PatternExists(board, direction, successfulCapture);
        }

        [TestMethod]
        public void BoardCheckForCapture_DiagonalDown_P1_Test()
        {
            Board board = new Board();

            //forwards
            Vec2 direction = new Vec2 { x = 1, y = 1 };
            BoardTestCheckForPatternMethod(ref board, direction, board.Capture, Piece.P1, board.CheckForCapture);

            PatternExists(board, direction, successfulCapture);

            //backwards
            BoardTestCheckForPatternMethod(ref board, -direction, board.Capture, Piece.P1, board.CheckForCapture);

            PatternExists(board, direction, successfulCapture);
        }

        [TestMethod]
        public void BoardCheckForCapture_DiagonalUp_P1_Test()
        {
            Board board = new Board();

            //forwards
            Vec2 direction = new Vec2 { x = 1, y = -1 };
            BoardTestCheckForPatternMethod(ref board, direction, board.Capture, Piece.P1, board.CheckForCapture);

            PatternExists(board, direction, successfulCapture);

            //backwards
            BoardTestCheckForPatternMethod(ref board, -direction, board.Capture, Piece.P1, board.CheckForCapture);

            PatternExists(board, direction, successfulCapture);
        }

        [TestMethod]
        public void BoardCheckForCapture_Vertical_P2_Test()
        {
            Board board = new Board();

            //forwards
            Vec2 direction = new Vec2 { x = 0, y = 1 };
            BoardTestCheckForPatternMethod(ref board, direction, board.Capture, Piece.P2, board.CheckForCapture);

            PatternExists(board, direction, successfulCapture);

            //backwards
            BoardTestCheckForPatternMethod(ref board, -direction, board.Capture, Piece.P2, board.CheckForCapture);

            PatternExists(board, direction, successfulCapture);
        }

        [TestMethod]
        public void BoardCheckForCapture_Horizontal_P2_Test()
        {
            Board board = new Board();

            //forwards
            Vec2 direction = new Vec2 { x = 1, y = 0 };
            BoardTestCheckForPatternMethod(ref board, direction, board.Capture, Piece.P2, board.CheckForCapture);

            PatternExists(board, direction, successfulCapture);

            //backwards
            BoardTestCheckForPatternMethod(ref board, -direction, board.Capture, Piece.P2, board.CheckForCapture);

            PatternExists(board, direction, successfulCapture);
        }

        [TestMethod]
        public void BoardCheckForCapture_DiagonalDown_P2_Test()
        {
            Board board = new Board();

            //forwards
            Vec2 direction = new Vec2 { x = 1, y = 1 };
            BoardTestCheckForPatternMethod(ref board, direction, board.Capture, Piece.P2, board.CheckForCapture);

            PatternExists(board, direction, successfulCapture);

            //backwards
            BoardTestCheckForPatternMethod(ref board, -direction, board.Capture, Piece.P2, board.CheckForCapture);

            PatternExists(board, direction, successfulCapture);
        }

        [TestMethod]
        public void BoardCheckForCapture_DiagonalUp_P2_Test()
        {
            Board board = new Board();

            //forwards
            Vec2 direction = new Vec2 { x = 1, y = -1 };

            BoardTestCheckForPatternMethod(ref board, direction, board.Capture, Piece.P2, board.CheckForCapture);

            PatternExists(board, direction, successfulCapture);

            //backwards
            BoardTestCheckForPatternMethod(ref board, -direction, board.Capture, Piece.P2, board.CheckForCapture);

            PatternExists(board, direction, successfulCapture);
        }
        #endregion

        #region Win Checks
        [TestMethod]
        public void BoardCheckForWin_FiveCaptures_P1_Test()
        {
            Board board = new Board();
            Player p1 = new Player();
            Player p2 = new Player();

            board.Initialize(p1, p2);
            board.p1.captures = 5;

            Assert.IsTrue(board.CheckForWin(start));
        }

        [TestMethod]
        public void BoardCheckForWin_FiveCaptures_P2_Test()
        {
            Board board = new Board();
            Player p1 = new Player();
            Player p2 = new Player();

            board.Initialize(p1, p2);
            board.currentPlayerPiece = Piece.P2;
            board.p2.captures = 5;

            Assert.IsTrue(board.CheckForWin(start));
        }

        [TestMethod]
        public void BoardCheckForWin_Vertical_P1_Test()
        {
            Board board = new Board();

            //forwards
            Vec2 direction = new Vec2 { x = 0, y = 1 };
            BoardTestCheckForPatternMethod(ref board, direction, board.Win, Piece.P1, board.CheckForWin);

            //backwards
            BoardTestCheckForPatternMethod(ref board, -direction, board.Win, Piece.P1, board.CheckForWin);
        }

        [TestMethod]
        public void BoardCheckForWin_Horizontal_P1_Test()
        {
            Board board = new Board();

            //forwards
            Vec2 direction = new Vec2 { x = 1, y = 0 };
            BoardTestCheckForPatternMethod(ref board, direction, board.Win, Piece.P1, board.CheckForWin);

            //backwards
            BoardTestCheckForPatternMethod(ref board, -direction, board.Win, Piece.P1, board.CheckForWin);
        }

        [TestMethod]
        public void BoardCheckForWin_DiagonalDown_P1_Test()
        {
            Board board = new Board();

            //forwards
            Vec2 direction = new Vec2 { x = 1, y = 1 };
            BoardTestCheckForPatternMethod(ref board, direction, board.Win, Piece.P1, board.CheckForWin);

            //backwards
            BoardTestCheckForPatternMethod(ref board, -direction, board.Win, Piece.P1, board.CheckForWin);
        }

        [TestMethod]
        public void BoardCheckForWin_DiagonalUp_P1_Test()
        {
            Board board = new Board();

            //forwards
            Vec2 direction = new Vec2 { x = 1, y = -1 };
            BoardTestCheckForPatternMethod(ref board, direction, board.Win, Piece.P1, board.CheckForWin);

            //backwards
            BoardTestCheckForPatternMethod(ref board, -direction, board.Win, Piece.P1, board.CheckForWin);
        }

        [TestMethod]
        public void BoardCheckForWin_Vertical_P2_Test()
        {
            Board board = new Board();

            //forwards
            Vec2 direction = new Vec2 { x = 0, y = 1 };
            BoardTestCheckForPatternMethod(ref board, direction, board.Win, Piece.P2, board.CheckForWin);

            //backwards
            BoardTestCheckForPatternMethod(ref board, -direction, board.Win, Piece.P2, board.CheckForWin);
        }

        [TestMethod]
        public void BoardCheckForWin_Horizontal_P2_Test()
        {
            Board board = new Board();

            //forwards
            Vec2 direction = new Vec2 { x = 1, y = 0 };
            BoardTestCheckForPatternMethod(ref board, direction, board.Win, Piece.P2, board.CheckForWin);

            //backwards
            BoardTestCheckForPatternMethod(ref board, -direction, board.Win, Piece.P2, board.CheckForWin);
        }

        [TestMethod]
        public void BoardCheckForWin_DiagonalDown_P2_Test()
        {
            Board board = new Board();

            //forwards
            Vec2 direction = new Vec2 { x = 1, y = 1 };
            BoardTestCheckForPatternMethod(ref board, direction, board.Win, Piece.P2, board.CheckForWin);

            //backwards
            BoardTestCheckForPatternMethod(ref board, -direction, board.Win, Piece.P2, board.CheckForWin);
        }

        [TestMethod]
        public void BoardCheckForWin_DiagonalUp_P2_Test()
        {
            Board board = new Board();

            //forwards
            Vec2 direction = new Vec2(1, -1);
            BoardTestCheckForPatternMethod(ref board, direction, board.Win, Piece.P2, board.CheckForWin);

            //backwards
            BoardTestCheckForPatternMethod(ref board, -direction, board.Win, Piece.P2, board.CheckForWin);
        }
        #endregion
    }
}