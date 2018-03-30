using System.Collections.Generic;
using System.Linq;
using InventoryDB;

namespace InventoryRepository
{
    public class ProductModel
    {
        public int      Id { get; set; }
        public string   OurPartNumber { get; set; }
        public string   Description { get; set; }
        public int      Status { get; set; }
        public int      Manufacturer { get; set; }
        public string   MfgPartNumber { get; set; }
        public string   MfgBarCode { get; set; }
        public string   Unit { get; set; }
        public int      Category { get; set; }
        public string   Origin { get; set; }
        public int?     OriginSource { get; set; }
        public string   Tariff { get; set; }
        public float?   Mass { get; set; }
        public int      Vendor1 { get; set; }
        public decimal? Price1 { get; set; }
        public int      Currency1 { get; set; }
        public int?     Vendor2 { get; set; }
        public decimal? Price2 { get; set; }
        public int?     Currency2 { get; set; }
        public string   WebPage { get; set; }
    }

    public partial class InventoryRepository
    {
        public ProductModel Add(ProductModel productModel)
        {
            var productDb = ToDbModel(productModel);

            DatabaseManager.Instance.Products.Add(productDb);
            DatabaseManager.Instance.SaveChanges();

            productModel = new ProductModel
            {
                Id            = productDb.Id,
                OurPartNumber = productDb.OurPartNumber,
                Description   = productDb.Description,
                Status        = productDb.Status,
                Manufacturer  = productDb.Manufacturer,
                MfgPartNumber = productDb.MfgPartNumber,
                MfgBarCode    = productDb.MfgBarCode2D,
                Unit          = productDb.Unit,
                Category      = productDb.Category,
                Origin        = productDb.GoodsOrigin,
                OriginSource  = productDb.SourceOfOriginInfo,
                Tariff        = productDb.Tariff,
                Mass          = productDb.Mass,
                Vendor1       = productDb.VendorOne,
                Price1        = productDb.VendorOnePrice,
                Currency1     = productDb.VendorOneCurrency,
                Vendor2       = productDb.VendorTwo,
                Price2        = productDb.VendorTwoPrice,
                Currency2     = productDb.VendorTwoCurrency,
                WebPage       = productDb.WebPage,
            };
            return productModel;
        }

        public List<ProductModel> GetAllProducts()
        {
            // Use .Select() to map the database contacts to ProductModel
            var items = DatabaseManager.Instance.Products
                .Select(t => new ProductModel
                {
                Id            = t.Id,
                OurPartNumber = t.OurPartNumber,
                Description   = t.Description,
                Status        = t.Status,
                Manufacturer  = t.Manufacturer,
                MfgPartNumber = t.MfgPartNumber,
                MfgBarCode    = t.MfgBarCode2D,
                Unit          = t.Unit,
                Category      = t.Category,
                Origin        = t.GoodsOrigin,
                OriginSource  = t.SourceOfOriginInfo,
                Tariff        = t.Tariff,
                Mass          = t.Mass,
                Vendor1       = t.VendorOne,
                Price1        = t.VendorOnePrice,
                Currency1     = t.VendorOneCurrency,
                Vendor2       = t.VendorTwo,
                Price2        = t.VendorTwoPrice,
                Currency2     = t.VendorTwoCurrency,
                WebPage       = t.WebPage,
                }).ToList();

            return items;
        }

        public bool Update(ProductModel productModel)
        {
            var original = DatabaseManager.Instance.Products.Find(productModel.Id);

            if (original != null)
            {
                DatabaseManager.Instance.Entry(original).CurrentValues.SetValues(ToDbModel(productModel));
                DatabaseManager.Instance.SaveChanges();
            }

            return false;
        }

        public bool RemoveProduct(int productId)
        {
            var items = DatabaseManager.Instance.Products.Where(t => t.Id == productId);

            if (0 == items.Count())
            {
                return false;
            }

            DatabaseManager.Instance.Products.Remove(items.First());
            DatabaseManager.Instance.SaveChanges();

            return true;
        }

        private Product ToDbModel(ProductModel productModel)
        {
            var productDb = new Product
            {
                Id                 = productModel.Id,
                OurPartNumber      = productModel.OurPartNumber,
                Description        = productModel.Description,
                Status             = productModel.Status,
                Manufacturer       = productModel.Manufacturer,
                MfgPartNumber      = productModel.MfgPartNumber,
                MfgBarCode2D       = productModel.MfgBarCode,
                Unit               = productModel.Unit,
                Category           = productModel.Category,
                GoodsOrigin        = productModel.Origin,
                SourceOfOriginInfo = productModel.OriginSource,
                Tariff             = productModel.Tariff,
                Mass               = productModel.Mass,
                VendorOne          = productModel.Vendor1,
                VendorOnePrice     = productModel.Price1,
                VendorOneCurrency  = productModel.Currency1,
                VendorTwo          = productModel.Vendor2,
                VendorTwoPrice     = productModel.Price2,
                VendorTwoCurrency  = productModel.Currency2,
                WebPage            = productModel.WebPage,
            };

            return productDb;
        }
    }
}
