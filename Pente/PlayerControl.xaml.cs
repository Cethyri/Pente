using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Pente
{
    /// <summary>
    /// Interaction logic for PlayerControl.xaml
    /// </summary>
    public partial class PlayerControl : UserControl
    {
        Player player;
        public PlayerControl()
        {
            InitializeComponent();
            name1.DataContext = player;
            captures1.DataContext = player;
        }

        public void SetPlayer(ref Player p)
        {
            if (p == null) return;
            player = p;
            name1.DataContext = player;
            captures1.DataContext = player;
        }
    }
}
