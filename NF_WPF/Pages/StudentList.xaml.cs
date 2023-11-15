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
using NF_WPF.Data;

namespace NF_WPF.Pages
{
    /// <summary>
    /// Логика взаимодействия для StudentList.xaml
    /// </summary>
    public partial class StudentList : Page
    {
        bool isInverted = false;
        public StudentList()
        {
            InitializeComponent();
            SortByComboBox.SelectedIndex = 0;
            ExamListView.ItemsSource = App.db.Student.ToList();
        }
        private void RefreshFilters()
        {
            IEnumerable<Student> list = App.db.Student.ToList();

            if (isInverted == false && SortByComboBox.SelectedIndex != 0)
                switch (SortByComboBox.SelectedIndex)
                {
                    case 1:
                        list = list.OrderBy(x => x.Surname).ToList();
                        break;
                    case 2:
                        list = list.OrderBy(x => x.Speciality.SName).ToList();
                        break;
                }
            else if (isInverted == true && SortByComboBox.SelectedIndex != 0)
                switch (SortByComboBox.SelectedIndex)
                {
                    case 1:
                        list = list.OrderByDescending(x => x.Surname).ToList();
                        break;
                    case 2:
                        list = list.OrderByDescending(x => x.Speciality.SName).ToList();
                        break;
                }

            if (SearchbarText.Text != "")
                list = list.Where(x =>
                x.Surname.ToLower().Contains(SearchbarText.Text.ToLower()) ||
                x.Speciality.SName.ToLower().Contains(SearchbarText.Text.ToLower())
                ).ToList();

            ExamListView.ItemsSource = null;
            ExamListView.ItemsSource = list;
            InvertSortByButton.Visibility = SortByComboBox.SelectedIndex == 0 ? Visibility.Collapsed : Visibility.Visible;
            ClearSearchbarButton.Visibility = SearchbarText.Text == "" ? Visibility.Collapsed : Visibility.Visible;
            ClearFiltersButton.Visibility = SortByComboBox.SelectedIndex == 0 && SearchbarText.Text == "" ? Visibility.Collapsed : Visibility.Visible;
        }

        private void SortByComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            RefreshFilters();
        }

        private void InvertSortByButton_Click(object sender, RoutedEventArgs e)
        {
            isInverted = !isInverted;
            RefreshFilters();
        }

        private void SearchbarText_TextChanged(object sender, TextChangedEventArgs e)
        {
            RefreshFilters();
        }

        private void ClearSearchbarButton_Click(object sender, RoutedEventArgs e)
        {
            SearchbarText.Text = "";
            RefreshFilters();
        }

        private void ClearFiltersButton_Click(object sender, RoutedEventArgs e)
        {
            isInverted = false;
            SearchbarText.Text = "";
            SortByComboBox.SelectedIndex = 0;
            RefreshFilters();
        }
    }
}
