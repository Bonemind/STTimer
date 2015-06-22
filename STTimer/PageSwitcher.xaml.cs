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
using STTimer.Windows;
using Hardcodet.Wpf.TaskbarNotification;
using System.Drawing;

namespace STTimer
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class PageSwitcher : Window
    {
        private TaskbarIcon taskBarIcon;
        private MinMaxCommand command;
        /// <summary>
        /// Default constructor
        /// </summary>
        public PageSwitcher()
        {
            InitializeComponent();
            //Setup the pageswitcher
            Switcher.pageSwitcher = this;
            //Switch to our main window
            Switcher.Switch(new LoginWindow());
            taskBarIcon = new TaskbarIcon();
            command = new MinMaxCommand(this);
            taskBarIcon.Icon = STTimer.Properties.Resources.timerIcon;
            taskBarIcon.ToolTipText = "Hello World!";
            taskBarIcon.MouseLeftButtonUp += taskBarIcon_MouseLeftButtonUp;
            taskBarIcon.MouseRightButtonDown += taskBarIcon_MouseLeftButtonUp;
            taskBarIcon.DoubleClickCommand = command;
            this.ResizeMode = ResizeMode.NoResize;
        }

        void taskBarIcon_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            Console.WriteLine("Callback");
            if (this.WindowState == WindowState.Minimized)
            {
                this.WindowState = WindowState.Normal;
            }
            else
            {
                this.WindowState = WindowState.Minimized;
            }
        }

        /// <summary>
        /// Navigates to a page
        /// </summary>
        /// <param name="nextPage">The page to navigate to</param>
        public void Navigate(UserControl nextPage)
        {
            this.Content = nextPage;
        }

        /// <summary>
        /// Switches to a page with a state
        /// </summary>
        /// <param name="nextPage">The page to navigate to</param>
        /// <param name="state">The state to pass along</param>
        public void Navigate(UserControl nextPage, object state)
        {
            this.Content = nextPage;
            ISwitchable s = nextPage as ISwitchable;

            if (s != null) {
                s.UtilizeState(state);
            }
            else
            {
                throw new ArgumentException("NextPage is not ISwitchable! " + nextPage.Name.ToString());
            }
        }

    }
}
