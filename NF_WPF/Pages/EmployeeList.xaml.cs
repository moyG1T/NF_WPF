using NF_WPF.Pages.UserControls;
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
    /// Логика взаимодействия для EmployeeList.xaml
    /// </summary>
    public partial class EmployeeList : Page
    {
        bool isInverted = false;
        bool isRemovedShowed = false;
        public EmployeeList()
        {
            InitializeComponent();

            SortByComboBox.SelectedIndex = 0;
            BottomBar.Visibility = App.isAdmin || App.isLecturer ? Visibility.Visible : Visibility.Collapsed;

            ShowRemovedAppointsButton.Visibility = App.isAdmin ? Visibility.Visible : Visibility.Collapsed;


            foreach (var item in App.db.Employee)
            {
                EmployeeWrapPanel.Children.Add(new EmployeeUserControl(item));
            }
        }

        private void RefreshFilters()
        {

        }

        private void SortByComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void InvertSortByButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void SearchbarText_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void ClearSearchbarButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void ClearFiltersButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void AddElementButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void EditElementButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void RemoveElementButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void ShowRemovedAppoints_Click(object sender, RoutedEventArgs e)
        {

        }

        private void RestoreElementButton_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
