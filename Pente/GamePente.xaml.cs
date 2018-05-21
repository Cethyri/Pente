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
using System.Windows.Threading;

namespace Pente
{
    /// <summary>
    /// Interaction logic for GamePente.xaml
    /// </summary>
    public partial class GamePente : Page
    {
        int timerF = 20;
        string time = "20";

        DispatcherTimer dispatcherTimer;




        public GamePente()
        {
            InitializeComponent();
            BoardControl.InitializeBoard();
            Player1.SetPlayer(ref Manager.instance.p1);
            Player2.SetPlayer(ref Manager.instance.p2);

            dispatcherTimer = new DispatcherTimer();
            dispatcherTimer.Tick += new EventHandler(dispatcherTimer_Tick);
            dispatcherTimer.Interval = new TimeSpan(0, 0, 1);
            dispatcherTimer.Start();

            time += "s";
            Timer.DataContext = time;
        }

        private void dispatcherTimer_Tick(object sender, EventArgs e)
        {
            timerF -= dispatcherTimer.Interval.Seconds;
            if (timerF <= -1) ResetClock();

            time = timerF.ToString();
            time = string.Format("{0:0.#}", time);

            time += "s";
            Timer.DataContext = time;

        }

        public void ResetClock()
        {
            timerF = 20;
            //swap turns

        }

        private void btnMainMenu_Click(object sender, RoutedEventArgs e)
        {
            this.NavigationService.Navigate(new Uri("/MainPage.xaml", UriKind.Relative));

        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
