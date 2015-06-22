using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace STTimer
{
    /// <summary>
    /// Implements ICommand to allow us to minimize the window through the notifyicon
    /// </summary>
    class MinMaxCommand : ICommand
    {
        /// <summary>
        /// The window to minimize
        /// </summary>
        private System.Windows.Window minMaxWindow;

        /// <summary>
        /// Default constructor
        /// </summary>
        /// <param name="window">The window to minimize</param>
        public MinMaxCommand(System.Windows.Window window)
        {
            minMaxWindow = window;
        }

        /// <summary>
        /// Whether the command can be executed
        /// Always true
        /// </summary>
        /// <param name="parameter"></param>
        /// <returns></returns>
        public bool CanExecute(object parameter)
        {
            return true;
        }

        /// <summary>
        /// Eventhandler
        /// </summary>
        public event EventHandler CanExecuteChanged;

        /// <summary>
        /// Actually shows and hides the window
        /// </summary>
        /// <param name="parameter"></param>
        public void Execute(object parameter)
        {
            if (minMaxWindow.IsVisible) 
            { 
                minMaxWindow.Hide();
            }
            else
            {
                minMaxWindow.Show();
            }
        }
    }
}
