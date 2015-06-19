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
            foreach (Task task in tasks) {
                Console.WriteLine(task.name);
            }
        }

        public void UtilizeState(object state)
        {
        }
    }
}
