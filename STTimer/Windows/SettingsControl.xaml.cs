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

namespace STTimer.Windows
{
    /// <summary>
    /// Allows us to change the baseurl for the application
    /// </summary>
    public partial class SettingsControl : UserControl, ISwitchable
    {
        /// <summary>
        /// Default constructor
        /// </summary>
        public SettingsControl()
        {
            InitializeComponent();
            apiUrl.Text = Properties.Settings.Default.BaseURL;
        }

        /// <summary>
        /// Saves the new url, validates it first
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void saveButton_Click(object sender, RoutedEventArgs e)
        {
            Uri outUri = null;
            bool result = Uri.TryCreate(apiUrl.Text, UriKind.Absolute, out outUri) && outUri.Scheme == Uri.UriSchemeHttp;
            if (!result)
            {
                MessageBox.Show("That is not a valid url");
                return;
            }
            Properties.Settings.Default.BaseURL = apiUrl.Text;
            Properties.Settings.Default.Save();
            ApiWrapper.Instance.destroy();
            Switcher.Switch(new LoginWindow());
        }

        /// <summary>
        /// No op for settings, we don't need a state
        /// </summary>
        /// <param name="state"></param>
        public void UtilizeState(object state)
        {
            //Void
        }
    }
}
