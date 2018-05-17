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
using System.Windows.Shapes;

namespace Pente
{
    /// <summary>
    /// Interaction logic for ModeSelect.xaml
    /// </summary>
    public partial class ModeSelect : Window
    {
        MainWindow parent;

        public ModeSelect(MainWindow window)
        {
            parent = window;
            InitializeComponent();
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            parent.Visibility = Visibility.Visible;
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
                Manager.instance.p1.name = "Player 1";
            }
            else
            {
                Manager.instance.p1.name = txtboxP1.Text;
            }
            if (txtboxP2.Text  == "")
            {
                Manager.instance.p2.name = "Player 2";
            }
            else if (txtboxP2.IsEnabled == false)
            {
                Manager.instance.p2.name = "Computer Player";
            }
            else
            {
                Manager.instance.p2.name = txtboxP2.Text;
            }

            parent.NewGame();
            this.Hide();
        }
    }
}
