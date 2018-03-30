using System;
using System.Windows;
using System.Linq;
using InventoryApp.Models;

namespace InventoryApp
{
    /// <summary>
    /// Interaction logic for ContactWindow.xaml
    /// </summary>
    public partial class ManufacturerWindow : Window
    {
        public MfgModel Manufacturer { get; set; }

        public ManufacturerWindow()
        {
            InitializeComponent();

            // Don't show this window in the task bar
            ShowInTaskbar = false;
        }

        public ManufacturerWindow(int MfgID)
        {
            InitializeComponent();

            // Don't show this window in the task bar
            ShowInTaskbar = false;
            if (MfgID > 0)
            {
                var manufacturer = App.InventoryRepository.GetAllManufacturers().First(n => n.Id == MfgID);

                uxMfgId.Text                 = manufacturer.Id.ToString();
                uxMfgName.Text               = manufacturer.Name;
                uxMfgOurAbbreviation.Text    = manufacturer.OurAbbreviation;
                uxMfgParentCorp.Text         = manufacturer.ParentCorp.ToString();
                uxMfgDefaultCategory.Text    = manufacturer.DefaultCategory.ToString();
                uxMfgDefaultGoodsOrigin.Text = manufacturer.DefaultGoodsOrigin;
                uxMfgSourceOfOriginInfo.Text = manufacturer.SourceOfOriginInfo.ToString();
                uxMfgWebSite.Text            = manufacturer.WebSite;
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
            if (Manufacturer != null)
            {
                uxSubmit.Content = "Update";
            }
            else
            {
                Manufacturer = new MfgModel();
            }

            uxMfgGrid.DataContext = Manufacturer;
        }
    }
}
