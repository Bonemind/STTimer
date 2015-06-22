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
        /// <summary>
        /// The taskbaricon to display
        /// </summary>
        private TaskbarIcon taskBarIcon;

        /// <summary>
        /// The command to execute when taskbaricon is doubleclicked
        /// </summary>
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

            //Creates the taskbar icon
            taskBarIcon = new TaskbarIcon();

            //Creates the minmax command
            command = new MinMaxCommand(this);

            //Sets the taskbar icon properties
            taskBarIcon.Icon = STTimer.Properties.Resources.timerIcon;
            taskBarIcon.ToolTipText = "Task Timer";
            taskBarIcon.DoubleClickCommand = command;

            //Remove max and min buttons
            this.ResizeMode = ResizeMode.NoResize;
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
