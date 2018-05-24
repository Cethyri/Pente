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
    /// Interaction logic for ModePage.xaml
    /// </summary>
    public partial class ModePage : Page
    {

        public ModePage()
        {
            InitializeComponent();
        }
        private void btnPVP_Click(object sender, RoutedEventArgs e)
        {
            txtboxP2.IsEnabled = true;
            Manager.instance.p2 = new Player();
        }

        private void btnPVE_Click(object sender, RoutedEventArgs e)
        {
            txtboxP2.IsEnabled = false;
            txtboxP2.Text = "Player 2";
            Manager.instance.p2 = new Computer();
        }

        private void Submit_Click(object sender, RoutedEventArgs e)
        {
            if (txtboxP1.Text == "")
            {
                Manager.instance.p1.Name = "Player 1";
            }
            else
            {
                Manager.instance.p1.Name = txtboxP1.Text;
            }
            if (txtboxP2.Text == "")
            {
                Manager.instance.p2.Name = "Player 2";
            }
            else if (txtboxP2.IsEnabled == false)
            {
                Manager.instance.p2.Name = "Computer Player";
            }
            else
            {
                Manager.instance.p2.Name = txtboxP2.Text;
            }

            Manager.instance.p1.Captures = 0;
            Manager.instance.p2.Captures = 0;

            Manager.instance.size = (int)sldr.Value;

            Manager.instance.board = new Board();
            Manager.instance.board.Initialize(Manager.instance.p1, Manager.instance.p2, Manager.instance.size);
            this.NavigationService.Navigate(new Uri("/GamePente.xaml", UriKind.Relative));
        }
    }

}
