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
    /// Логика взаимодействия для ExamList.xaml
    /// </summary>
    public partial class ExamList : Page
    {
        bool isInverted = false;
        public ExamList()
        {
            InitializeComponent();
            SortByComboBox.SelectedIndex = 0;
            ExamListView.ItemsSource = App.db.Exam.ToList();
        }

        private void RefreshFilters()
        {
            if (isInverted == false)
            {
                switch (SortByComboBox.SelectedIndex)
                {
                    case 0:
                        ExamListView.ItemsSource = App.db.Exam.ToList();
                        break;
                    case 1:
                        ExamListView.ItemsSource = App.db.Exam.OrderBy(x => x.Id_disc).ToList();
                        break;
                    case 2:
                        ExamListView.ItemsSource = App.db.Exam.OrderBy(x => x.Id_emp).ToList();
                        break;
                    case 3:
                        ExamListView.ItemsSource = App.db.Exam.OrderBy(x => x.Id_stud).ToList();
                        break;
                    case 4:
                        ExamListView.ItemsSource = App.db.Exam.OrderBy(x => x.EDate).ToList();
                        break;
                    case 5:
                        ExamListView.ItemsSource = App.db.Exam.OrderBy(x => x.Auditory).ToList();
                        break;
                    case 6:
                        ExamListView.ItemsSource = App.db.Exam.OrderBy(x => x.Points).ToList();
                        break;
                }

                SearchText();
            }
            else if (isInverted == true)
            {
                switch (SortByComboBox.SelectedIndex)
                {
                    case 0:
                        ExamListView.ItemsSource = App.db.Exam.ToList();
                        break;
                    case 1:
                        ExamListView.ItemsSource = App.db.Exam.OrderByDescending(x => x.Id_disc).ToList();
                        break;
                    case 2:
                        ExamListView.ItemsSource = App.db.Exam.OrderByDescending(x => x.Id_emp).ToList();
                        break;
                    case 3:
                        ExamListView.ItemsSource = App.db.Exam.OrderByDescending(x => x.Id_stud).ToList();
                        break;
                    case 4:
                        ExamListView.ItemsSource = App.db.Exam.OrderByDescending(x => x.EDate).ToList();
                        break;
                    case 5:
                        ExamListView.ItemsSource = App.db.Exam.OrderByDescending(x => x.Auditory).ToList();
                        break;
                    case 6:
                        ExamListView.ItemsSource = App.db.Exam.OrderByDescending(x => x.Points).ToList();
                        break;
                }

                SearchText();
            }
            InvertSortByButton.Visibility = SortByComboBox.SelectedIndex == 0 ? Visibility.Collapsed : Visibility.Visible;
            ClearSearchbarButton.Visibility = SearchbarText.Text == "" ? Visibility.Collapsed : Visibility.Visible;
            ClearFiltersButton.Visibility = SortByComboBox.SelectedIndex == 0 && SearchbarText.Text == "" ? Visibility.Collapsed : Visibility.Visible;
        }

        private void SearchText()
        {
            if (SearchbarText.Text != "")
                ExamListView.ItemsSource = App.db.Exam.Where(x =>
                x.Discipline.DName.ToLower().Contains(SearchbarText.Text.ToLower()) ||
                x.Employee.Surname.ToLower().Contains(SearchbarText.Text.ToLower()) ||
                x.Student.Surname.ToLower().Contains(SearchbarText.Text.ToLower()) ||
                x.Auditory.ToLower().Contains(SearchbarText.Text.ToLower()) ||
                x.Points.ToString().ToLower().Contains(SearchbarText.Text.ToLower())
                ).ToList();
            else if (SearchbarText.Text == "")
                ExamListView.ItemsSource = App.db.Exam.ToList();
        }

        private void SortByComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            RefreshFilters();
        }

        private void SearchbarText_TextChanged(object sender, TextChangedEventArgs e)
        {
            RefreshFilters();
        }

        private void InvertSortByButton_Click(object sender, RoutedEventArgs e)
        {
            isInverted = !isInverted;
            RefreshFilters();
        }

        private void ClearFiltersButton_Click(object sender, RoutedEventArgs e)
        {
            isInverted = false;
            SortByComboBox.SelectedIndex = 0;
            SearchbarText.Text = "";
            RefreshFilters();
        }

        private void ClearSearchbarButton_Click(object sender, RoutedEventArgs e)
        {
            SearchbarText.Text = "";
            RefreshFilters();
        }

        private void EditElementButton_Click(object sender, RoutedEventArgs e)
        {
            AppNav.Navigate(new PageComps("Редактирование", new EditExam(ExamListView.SelectedItem as Exam)));
        }

        private void AddElementButton_Click(object sender, RoutedEventArgs e)
        {
            AppNav.Navigate(new PageComps("Добавление", new EditExam(new Exam())));
        }
    }
}
