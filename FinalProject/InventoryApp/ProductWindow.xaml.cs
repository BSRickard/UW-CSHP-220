using System;
using System.Windows;
using System.Linq;
using InventoryApp.Models;

namespace InventoryApp
{
    /// <summary>
    /// Interaction logic for ProductWindow.xaml
    /// </summary>
    public partial class ProductWindow : Window
    {
        public ProductModel Product { get; set; }

        public ProductWindow()
        {
            InitializeComponent();

            // Don't show this window in the task bar
            ShowInTaskbar = false;
        }

        public ProductWindow(int ProductID)
        {
            InitializeComponent();

            // Don't show this window in the task bar
            ShowInTaskbar = false;
            if (ProductID > 0)
            {
                var product = App.InventoryRepository.GetAllProducts().First(n => n.Id == ProductID);

                uxProductId.Text            = product.Id.ToString();
                uxProductPartNumber.Text    = product.OurPartNumber;
                uxProductDescription.Text   = product.Description;
                uxProductStatus.Text        = product.Status.ToString();
                uxProductMfg.Text           = product.Manufacturer.ToString();
                uxProductMfgPartNumber.Text = product.MfgPartNumber;
                uxProductMfgBarCode.Text    = product.MfgBarCode;
                uxProductUnit.Text          = product.Unit;
                uxProductCategory.Text      = product.Category.ToString();
                uxProductOrigin.Text        = product.Origin;
                uxProductOriginSource.Text  = product.OriginSource.ToString();
                uxProductTariff.Text        = product.Tariff;
                uxProductMass.Text          = product.Mass.ToString();
                uxProductVendor1.Text       = product.Vendor1.ToString();
                uxProductPrice1.Text        = product.Price1.ToString();
                uxProductCurrency1.Text     = product.Currency1.ToString();
                uxProductVendor2.Text       = product.Vendor2.ToString();
                uxProductPrice2.Text        = product.Price2.ToString();
                uxProductCurrency2.Text     = product.Currency2.ToString();
                uxProductWebPage.Text       = product.WebPage;
            }
        }

        private void uxSubmit_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = true;
            Close();
        }

        private void uxCancel_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
            Close();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            if (Product != null)
            {
                uxProdSubmit.Content = "Update";
            }
            else
            {
                Product = new ProductModel();
            }

            uxProductGrid.DataContext = Product;
        }
    }
}
