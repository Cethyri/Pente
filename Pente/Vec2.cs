using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pente
{
    [Serializable]
    public struct Vec2
    {
        public int x;
        public int y;

        public Vec2(int x, int y)
        {
            this.x = x;
            this.y = y;
        }

        public static Vec2 operator* (Vec2 left, int right)
        {
            return new Vec2(left.x * right, left.y * right);
        }

        public static Vec2 operator+ (Vec2 left, Vec2 right)
        {
            return new Vec2(left.x + right.x, left.y + right.y);
        }

        public static Vec2 operator -(Vec2 left, Vec2 right)
        {
            return new Vec2(left.x - right.x, left.y - right.y);
        }

        public static Vec2 operator -(Vec2 right)
        {
            return new Vec2(- right.x, - right.y);
        }

        public Vec2 Clamp (int min, int max)
        {
            return new Vec2(x.Clamp(min, max), y.Clamp(min, max));
        }
    }
}
