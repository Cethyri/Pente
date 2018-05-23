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
    /// Interaction logic for MainPage.xaml
    /// </summary>
    public partial class MainPage : Page
    {
        //	HowToPlay howToPlayWindow;
        public MainPage()
        {
            InitializeComponent();
            Manager.CreateNewManager();

        }



        public void NewGame()
        {
       //     gameWindow = new GameWindow(this);
        //    gameWindow.Show();
        }

        //PopupWindow for mode, Select mode, true/false for mode select, popup name window, open actual game window
        public void Start()
        {
            //Do we still need this?
        }

        public void Quit()
        {
            Application.Current.Shutdown();
        }

        private void btnPlay_Click(object sender, RoutedEventArgs e)
        {
            this.NavigationService.Navigate(new Uri("/ModePage.xaml", UriKind.Relative));
        }

        private void btnHowToPlay_Click(object sender, RoutedEventArgs e)
        {
            System.Diagnostics.Process.Start("https://www.pente.net/instructions.html");
        }

        private void btnExit_Click(object sender, RoutedEventArgs e)
        {
            Quit();
        }

        private void btnLoad_Click(object sender, RoutedEventArgs e)
        {
            Manager.instance.board = (Board)DataPersistence.Serializer.LoadFromFile();
            if (Manager.instance.board != null)
            {
                Manager.instance.p1 = Manager.instance.board.p1;
                Manager.instance.p2 = Manager.instance.board.p2;
                Manager.instance.size = Manager.instance.board.Grid.GetLength(0);

                this.NavigationService.Navigate(new Uri("/GamePente.xaml", UriKind.Relative));
            }
        }
    }
}
