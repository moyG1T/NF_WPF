﻿using NF_WPF.Data;
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
        //public static HomeEntities db = new HomeEntities();
        public static NF_WPF_PC2_Entities db = new NF_WPF_PC2_Entities();
        public static bool isAdmin = false;
        public static bool isLecturer = false;
        public static bool isStudent = false;
        public static int userId;
        public static MainWindow mainWindow;
    }
}
