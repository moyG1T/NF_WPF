using NF_WPF.Data;
using NF_WPF.NavHost;
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
    /// Логика взаимодействия для EditDiscipline.xaml
    /// </summary>
    public partial class EditDiscipline : Page
    {
        private Discipline discipline;
        public EditDiscipline(Discipline discipline)
        {
            InitializeComponent();
            this.discipline = discipline;

            DataContext = this.discipline;
        }

        private void WorkloadBox_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            if (!char.IsDigit(e.Text, 0))
            {
                e.Handled = true;
            }
        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            StringBuilder err = new StringBuilder();

            if (DisciplineBox.Text == "")
                err.AppendLine("Дисциплина пуста");
            if (WorkloadBox.Text == "")
                err.AppendLine("Объем не указан");

            if (err.Length > 0)
            {
                MessageBox.Show(err.ToString());
            }
            else
            {
                if (discipline.Id_disc == 0)
                {
                    App.db.Discipline.Add(new Discipline()
                    {
                        DName = DisciplineBox.Text,
                        Workload = int.Parse(WorkloadBox.Text),
                        IsRemoved = false,
                    });
                    App.db.SaveChanges();
                    AppNav.Navigate(new PageComps("Дисциплины", new DisciplineList()));
                    MessageBox.Show("Добавлено");
                }
                else
                {
                    App.db.SaveChanges();
                    AppNav.Navigate(new PageComps("Дисциплины", new DisciplineList()));
                    MessageBox.Show("Сохранено");
                }
            }
        }
    }
}
