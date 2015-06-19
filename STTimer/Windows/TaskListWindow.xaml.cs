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
        public TaskListWindow()
        {
            InitializeComponent();
            User currUser = ApiWrapper.Instance.getUser();
            List<STTimer.ApiObjects.Task> tasks = ApiWrapper.Instance.getTasks();
            tasks.RemoveAll(x => x.owner != currUser.id);
            tasks.RemoveAll(x => x.status == "DONE");
            taskListView.ItemsSource = tasks;
        }

        public void UtilizeState(object state)
        {
        }

        private void taskListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Task task = ((ListView) sender).SelectedItem as Task;
            if (task == null)
            {
                return;
            }

            Console.WriteLine(task.id);
        }

        private void ListViewItem_MouseUp(object sender, MouseButtonEventArgs e)
        {

            Task task = ((ListView) sender).SelectedItem as Task;
            if (task == null)
            {
                return;
            }
            Console.WriteLine(task.id);
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (taskListView.SelectedItem == null)
            {
                MessageBox.Show("Please select a task to track");
            }
            Switcher.Switch(new TaskTrackWindow(), taskListView.SelectedItem);
        }
    }
}
