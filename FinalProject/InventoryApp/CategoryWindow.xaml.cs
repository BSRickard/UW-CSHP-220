using System;
using System.Windows;
using System.Linq;
using InventoryApp.Models;

namespace InventoryApp
{
    /// <summary>
    /// Interaction logic for ContactWindow.xaml
    /// </summary>
    public partial class CategoryWindow : Window
    {
        public CategoryModel Category { get; set; }

        public CategoryWindow()
        {
            InitializeComponent();

            // Don't show this window in the task bar
            ShowInTaskbar = false;
        }

        public CategoryWindow(int CatID)
        {
            InitializeComponent();

            // Don't show this window in the task bar
            ShowInTaskbar = false;
            if (CatID > 0)
            {
                var category = App.InventoryRepository.GetAllCategories().First(n => n.Id == CatID);

                uxCategoryId.Text          = category.Id.ToString();
                uxCategoryParent.Text      = App.InventoryRepository.GetAllCategories()
                                                .First(n => n.Id == category.Parent).Description;
                uxCategoryDescription.Text = category.Description;
            }
        }

        private void uxCatSubmit_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = true;
            Close();
        }

        private void uxCatCancel_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
            Close();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            if (Category != null)
            {
                uxCatSubmit.Content = "Update";
            }
            else
            {
                Category = new CategoryModel();
            }

            uxCategoryGrid.DataContext = Category;
        }
    }
}
