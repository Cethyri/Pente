using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pente
{
    public class Board
    {
        // multiply by P2 to get P2Version
        public readonly Piece[] Capture = { Piece.P1, Piece.P2, Piece.P2, Piece.P1 };
        public readonly Piece[] Tria = { Piece.EMPTY, Piece.P1, Piece.P1, Piece.P1, Piece.EMPTY };
        public readonly Piece[] Tessera = { Piece.EMPTY, Piece.P1, Piece.P1, Piece.P1, Piece.P1 };
        public readonly Piece[] Win = { Piece.P1, Piece.P1, Piece.P1, Piece.P1, Piece.P1 };

        public Piece[,] Grid = new Piece[19,19];

        public Player p1;
        public Player p2;

        public Piece currentPlayerPiece = Piece.P1;

        public struct Vec2
        {
            public int x;
            public int y;
        }

        public enum Piece
        {
            P1 = 1,
            P2 = -1,
            EMPTY = 0
        }

        /// <summary>
        /// Initializes the board to empty, initializes players, initializes playerPiece to P1
        /// </summary>
        /// <param name="p1"></param>
        /// <param name="p2"></param>
        public void Initialize(Player p1, Player p2)
        {

        }

        /// <summary>
        /// checks validity of placement
        /// </summary>
        /// <param name="position"></param>
        public bool IsValidPlacement(Vec2 position)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Placce Piece on board, Check all Patterns, switch currentPlayerPiece, if currentPlayerPiece is P2 and p2 is type computer call PlacePiece(p2.takeTurn())
        /// </summary>
        /// <param name="position"></param>
        public void PlacePiece(Vec2 position)
        {

        }

        /// <summary>
        /// CheckForPattern(Win) or currentPlayer captures >= 5
        /// </summary>
        /// <returns></returns>
        public bool CheckForWin(Vec2 position)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// CheckForPattern(Capture) and then remove pieces if capture exists
        /// </summary>
        public bool CheckForCapture(Vec2 position)
        {
            throw new NotImplementedException();
        }
        
        /// <summary>
        /// checks for pattern in the row, column, and both diagonals of the placed piece
        /// </summary>
        /// <param name="pattern"></param>
        /// <param name="start"></param>
        /// <param name="end"></param>
        /// <returns>bool if the pattern was found</returns>
        public bool CheckForPattern(Vec2 position, Piece[] pattern, bool isSymetrical, ref Vec2 start, ref Vec2 end)
        {
            throw new NotImplementedException();
        }
    }
}
