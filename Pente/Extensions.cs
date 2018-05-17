using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pente
{
    public static class Extensions
    {
        public static Board.Piece[] GetPatternFor(this Board.Piece[] pattern, Board.Piece playerPiece)
        {
            Board.Piece[] newPattern = new Board.Piece[pattern.Length];
            pattern.CopyTo(newPattern, 0);

            for (int i = 0; i < newPattern.Length; i++)
            {
                newPattern[i] = newPattern[i].Mult(playerPiece);
            }

            return newPattern;
        }

        public static Board.Piece Mult(this Board.Piece left, Board.Piece right)
        {
            return (Board.Piece)((int)left * (int)right);
        }

        public static Board.Piece Get(this Board.Piece[,] Grid, Board.Vec2 position)
        {
            if (position.x >= 0 && position.x < Grid.GetLength(0) && position.y >= 0 && position.y < Grid.GetLength(1))
            {
                return Grid[position.x, position.y];
            }

            return Board.Piece.EMPTY;
        }
    }
}
