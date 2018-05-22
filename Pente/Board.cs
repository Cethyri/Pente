using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pente
{
    [Serializable]
    public enum Piece
    {
        P1 = 1,
        P2 = -1,
        EMPTY = 0
    }

    [Serializable]
    public class Board
    {
        // multiply by P2 to get P2Version
        public readonly Piece[] Capture = { Piece.P1, Piece.P2, Piece.P2, Piece.P1 };
        public readonly Piece[] Tria = { Piece.EMPTY, Piece.P1, Piece.P1, Piece.P1, Piece.EMPTY };
        public readonly Piece[] Tessera = { Piece.EMPTY, Piece.P1, Piece.P1, Piece.P1, Piece.P1 };
        public readonly Piece[] Win = { Piece.P1, Piece.P1, Piece.P1, Piece.P1, Piece.P1 };

        public readonly Vec2[] directions = { new Vec2(1, 0), new Vec2(0, 1), new Vec2(1, 1), new Vec2(1, -1) };

        public Piece[,] Grid;

        public Player p1;
        public Player p2;

        public int turnCount = 0;
        public Piece currentPlayerPiece { get { return (turnCount % 2 == 0) ? Piece.P1 : Piece.P2; } }

        /// <summary>
        /// Initializes the board to empty with p1 piece in the center, initializes players, initializes playerPiece to P1
        /// </summary>
        /// <param name="p1"></param>
        /// <param name="p2"></param>
        public void Initialize(Player player1, Player player2, int size)
        {
            Grid = new Piece[size, size];
            p1 = player1;
            p2 = player2;
            Grid[size / 2, size / 2] = Piece.P1;
            turnCount = 1;
        }

        /// <summary>
        /// checks validity of placement
        /// </summary>
        /// <param name="position"></param>
        public bool IsValidPlacement(Vec2 position)
        {
            if (turnCount == 2)
            {
                Vec2 relativePosition = position - (new Vec2(Grid.GetLength(0), Grid.GetLength(1)) / 2);

                if (relativePosition.x < 3 && relativePosition.x > -3 &&
                    relativePosition.y < 3 && relativePosition.y > -3) return false;
            }

            return Grid.Get(position).Equals(Piece.EMPTY);
        }

        /// <summary>
        /// Placce Piece on board, Check all Patterns, switch currentPlayerPiece, if currentPlayerPiece is P2 and p2 is type computer call PlacePiece(p2.takeTurn())
        /// </summary>
        /// <param name="position"></param>
        public void PlacePiece(Vec2 position)
        {
            if (IsValidPlacement(position))
            {
                Grid[position.x, position.y] = currentPlayerPiece;
                turnCount++;
            }
        }

        /// <summary>
        /// CheckForPattern(Win) or currentPlayer captures >= 5
        /// </summary>
        /// <returns></returns>
        public bool CheckForWin(Vec2 position)
        {
            Vec2 nothing = new Vec2();
            if (CheckForPattern(position, Win, true, ref nothing, ref nothing))
            {
                return true;
            }
            if (p1.Captures >= 5 || p2.Captures >=5)
            {
                return true;
            }

            return false;
        }

        /// <summary>
        /// CheckForPattern(Capture) and then remove pieces if capture exists
        /// </summary>
        public bool CheckForCapture(Vec2 position)
        {
            Vec2 start = new Vec2();
            Vec2 direction = new Vec2();
            
            if (CheckForPattern(position, Capture, false, ref start, ref direction))
            {
                start += direction;
                Grid.Set(start, Piece.EMPTY);// = Piece.EMPTY;
                start += direction;
                Grid.Set(start, Piece.EMPTY);// = Piece.EMPTY;
                //Grid[position.x, position.y] = Piece.EMPTY;
                return true;
            }
            return false;
        }

        public bool CheckForPattern(Vec2 position, Piece[] pattern, bool isSymetrical)
        {
            Vec2 temp = new Vec2();
            return CheckForPattern(position, pattern, isSymetrical, ref temp, ref temp);
        }

        /// <summary>
        /// checks for pattern in the row, column, and both diagonals of the placed piece
        /// </summary>
        /// <param name="pattern"></param>
        /// <param name="startOfPattern"></param>
        /// <param name="foundDirection"></param>
        /// <returns>bool if the pattern was found</returns>
        public bool CheckForPattern(Vec2 position, Piece[] pattern, bool isSymetrical, ref Vec2 startOfPattern, ref Vec2 foundDirection)
        {
            bool hasFoundPattern = false;
            foreach (Vec2 direction in directions)
            {
                if (hasFoundPattern = CheckForPatternInDirection(position, pattern, ref startOfPattern, direction))
                {
                    foundDirection = direction;
                    break;
                }
            }

            if (!isSymetrical && !hasFoundPattern)
                foreach (Vec2 direction in directions)
                {
                    if (hasFoundPattern = CheckForPatternInDirection(position, pattern, ref startOfPattern, -direction))
                    {
                        foundDirection = -direction;
                        break;
                    }
                }

            return hasFoundPattern;
        }

        private bool CheckForPatternInDirection(Vec2 position, Piece[] pattern, ref Vec2 startOfPattern, Vec2 direction)
        {
            Vec2 startPosition = position - (direction * (pattern.Length - 1));
            Vec2 endPosition = position + (direction * (pattern.Length - 1));

            startPosition.Clamp(0, Grid.GetLength(0));
            endPosition.Clamp(0, Grid.GetLength(0));

            int startValue = Math.Abs(startPosition.x - position.x) > Math.Abs(startPosition.y - position.y) ? (startPosition.x - position.x) * direction.x: (startPosition.y - position.y) * direction.y;
            int endValue = Math.Abs(endPosition.x - position.x) > Math.Abs(endPosition.y - position.y) ? (endPosition.x - position.x) * direction.x : (endPosition.y - position.y) * direction.y;

            Vec2 positionToCheck;

            bool hasFoundPattern = false;

            for (int shift = startValue; shift <= endValue; shift++)
            {
                hasFoundPattern = true;

                startOfPattern = position + (direction * shift);

                for (int i = 0; i < pattern.Length; i++)
                {
                    positionToCheck = position + (direction * (shift + i));

                    if (Grid.Get(positionToCheck) != pattern[i])
                    {
                        hasFoundPattern = false;
                        break;
                    }
                }

                if (hasFoundPattern)
                {
                    break;
                }
            }

            return hasFoundPattern;
        }

        public void Save()
        {

        }

        public void Load()
        {

        }
    }
}
