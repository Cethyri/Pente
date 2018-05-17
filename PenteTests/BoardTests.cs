using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Pente;

namespace PenteTests
{
    [TestClass]
    public class BoardTests
    {
        private readonly Board.Piece[] successfulCapture = { Board.Piece.P1, Board.Piece.EMPTY, Board.Piece.EMPTY, Board.Piece.P1 };
        private Board.Vec2 start = new Board.Vec2 { x = 9, y = 9 };

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
                    Assert.AreEqual(Board.Piece.EMPTY, board.Grid[i, j]);
                }
            }
        }

        public delegate bool CheckForDelegate(Board.Vec2 position);

        public bool BoardCheckForPattern(ref Board board, Board.Vec2 direction, Board.Piece[] pattern, Board.Piece playerPiece, CheckForDelegate checkForDelegate)
        {
            Player p1 = new Player();
            Player p2 = new Player();

            pattern = pattern.GetPatternFor(playerPiece);

            board.Initialize(p1, p2);
            board.currentPlayerPiece = playerPiece;

            for (int i = 0; i < pattern.Length; i++)
            {
                board.Grid[start.x + direction.x * i, start.y + direction.x * i] = pattern[i];
            }

            return checkForDelegate(start);
        }

        [TestMethod]
        public void BoardCheckForCapture_Vertical_P1_Test()
        {
            Board board = new Board();

            //forwards
            Board.Vec2 direction = new Board.Vec2 { x = 0, y = 1 };
            BoardCheckForPattern(ref board, direction, board.Capture, Board.Piece.P1, board.CheckForCapture);

            for (int i = 0; i < successfulCapture.Length; i++)
            {
                Assert.AreEqual(successfulCapture[i], board.Grid[start.x, start.y + i]);
            }

            //backwards
            direction = new Board.Vec2 { x = 0, y = -1 };
            BoardCheckForPattern(ref board, direction, board.Capture, Board.Piece.P1, board.CheckForCapture);

            for (int i = 0; i < successfulCapture.Length; i++)
            {
                Assert.AreEqual(successfulCapture[i], board.Grid[start.x, start.y - i]);
            }
        }

        [TestMethod]
        public void BoardCheckForCapture_Horizontal_P1_Test()
        {
            Board board = new Board();

            //forwards
            Board.Vec2 direction = new Board.Vec2 { x = 1, y = 0 };
            BoardCheckForPattern(ref board, direction, board.Capture, Board.Piece.P1, board.CheckForCapture);

            for (int i = 0; i < successfulCapture.Length; i++)
            {
                Assert.AreEqual(successfulCapture[i], board.Grid[start.x, start.y + i]);
            }

            //backwards
            direction = new Board.Vec2 { x = -1, y = 0 };
            BoardCheckForPattern(ref board, direction, board.Capture, Board.Piece.P1, board.CheckForCapture);

            for (int i = 0; i < successfulCapture.Length; i++)
            {
                Assert.AreEqual(successfulCapture[i], board.Grid[start.x, start.y - i]);
            }
        }

        [TestMethod]
        public void BoardCheckForCapture_DiagonalDown_P1_Test()
        {
            Board board = new Board();

            //forwards
            Board.Vec2 direction = new Board.Vec2 { x = 1, y = 1 };
            BoardCheckForPattern(ref board, direction, board.Capture, Board.Piece.P1, board.CheckForCapture);

            for (int i = 0; i < successfulCapture.Length; i++)
            {
                Assert.AreEqual(successfulCapture[i], board.Grid[start.x, start.y + i]);
            }

            //backwards
            direction = new Board.Vec2 { x = -1, y = -1 };
            BoardCheckForPattern(ref board, direction, board.Capture, Board.Piece.P1, board.CheckForCapture);

            for (int i = 0; i < successfulCapture.Length; i++)
            {
                Assert.AreEqual(successfulCapture[i], board.Grid[start.x, start.y - i]);
            }
        }

        [TestMethod]
        public void BoardCheckForCapture_DiagonalUp_P1_Test()
        {
            Board board = new Board();

            //forwards
            Board.Vec2 direction = new Board.Vec2 { x = 1, y = -1 };
            BoardCheckForPattern(ref board, direction, board.Capture, Board.Piece.P1, board.CheckForCapture);

            for (int i = 0; i < successfulCapture.Length; i++)
            {
                Assert.AreEqual(successfulCapture[i], board.Grid[start.x, start.y + i]);
            }

            //backwards
            direction = new Board.Vec2 { x = -1, y = 1 };
            BoardCheckForPattern(ref board, direction, board.Capture, Board.Piece.P1, board.CheckForCapture);

            for (int i = 0; i < successfulCapture.Length; i++)
            {
                Assert.AreEqual(successfulCapture[i], board.Grid[start.x, start.y - i]);
            }
        }

        [TestMethod]
        public void BoardCheckForCapture_Vertical_P2_Test()
        {
            Board board = new Board();

            //forwards
            Board.Vec2 direction = new Board.Vec2 { x = 0, y = 1 };
            BoardCheckForPattern(ref board, direction, board.Capture, Board.Piece.P2, board.CheckForCapture);

            for (int i = 0; i < successfulCapture.Length; i++)
            {
                Assert.AreEqual(successfulCapture[i].Mult(Board.Piece.P2), board.Grid[start.x, start.y + i]);
            }

            //backwards
            direction = new Board.Vec2 { x = 0, y = -1 };
            BoardCheckForPattern(ref board, direction, board.Capture, Board.Piece.P2, board.CheckForCapture);

            for (int i = 0; i < successfulCapture.Length; i++)
            {
                Assert.AreEqual(successfulCapture[i].Mult(Board.Piece.P2), board.Grid[start.x, start.y - i]);
            }
        }

        [TestMethod]
        public void BoardCheckForCapture_Horizontal_P2_Test()
        {
            Board board = new Board();

            //forwards
            Board.Vec2 direction = new Board.Vec2 { x = 1, y = 0 };
            BoardCheckForPattern(ref board, direction, board.Capture, Board.Piece.P2, board.CheckForCapture);

            for (int i = 0; i < successfulCapture.Length; i++)
            {
                Assert.AreEqual(successfulCapture[i].Mult(Board.Piece.P2), board.Grid[start.x, start.y + i]);
            }

            //backwards
            direction = new Board.Vec2 { x = -1, y = 0 };
            BoardCheckForPattern(ref board, direction, board.Capture, Board.Piece.P2, board.CheckForCapture);

            for (int i = 0; i < successfulCapture.Length; i++)
            {
                Assert.AreEqual(successfulCapture[i].Mult(Board.Piece.P2), board.Grid[start.x, start.y - i]);
            }
        }

        [TestMethod]
        public void BoardCheckForCapture_DiagonalDown_P2_Test()
        {
            Board board = new Board();

            //forwards
            Board.Vec2 direction = new Board.Vec2 { x = 1, y = 1 };
            BoardCheckForPattern(ref board, direction, board.Capture, Board.Piece.P2, board.CheckForCapture);

            for (int i = 0; i < successfulCapture.Length; i++)
            {
                Assert.AreEqual(successfulCapture[i].Mult(Board.Piece.P2), board.Grid[start.x, start.y + i]);
            }

            //backwards
            direction = new Board.Vec2 { x = -1, y = -1 };
            BoardCheckForPattern(ref board, direction, board.Capture, Board.Piece.P2, board.CheckForCapture);

            for (int i = 0; i < successfulCapture.Length; i++)
            {
                Assert.AreEqual(successfulCapture[i].Mult(Board.Piece.P2), board.Grid[start.x, start.y - i]);
            }
        }

        [TestMethod]
        public void BoardCheckForCapture_DiagonalUp_P2_Test()
        {
            Board board = new Board();

            //forwards
            Board.Vec2 direction = new Board.Vec2 { x = 1, y = -1 };

            BoardCheckForPattern(ref board, direction, board.Capture, Board.Piece.P2, board.CheckForCapture);

            for (int i = 0; i < successfulCapture.Length; i++)
            {
                Assert.AreEqual(successfulCapture[i].Mult(Board.Piece.P2), board.Grid[start.x, start.y + i]);
            }

            //backwards
            direction = new Board.Vec2 { x = -1, y = 1 };
            BoardCheckForPattern(ref board, direction, board.Capture, Board.Piece.P2, board.CheckForCapture);

            for (int i = 0; i < successfulCapture.Length; i++)
            {
                Assert.AreEqual(successfulCapture[i].Mult(Board.Piece.P2), board.Grid[start.x, start.y - i]);
            }
        }

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
            board.currentPlayerPiece = Board.Piece.P2;
            board.p2.captures = 5;

            Assert.IsTrue(board.CheckForWin(start));
        }

        [TestMethod]
        public void BoardCheckForWin_Vertical_P1_Test()
        {
            Board board = new Board();

            //forwards
            Board.Vec2 direction = new Board.Vec2 { x = 0, y = 1 };
            BoardCheckForPattern(ref board, direction, board.Win, Board.Piece.P1, board.CheckForWin);

            //backwards
            direction = new Board.Vec2 { x = 0, y = -1 };
            BoardCheckForPattern(ref board, direction, board.Win, Board.Piece.P1, board.CheckForWin);
        }

        [TestMethod]
        public void BoardCheckForWin_Horizontal_P1_Test()
        {
            Board board = new Board();

            //forwards
            Board.Vec2 direction = new Board.Vec2 { x = 1, y = 0 };
            BoardCheckForPattern(ref board, direction, board.Win, Board.Piece.P1, board.CheckForWin);

            //backwards
            direction = new Board.Vec2 { x = -1, y = 0 };
            BoardCheckForPattern(ref board, direction, board.Win, Board.Piece.P1, board.CheckForWin);
        }

        [TestMethod]
        public void BoardCheckForWin_DiagonalDown_P1_Test()
        {
            Board board = new Board();

            //forwards
            Board.Vec2 direction = new Board.Vec2 { x = 1, y = 1 };
            BoardCheckForPattern(ref board, direction, board.Win, Board.Piece.P1, board.CheckForWin);

            //backwards
            direction = new Board.Vec2 { x = -1, y = -1 };
            BoardCheckForPattern(ref board, direction, board.Win, Board.Piece.P1, board.CheckForWin);
        }

        [TestMethod]
        public void BoardCheckForWin_DiagonalUp_P1_Test()
        {
            Board board = new Board();

            //forwards
            Board.Vec2 direction = new Board.Vec2 { x = 1, y = -1 };
            BoardCheckForPattern(ref board, direction, board.Win, Board.Piece.P1, board.CheckForWin);

            //backwards
            direction = new Board.Vec2 { x = -1, y = 1 };
            BoardCheckForPattern(ref board, direction, board.Win, Board.Piece.P1, board.CheckForWin);
        }

        [TestMethod]
        public void BoardCheckForWin_Vertical_P2_Test()
        {
            Board board = new Board();

            //forwards
            Board.Vec2 direction = new Board.Vec2 { x = 0, y = 1 };
            BoardCheckForPattern(ref board, direction, board.Win, Board.Piece.P2, board.CheckForWin);

            //backwards
            direction = new Board.Vec2 { x = 0, y = -1 };
            BoardCheckForPattern(ref board, direction, board.Win, Board.Piece.P2, board.CheckForWin);
        }

        [TestMethod]
        public void BoardCheckForWin_Horizontal_P2_Test()
        {
            Board board = new Board();

            //forwards
            Board.Vec2 direction = new Board.Vec2 { x = 1, y = 0 };
            BoardCheckForPattern(ref board, direction, board.Win, Board.Piece.P2, board.CheckForWin);

            //backwards
            direction = new Board.Vec2 { x = -1, y = 0 };
            BoardCheckForPattern(ref board, direction, board.Win, Board.Piece.P2, board.CheckForWin);
        }

        [TestMethod]
        public void BoardCheckForWin_DiagonalDown_P2_Test()
        {
            Board board = new Board();

            //forwards
            Board.Vec2 direction = new Board.Vec2 { x = 1, y = 1 };
            BoardCheckForPattern(ref board, direction, board.Win, Board.Piece.P2, board.CheckForWin);

            //backwards
            direction = new Board.Vec2 { x = -1, y = -1 };
            BoardCheckForPattern(ref board, direction, board.Win, Board.Piece.P2, board.CheckForWin);
        }

        [TestMethod]
        public void BoardCheckForWin_DiagonalUp_P2_Test()
        {
            Board board = new Board();

            //forwards
            Board.Vec2 direction = new Board.Vec2 { x = 1, y = -1 };
            BoardCheckForPattern(ref board, direction, board.Win, Board.Piece.P2, board.CheckForWin);

            //backwards
            direction = new Board.Vec2 { x = -1, y = 1 };
            BoardCheckForPattern(ref board, direction, board.Win, Board.Piece.P2, board.CheckForWin);
        }
    }
}
