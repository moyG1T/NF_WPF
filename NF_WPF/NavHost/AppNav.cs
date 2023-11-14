using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NF_WPF.NavHost
{
    internal class AppNav
    {
        private static List<PageComps> history = new List<PageComps>();

        private static void Refresh(PageComps page)
        {
            App.mainWindow.MainWindowFrame.Navigate(page.Page);
            App.mainWindow.TitleText.Text = page.Title;
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
