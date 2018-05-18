using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pente
{
    public static class Extensions
    {
        public static Piece[] GetPatternFor(this Piece[] pattern, Piece playerPiece)
        {
            Piece[] newPattern = new Piece[pattern.Length];
            pattern.CopyTo(newPattern, 0);

            for (int i = 0; i < newPattern.Length; i++)
            {
                newPattern[i] = newPattern[i].Mult(playerPiece);
            }

            return newPattern;
        }

        public static Piece Mult(this Piece left, Piece right)
        {
            return (Piece)((int)left * (int)right);
        }

        public static Piece Get(this Piece[,] Grid, Vec2 position)
        {
            if (position.x >= 0 && position.x < Grid.GetLength(0) && position.y >= 0 && position.y < Grid.GetLength(1))
            {
                return Grid[position.x, position.y];
            }

            return Piece.EMPTY;
        }

        public static void Set(this Piece[,] Grid, Vec2 position, Piece piece)
        {
            if (position.x >= 0 && position.x < Grid.GetLength(0) && position.y >= 0 && position.y < Grid.GetLength(1))
            {
                Grid[position.x, position.y] = piece;
            }
        }

        public static int Clamp(this int value, int min, int max)
        {
            return value < min ? min : value > max ? max : value;
        }
    }
}
