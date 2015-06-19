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
using STTimer.Components;

namespace STTimer.Windows
{
    /// <summary>
    /// Interaction logic for TaskTrackWindow.xaml
    /// </summary>
    public partial class TaskTrackWindow : UserControl, ISwitchable
    {
        private STTimer.ApiObjects.Task task;
        private DispatcherTimer timer;
        private const int TIMER_INTERVAL = 1000;
        private int elapsed;
        public int timeElapsedMs
        {
            get
            {
                return this.elapsed;
            }
        }
        public TaskTrackWindow()
        {
            elapsed = 0;
            InitializeComponent();
            timer = new DispatcherTimer();
            timer.Interval = new TimeSpan(0, 0, 1);
            timer.Tick += timer_Tick;
        }

        void timer_Tick(object sender, EventArgs e)
        {
            if (task == null)
            {
                return;
            }
            elapsed += 60;
            timerLabel.Content = formatTime(elapsed);
        }

        public void UtilizeState(object state)
        {
            task = (STTimer.ApiObjects.Task)state; 
        }

        private string formatTime(int seconds)
        {
            string hours = "0" + Math.Floor((double)seconds / 3600);
            string minutes = "0" + seconds % 3600 / 60;
            return String.Format("{0}:{1}", hours.Substring(hours.Length -2), minutes.Substring(minutes.Length - 2));
        }

        private void pauseButton_Click(object sender, RoutedEventArgs e)
        {
            if (timer.IsEnabled)
            {
                timer.Stop();
                pauseButton.Content = "Start";
            }
            else
            {
                timer.Start();
                pauseButton.Content = "Pause";
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (timer.IsEnabled)
            {
                timer.Stop();
            }
            TimeConfirmDialog confirm = new TimeConfirmDialog("Please check your time", formatTime(elapsed));
            if (confirm.ShowDialog() == true)
            {
                Console.WriteLine("Confirmed");
                string time = confirm.Answer;
                saveEffort(time);
            }
            else 
            {
                Console.WriteLine("Denied");
                return;
            }
            
        }

        private void saveEffort(string time)
        {
            string[] split = time.Split(':');
            if (split.Length != 2)
            {
                MessageBox.Show("That time format is not valid, format: HH:mm");
                return;
            }
            int hours = 0;
            int minutes = 0;
            try
            {
                hours = int.Parse(split[0]);
                minutes = int.Parse(split[1]);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Please only enter numbers in the minute and second portions");
            }
            double effort = hours + (minutes / 60);
            ApiWrapper.Instance.saveTaskEffort(task, effort);
        }
    }
}
