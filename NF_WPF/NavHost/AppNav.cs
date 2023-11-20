using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;

namespace NF_WPF.NavHost
{
    internal class AppNav
    {
        private static List<PageComps> history = new List<PageComps>();

        private static void Refresh(PageComps page)
        {
            App.mainWindow.MainWindowFrame.Navigate(page.Page);
            App.mainWindow.TitleText.Text = page.Title;
            App.mainWindow.ModeText.Visibility = App.isAdmin ? Visibility.Visible : Visibility.Collapsed;
            //App.mainWindow.ProfileButton.Visibility = App.isAdmin || App.isLecturer || App.isStudent ? Visibility.Visible : Visibility.Collapsed;
            //App.mainWindow.QuitButton.Visibility = App.isAdmin || App.isLecturer || App.isStudent ? Visibility.Visible : Visibility.Collapsed;
            App.mainWindow.ProfileButton.IsEnabled = false;
            App.mainWindow.ProfileButton.IsEnabled = App.isLecturer || App.isStudent ? true : false;
            App.mainWindow.QuitButton.IsEnabled = App.isAdmin || App.isLecturer || App.isStudent ? true : false;
            App.mainWindow.PopButton.IsEnabled = history.Count >= 3 ? true : false;
        }

        public static void Navigate(PageComps page)
        {
            history.Add(page);
            Refresh(page);
        }
        public static void NavigateAndPop()
        {
            if (history.Count >= 3)
            {
                history.RemoveAt(history.Count - 1);
                Refresh(history[history.Count-1]);
            }
        }
        public static void DropHistory()
        {
            App.isAdmin = false;
            App.isLecturer = false;
            App.isStudent = false;
            history.Clear();
        }
    }
}
