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
using RestSharp;
using STTimer.ApiObjects;

namespace STTimer.Windows
{
    /// <summary>
    /// Interaction logic for Login.xaml
    /// </summary>
    public partial class LoginWindow : UserControl, ISwitchable
    {
        /// <summary>
        /// Default constructor
        /// </summary>
        public LoginWindow()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Implementation of ISWitchable
        /// LoginWindow does not need a state, so ignored
        /// </summary>
        /// <param name="state">The state passed to this view</param>
        void ISwitchable.UtilizeState(object state)
        {
            //Void
        }

        /// <summary>
        /// Tries to login the user
        /// Switches to the tasklist on success, and displays an error otherwise
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void loginButton_Click(object sender, RoutedEventArgs e)
        {
            ApiWrapper.Instance.Login(userBox.Text, passwordBox.Password);
            if (ApiWrapper.Instance.isLoggedIn())
            {
                Switcher.Switch(new TaskListWindow());
            }
            else
            {
                MessageBox.Show("Invalid username or password");
            }
        }
    }
}
