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
    /// Логика взаимодействия для DisciplineList.xaml
    /// </summary>
    public partial class DisciplineList : Page
    {
        bool isInverted = false;
        bool isRemovedShowed = false;
        public DisciplineList()
        {
            InitializeComponent();

            SortByComboBox.SelectedIndex = 0;

            BottomBar.Visibility = App.isAdmin ? Visibility.Visible : Visibility.Collapsed;

            RefreshFilters();
        }

        private void RefreshFilters()
        {
            IEnumerable<Discipline> list = isRemovedShowed ?
                App.db.Discipline.Where(x => x.IsRemoved == true).ToList()
                :
                App.db.Discipline.Where(x => x.IsRemoved == false).ToList();


            if (isInverted == false && SortByComboBox.SelectedIndex != 0)
                switch (SortByComboBox.SelectedIndex)
                {
                    case 1:
                        list = list.OrderBy(x => x.DName).ToList();
                        break;
                    case 2:
                        list = list.OrderBy(x => x.Workload).ToList();
                        break;
                    case 3:
                        list = list.OrderBy(x => x.SpecialityCount).ToList();
                        break;
                }
            else if (isInverted == true && SortByComboBox.SelectedIndex != 0)
                switch (SortByComboBox.SelectedIndex)
                {
                    case 1:
                        list = list.OrderByDescending(x => x.DName).ToList();
                        break;
                    case 2:
                        list = list.OrderByDescending(x => x.Workload).ToList();
                        break;
                    case 3:
                        list = list.OrderByDescending(x => x.SpecialityCount).ToList();
                        break;
                }

            if (SearchbarText.Text != "")
                list = list.Where(x =>
                x.DName.ToLower().Contains(SearchbarText.Text.ToLower()) ||
                x.Workload.ToString().ToLower().Contains(SearchbarText.Text.ToLower()) ||
                x.SpecialityCount.ToString().ToLower().Contains(SearchbarText.Text.ToLower())
                ).ToList();

            DisciplineListView.ItemsSource = null;
            DisciplineListView.ItemsSource = list;

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

        private void AddElementButton_Click(object sender, RoutedEventArgs e)
        {
            AppNav.Navigate(new PageComps("Добавление", new EditDiscipline(new Discipline())));
        }

        private void EditElementButton_Click(object sender, RoutedEventArgs e)
        {
            if (DisciplineListView.SelectedItem != null)
                AppNav.Navigate(new PageComps("Редактирование", new EditDiscipline(DisciplineListView.SelectedItem as Discipline)));
            else
                MessageBox.Show("Элемент не выбран");
        }

        private void RemoveElementButton_Click(object sender, RoutedEventArgs e)
        {
            if (DisciplineListView.SelectedItem != null)
            {
                (DisciplineListView.SelectedItem as Discipline).IsRemoved = true;
                App.db.SaveChanges();
                RefreshFilters();
            }
            else
                MessageBox.Show("Элемент не выбран");
        }

        private void RestoreElementButton_Click(object sender, RoutedEventArgs e)
        {
            (DisciplineListView.SelectedItem as Discipline).IsRemoved = false;
            App.db.SaveChanges();
            RefreshFilters();
        }

        private void ShowRemovedAppoints_Click(object sender, RoutedEventArgs e)
        {

            isRemovedShowed = !isRemovedShowed;

            RefreshFilters();

            if (isRemovedShowed == true)
            {
                ShowRemovedText.Text = "Актуальный список";
                App.mainWindow.TitleText.Text = "Удаленные элементы";
            }
            else
            {
                ShowRemovedText.Text = "Удаленные элементы";
                App.mainWindow.TitleText.Text = "Дисциплины";
            }

            AddElementButton.Visibility = isRemovedShowed ? Visibility.Collapsed : Visibility.Visible;
            EditElementButton.Visibility = isRemovedShowed ? Visibility.Collapsed : Visibility.Visible;
            RemoveElementButton.Visibility = isRemovedShowed ? Visibility.Collapsed : Visibility.Visible;
            RestoreElementButton.Visibility = isRemovedShowed ? Visibility.Visible : Visibility.Collapsed;
        }
    }
}
