using NF_WPF.NavHost;
using NF_WPF.Pages;
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

namespace NF_WPF
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            App.mainWindow = this;
            AppNav.Navigate(new PageComps("Авторизация", new Authorization()));
        }

        private void PopButton_Click(object sender, RoutedEventArgs e)
        {
            AppNav.NavigateAndPop();
        }

        private void QuitAsAdminButton_Click(object sender, RoutedEventArgs e)
        {
            AppNav.DropHistory();
            AppNav.Navigate(new PageComps("Авторизация", new Authorization()));
        }
    }
}
