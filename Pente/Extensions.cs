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
    }
}
