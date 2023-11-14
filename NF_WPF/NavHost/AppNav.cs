using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

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
            App.mainWindow.QuitAsAdminButton.Visibility = App.isAdmin ? Visibility.Visible : Visibility.Collapsed;
            App.mainWindow.PopButton.Visibility = history.Count > 1 ? Visibility.Visible : Visibility.Collapsed;
        }

        public static void Navigate(PageComps page)
        {
            history.Add(page);
            Refresh(page);
        }
        public static void NavigateAndPop()
        {
            if (history.Count >= 2)
            {
                history.RemoveAt(history.Count - 1);
                Refresh(history[history.Count-1]);
            }
        }
        public static void DropHistory()
        {
            App.isAdmin = false;
            history.Clear();
        }
    }
}
