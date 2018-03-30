using System;

namespace InventoryApp.Models
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

        public InventoryRepository.ProductModel ToRepositoryModel()
        {
            var repositoryModel = new InventoryRepository.ProductModel
            {
                Id            = Id,
                OurPartNumber = OurPartNumber,
                Description   = Description,
                Status        = Status,
                Manufacturer  = Manufacturer,
                MfgPartNumber = MfgPartNumber,
                MfgBarCode    = MfgBarCode,
                Unit          = Unit,
                Category      = Category,
                Origin        = Origin,
                OriginSource  = OriginSource,
                Tariff        = Tariff,
                Mass          = Mass,
                Vendor1       = Vendor1,
                Price1        = Price1,
                Currency1     = Currency1,
                Vendor2       = Vendor2,
                Price2        = Price2,
                Currency2     = Currency2,
                WebPage       = WebPage,
            };

            return repositoryModel;
        }

        public static ProductModel ToModel(InventoryRepository.ProductModel repositoryModel)
        {
            var productModel = new ProductModel
            {
                Id            = repositoryModel.Id,
                OurPartNumber = repositoryModel.OurPartNumber,
                Description   = repositoryModel.Description,
                Status        = repositoryModel.Status,
                Manufacturer  = repositoryModel.Manufacturer,
                MfgPartNumber = repositoryModel.MfgPartNumber,
                MfgBarCode    = repositoryModel.MfgBarCode,
                Unit          = repositoryModel.Unit,
                Category      = repositoryModel.Category,
                Origin        = repositoryModel.Origin,
                OriginSource  = repositoryModel.OriginSource,
                Tariff        = repositoryModel.Tariff,
                Mass          = repositoryModel.Mass,
                Vendor1       = repositoryModel.Vendor1,
                Price1        = repositoryModel.Price1,
                Currency1     = repositoryModel.Currency1,
                Vendor2       = repositoryModel.Vendor2,
                Price2        = repositoryModel.Price2,
                Currency2     = repositoryModel.Currency2,
                WebPage       = repositoryModel.WebPage,
            };

            return productModel;
        }

        public ProductModel Clone()
        {
            return (ProductModel)MemberwiseClone();
        }
    }
}
