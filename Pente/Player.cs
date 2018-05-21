using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pente
{
    [Serializable]
    public class Player
    {
        int captures;
        string name;

        public int Captures { get { return captures; } set { captures = value; } }
        public string Name { get { return name; } set { name = value; } }
    }
}
