using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using STTimer.ApiObjects;

namespace STTimer.Windows
{
    /// <summary>
    /// Interaction logic for TaskListWindow.xaml
    /// </summary>
    public partial class TaskListWindow : UserControl, ISwitchable
    {
        /// <summary>
        /// Default constructor
        /// Fetches the tasks assigned to a user, that are not done
        /// </summary>
        public TaskListWindow()
        {
            InitializeComponent();
            User currUser = ApiWrapper.Instance.getUser();
            List<STTimer.ApiObjects.Task> tasks = ApiWrapper.Instance.getTasks();
            tasks.RemoveAll(x => x.owner != currUser.id);
            tasks.RemoveAll(x => x.status == "DONE");
            taskListView.ItemsSource = tasks;
        }

        /// <summary>
        /// Implements ISwitchable
        /// Tasklist does not need a state, so it is ignored
        /// </summary>
        /// <param name="state">The state to pass</param>
        public void UtilizeState(object state)
        {
            //Void
        }

        /// <summary>
        /// Starts tracking a task
        /// Switches to the TaskTrackWindow and passes the selected task
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (taskListView.SelectedItem == null)
            {
                MessageBox.Show("Please select a task to track");
                return;
            }
            Switcher.Switch(new TaskTrackWindow(), taskListView.SelectedItem);
        }
    }
}
