using NF_WPF.Data;
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

namespace NF_WPF.Pages
{
    /// <summary>
    /// Логика взаимодействия для EditExam.xaml
    /// </summary>
    public partial class EditExam : Page
    {
        private Exam exam;
        public EditExam(Exam exam)
        {
            InitializeComponent();

            this.exam = exam;
            DataContext = this.exam;

            DisciplineComboBox.ItemsSource = App.db.Discipline.Distinct().ToList();
            DisciplineComboBox.DisplayMemberPath = "DName";
        }
    }
}
