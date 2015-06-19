using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace STTimer
{
    /// <summary>
    /// Static switcher class
    /// Provides a central location that all pages can use to switch
    /// </summary>
    public static class Switcher
    {
        /// <summary>
        /// The main pageSwitcher window
        /// </summary>
        public static  PageSwitcher pageSwitcher;

        /// <summary>
        /// Actually switches to a page
        /// </summary>
        /// <param name="newPage">The page to switch to</param>
        public static void Switch(UserControl newPage) 
        {
            pageSwitcher.Navigate(newPage);
        }

        /// <summary>
        /// Actually switches to a page
        /// </summary>
        /// <param name="newPage">The page to switch to</param>
        /// <param name="state">The state object to pass to the new page</param>
        public static void Switch(UserControl newPage, object state)
        {
            pageSwitcher.Navigate(newPage, state);

        }
    }
}
