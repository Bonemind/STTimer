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
using NHotkey.Wpf;

namespace STTimer.Windows
{
    /// <summary>
    /// Interaction logic for TaskTrackWindow.xaml
    /// </summary>
    public partial class TaskTrackWindow : UserControl, ISwitchable
    {
        /// <summary>
        /// The task we are trackjing
        /// </summary>
        private STTimer.ApiObjects.Task task;

        /// <summary>
        /// Timer to use
        /// </summary>
        private DispatcherTimer timer;

        /// <summary>
        /// The total elapsed time
        /// </summary>
        private int elapsed;

        /// <summary>
        /// Returnes the total elapsed ms
        /// </summary>
        public int timeElapsedMs
        {
            get
            {
                return this.elapsed;
            }
        }

        /// <summary>
        /// Default constructor
        /// </summary>
        public TaskTrackWindow()
        {
            elapsed = 0;
            InitializeComponent();
            timer = new DispatcherTimer();
            timer.Interval = new TimeSpan(0, 0, 1);
            timer.Tick += timer_Tick;
            try
            {
                HotkeyManager.Current.AddOrReplace("TimerStartStop", Key.F12, ModifierKeys.Control, toggleTimer);
            }
            catch (NHotkey.HotkeyAlreadyRegisteredException ex)
            {
                MessageBox.Show("The toggle hotkey is already registered by another application, toggleing will not work");
            }
            timerLabel.Content = formatTime(elapsed);
        }

        /// <summary>
        /// Handles toggleing of a time via hotkey
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void toggleTimer(object sender, EventArgs e)
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

        /// <summary>
        /// Handles the tick of a timer
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void timer_Tick(object sender, EventArgs e)
        {
            if (task == null)
            {
                return;
            }
            elapsed += 60;
            timerLabel.Content = formatTime(elapsed);
        }

        /// <summary>
        /// Implements ISwitchable
        /// We need to know which task we are tracking
        /// </summary>
        /// <param name="state">The task to track</param>
        public void UtilizeState(object state)
        {
            task = (STTimer.ApiObjects.Task)state;
            taskName.Content = task.name;
        }

        /// <summary>
        /// Formats the elapsed time to a human readable format
        /// </summary>
        /// <param name="seconds">The number of seconds that have elapsed</param>
        /// <returns>String in the format HH:MM</returns>
        private string formatTime(int seconds)
        {
            string hours = "0" + Math.Floor((double)seconds / 3600);
            string minutes = "0" + seconds % 3600 / 60;
            return String.Format("{0}:{1}", hours.Substring(hours.Length -2), minutes.Substring(minutes.Length - 2));
        }

        /// <summary>
        /// Toggles the timer
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void pauseButton_Click(object sender, RoutedEventArgs e)
        {
            toggleTimer(null, null);
        }

        /// <summary>
        /// Saves the elapsed time to the sever
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            //Stop the timer so we stay at the time at the moment the user wants to save
            if (timer.IsEnabled)
            {
                timer.Stop();
            }

            //Lets the user check and optionally edit how much time he has spent on a task
            TimeConfirmDialog confirm = new TimeConfirmDialog("Please check your time", formatTime(elapsed));
            if (confirm.ShowDialog() == true)
            {
                Console.WriteLine("Confirmed");
                string time = confirm.Answer;
                saveEffort(time);
            }
        }

        /// <summary>
        /// Persists the time to the server
        /// </summary>
        /// <param name="time">The formatted time string in format HH:MM to persist</param>
        private void saveEffort(string time)
        {
            //Parse the time to hours and minutes
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

            //Turn the hours and minuts into a double (5 hours and 30 minuts becomes 5.5)
            double effort = hours + (minutes / 60);

            //Save the time
            ApiWrapper.Instance.saveTaskEffort(task, effort);
        }

        /// <summary>
        /// Switches back to the tasklist window
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void backButton_Click(object sender, RoutedEventArgs e)
        {
            Switcher.Switch(new TaskListWindow());
        }
    }
}
