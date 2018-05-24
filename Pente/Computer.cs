using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pente
{
    [Serializable]
    public class Computer : Player
    {
        public Vec2 TakeTurn()
        {
            //bool looper = false;
            Random rng = new Random();
            //int x = -1;
            //int y = -1;
            //while (looper == false)
            //{
            //    x = rng.Next(0, Manager.instance.size);
            //    y = rng.Next(0, Manager.instance.size);
            //    looper = Manager.instance.board.IsValidPlacement(new Vec2(x, y));
            //}

            List<Vec2> possiblePositions = new List<Vec2>();

            Vec2 position;

            for (int i = 0; i < Manager.instance.size; i++)
            {
                for (int j = 0; j < Manager.instance.size; j++)
                {
                    position = new Vec2(i, j);
                    if (Manager.instance.board.IsValidPlacement(position))
                    {
                        possiblePositions.Add(position);
                    }
                }
            }

            return possiblePositions[rng.Next(0, possiblePositions.Count)];


        }
    }
}
