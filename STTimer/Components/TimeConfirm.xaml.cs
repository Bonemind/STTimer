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
using System.Windows.Shapes;

namespace STTimer.Components
{
    /// <summary>
    /// Confirmation window for the time spent on a task
    /// </summary>
    public partial class TimeConfirmDialog : Window
    {
        /// <summary>
        /// Default constructor
        /// </summary>
        /// <param name="question">The question to pose to the user</param>
        /// <param name="answer">The answer the user gave</param>
        public TimeConfirmDialog(string question, string answer)
        {
            InitializeComponent( );
            lblQuestion.Content = question;
            txtAnswer.Text = answer;
        }

        /// <summary>
        /// Confirms the time in the dialog
        /// </summary>
        /// <param name="sender">The ok button used to trigger the confirmation</param>
        /// <param name="e">The event params</param>
        private void btnDialogOk_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = true;
        }

        /// <summary>
        /// Called after the contents of the dialog are rendered
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Window_ContentRendered(object sender, EventArgs e)
        {
            txtAnswer.SelectAll();
            txtAnswer.Focus();
        }

        /// <summary>
        /// Answer property to be able to get the time a user has spent
        /// </summary>
        public string Answer
        {
            get { return txtAnswer.Text; }
        }
    }
}
