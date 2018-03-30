using System.ComponentModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Media;
using InventoryApp.Models;

namespace InventoryApp
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private GridViewColumnHeader listViewSortCol = null;
        private SortAdorner          listViewSortAdorner = null;
        private MfgModel             selectedManufacturer;
        private CategoryModel        selectedCategory;
        private ProductModel         selectedProduct;

        public MainWindow()
        {
            InitializeComponent();

            LoadManufacturers();
            LoadCategories();
            LoadProducts();
        }

        private void LoadManufacturers()
        {
            var mfgs = App.InventoryRepository.GetAllManufacturers();

            uxMfgList.ItemsSource = mfgs
                .Select(t => MfgModel.ToModel(t))
                .ToList();
        }

        private void LoadCategories()
        {
            var cats = App.InventoryRepository.GetAllCategories();

            uxCategoryList.ItemsSource = cats
                .Select(t => CategoryModel.ToModel(t))
                .ToList();
        }

        private void LoadProducts()
        {
            var prods = App.InventoryRepository.GetAllProducts();

            uxProductList.ItemsSource = prods
                .Select(t => ProductModel.ToModel(t))
                .ToList();
        }

        private void uxMfgNew_Click(object sender, RoutedEventArgs e)
        {
            var window = new ManufacturerWindow();

            if (window.ShowDialog() == true)
            {
                var uiModel = window.Manufacturer;
                var repositoryModel = uiModel.ToRepositoryModel();
                App.InventoryRepository.Add(repositoryModel);
                LoadManufacturers();
            }
        }

        private void uxCategoryNew_Click(object sender, RoutedEventArgs e)
        {
            var window = new CategoryWindow();

            if (true == window.ShowDialog())
            {
                var uiModel = window.Category;
                var repositoryModel = uiModel.ToRepositoryModel();
                App.InventoryRepository.Add(repositoryModel);
                LoadCategories();
            }
        }

        private void uxProductNew_Click(object sender, RoutedEventArgs e)
        {
            var window = new ProductWindow();

            if (true == window.ShowDialog())
            {
                var uiModel = window.Product;
                var repositoryModel = uiModel.ToRepositoryModel();
                App.InventoryRepository.Add(repositoryModel);
                LoadProducts();
            }
        }

        private void uxMfgList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            selectedManufacturer = (MfgModel)uxMfgList.SelectedValue;
            // This event doesn't catch the use of the keyboard's context menu key if haven't first clicked a selection
        }

        private void uxCategoryList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            selectedCategory = (CategoryModel)uxCategoryList.SelectedValue;
        }

        private void uxProductList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            selectedProduct = (ProductModel)uxProductList.SelectedValue;
        }

        private void uxMfgChange_Click(object sender, RoutedEventArgs e)
        {
            var window = new ManufacturerWindow();
            window.Manufacturer = selectedManufacturer.Clone();

            if (true == window.ShowDialog())
            {
                App.InventoryRepository.Update(window.Manufacturer.ToRepositoryModel());
                LoadManufacturers();
            }
        }

        private void uxCategoryChange_Click(object sender, RoutedEventArgs e)
        {
            var window = new CategoryWindow();
            window.Category = selectedCategory.Clone();

            if (true == window.ShowDialog())
            {
                App.InventoryRepository.Update(window.Category.ToRepositoryModel());
                LoadCategories();
            }
        }

        private void uxProductChange_Click(object sender, RoutedEventArgs e)
        {
            var window = new ProductWindow();
            window.Product = selectedProduct.Clone();

            if (true == window.ShowDialog())
            {
                App.InventoryRepository.Update(window.Product.ToRepositoryModel());
                LoadProducts();
            }
        }

        //private void uxCategoryChange_Click(object sender, System.Windows.Input.MouseButtonEventArgs e)
        //{
        //    uxCategoryChange_Click(sender, RoutedEventArgs e);
        //}

        private void uxMfgList_MouseDoubleClick(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            uxMfgChange_Click(sender, e);
        }

        private void uxMfgDelete_Click(object sender, RoutedEventArgs e)
        {
            App.InventoryRepository.RemoveMfg(selectedManufacturer.Id);
            selectedManufacturer = null;
            LoadManufacturers();
        }

        private void uxCategoryDelete_Click(object sender, RoutedEventArgs e)
        {
            App.InventoryRepository.RemoveCategory(selectedCategory.Id);
            selectedCategory = null;
            LoadCategories();
        }

        private void uxProductDelete_Click(object sender, RoutedEventArgs e)
        {
            App.InventoryRepository.RemoveProduct(selectedProduct.Id);
            selectedProduct = null;
            LoadProducts();
        }

        private void uxMfgDelete_Loaded(object sender, RoutedEventArgs e)
        {
            uxFileDelete.IsEnabled = uxMfgDelete.IsEnabled = (selectedManufacturer != null);
        }

        private void uxCategoryDelete_Loaded(object sender, RoutedEventArgs e)
        {
            uxCategoryDelete.IsEnabled = (selectedCategory != null);
        }

        private void uxProductDelete_Loaded(object sender, RoutedEventArgs e)
        {
            uxProductDelete.IsEnabled = (selectedProduct != null);
        }

        private void uxMfgChange_Loaded(object sender, RoutedEventArgs e)
        {
            uxFileChange.IsEnabled = uxMfgChange.IsEnabled = (selectedManufacturer != null);
        }

        private void uxCategoryChange_Loaded(object sender, RoutedEventArgs e)
        {
            uxFileChange.IsEnabled = uxCategoryChange.IsEnabled = (selectedCategory!= null);
        }

        private void uxProductChange_Loaded(object sender, RoutedEventArgs e)
        {
            uxFileChange.IsEnabled = uxProductChange.IsEnabled = (selectedProduct!= null);
        }

        private void uxMfgListColumnHeader_Click(object sender, RoutedEventArgs e)
        {
            GridViewColumnHeader column = (sender as GridViewColumnHeader);
            string sortBy = column.Tag.ToString();
            if (listViewSortCol != null)
            {
                AdornerLayer.GetAdornerLayer(listViewSortCol).Remove(listViewSortAdorner);
                uxMfgList.Items.SortDescriptions.Clear();
            }

            ListSortDirection newDir = ListSortDirection.Ascending;
            if (listViewSortCol == column && listViewSortAdorner.Direction == newDir)
                newDir = ListSortDirection.Descending;

            listViewSortCol = column;
            listViewSortAdorner = new SortAdorner(listViewSortCol, newDir);
            AdornerLayer.GetAdornerLayer(listViewSortCol).Add(listViewSortAdorner);
            uxMfgList.Items.SortDescriptions.Add(new SortDescription(sortBy, newDir));
        }

        private void uxCategoryListColumnHeader_Click(object sender, RoutedEventArgs e)
        {
            GridViewColumnHeader column = (sender as GridViewColumnHeader);
            string sortBy = column.Tag.ToString();
            if (listViewSortCol != null)
            {
                AdornerLayer.GetAdornerLayer(listViewSortCol).Remove(listViewSortAdorner);
                uxCategoryList.Items.SortDescriptions.Clear();
            }

            ListSortDirection newDir = ListSortDirection.Ascending;
            if (listViewSortCol == column && listViewSortAdorner.Direction == newDir)
                newDir = ListSortDirection.Descending;

            listViewSortCol = column;
            listViewSortAdorner = new SortAdorner(listViewSortCol, newDir);
            AdornerLayer.GetAdornerLayer(listViewSortCol).Add(listViewSortAdorner);
            uxCategoryList.Items.SortDescriptions.Add(new SortDescription(sortBy, newDir));
        }

        private void uxProductListColumnHeader_Click(object sender, RoutedEventArgs e)
        {
            GridViewColumnHeader column = (sender as GridViewColumnHeader);
            string sortBy = column.Tag.ToString();
            if (listViewSortCol != null)
            {
                AdornerLayer.GetAdornerLayer(listViewSortCol).Remove(listViewSortAdorner);
                uxProductList.Items.SortDescriptions.Clear();
            }

            ListSortDirection newDir = ListSortDirection.Ascending;
            if (listViewSortCol == column && listViewSortAdorner.Direction == newDir)
                newDir = ListSortDirection.Descending;

            listViewSortCol = column;
            listViewSortAdorner = new SortAdorner(listViewSortCol, newDir);
            AdornerLayer.GetAdornerLayer(listViewSortCol).Add(listViewSortAdorner);
            uxProductList.Items.SortDescriptions.Add(new SortDescription(sortBy, newDir));
        }

        public class SortAdorner : Adorner
        {
            private static Geometry ascGeometry =
                    Geometry.Parse("M 0 4 L 3.5 0 L 7 4 Z");

            private static Geometry descGeometry =
                    Geometry.Parse("M 0 0 L 3.5 4 L 7 0 Z");

            public ListSortDirection Direction { get; private set; }

            public SortAdorner(UIElement element, ListSortDirection dir)
                    : base(element)
            {
                this.Direction = dir;
            }

            protected override void OnRender(DrawingContext drawingContext)
            {
                base.OnRender(drawingContext);

                if (AdornedElement.RenderSize.Width < 20)
                    return;

                TranslateTransform transform = new TranslateTransform
                        (
                                AdornedElement.RenderSize.Width - 15,
                                (AdornedElement.RenderSize.Height - 5) / 2
                        );
                drawingContext.PushTransform(transform);

                Geometry geometry = ascGeometry;
                if (this.Direction == ListSortDirection.Descending)
                    geometry = descGeometry;
                drawingContext.DrawGeometry(Brushes.Black, null, geometry);

                drawingContext.Pop();
            }
        }

        private void uxFileExit_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
