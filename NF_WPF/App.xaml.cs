using NF_WPF.Data;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace NF_WPF
{
    /// <summary>
    /// Логика взаимодействия для App.xaml
    /// </summary>
    public partial class App : Application
    {
        //public static NF_WPF_Entities db = new NF_WPF_Entities();
        public static NF_WPF_HOME_Entities db = new NF_WPF_HOME_Entities();
        public static bool isAdmin = true;
        public static MainWindow mainWindow;
    }
}
