using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pente
{
    class Manager
    {
        public static Manager instance;

        public Player p1;
        public Player p2;

        public int size;
         
        public Board board;

        private Manager()
        {
            instance = this;
            p1 = new Player();
            p2 = new Player();
            board = new Board();
            size = 19;
            
        }

        public static void CreateNewManager()
        {
            if (instance == null)
            {
                Manager manager = new Manager();
            }
        }

    }
}
