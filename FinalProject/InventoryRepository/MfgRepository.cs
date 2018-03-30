using System.Collections.Generic;
using System.Linq;
using InventoryDB;

namespace InventoryRepository
{
    public class MfgModel
    {
        public int    Id { get; set; }
        public string Name { get; set; }
        public string OurAbbreviation { get; set; }
        public string ParentCorp { get; set; }
        public int    DefaultCategory { get; set; }
        public string DefaultGoodsOrigin { get; set; }
        public int?   SourceOfOriginInfo { get; set; }
        public string WebSite { get; set; }
    }

    public partial class InventoryRepository
    {
        public MfgModel Add(MfgModel mfgModel)
        {
            var mfgDb = ToDbModel(mfgModel);

            DatabaseManager.Instance.Manufacturers.Add(mfgDb);
            DatabaseManager.Instance.SaveChanges();

            mfgModel = new MfgModel
            {
                Id                 = mfgDb.Id,
                Name               = mfgDb.Name,
                OurAbbreviation    = mfgDb.OurAbbreviation,
                ParentCorp         = mfgDb.ParentCorp,
                DefaultCategory    = mfgDb.DefaultCategory,
                DefaultGoodsOrigin = mfgDb.DefaultGoodsOrigin,
                SourceOfOriginInfo = mfgDb.SourceOfOriginInfo,
                WebSite            = mfgDb.WebSite,
            };
            return mfgModel;
        }

        public List<MfgModel> GetAllManufacturers()
        {
            // Use .Select() to map the database contacts to MfgModel
            var items = DatabaseManager.Instance.Manufacturers
                .Select(t => new MfgModel
                {
                  Id                 = t.Id,
                  Name               = t.Name,
                  OurAbbreviation    = t.OurAbbreviation,
                  ParentCorp         = t.ParentCorp,
                  DefaultCategory    = t.DefaultCategory,
                  DefaultGoodsOrigin = t.DefaultGoodsOrigin,
                  SourceOfOriginInfo = t.SourceOfOriginInfo,
                  WebSite            = t.WebSite,
                }).ToList();

            return items;
        }

        public bool Update(MfgModel mfgModel)
        {
            var original = DatabaseManager.Instance.Manufacturers.Find(mfgModel.Id);

            if (original != null)
            {
                DatabaseManager.Instance.Entry(original).CurrentValues.SetValues(ToDbModel(mfgModel));
                DatabaseManager.Instance.SaveChanges();
            }

            return false;
        }

        public bool RemoveMfg(int mfgId)
        {
            var items = DatabaseManager.Instance.Manufacturers.Where(t => t.Id == mfgId);

            if (0 == items.Count())
            {
                return false;
            }

            DatabaseManager.Instance.Manufacturers.Remove(items.First());
            DatabaseManager.Instance.SaveChanges();

            return true;
        }

        private Manufacturer ToDbModel(MfgModel mfgModel)
        {
            var mfgDb = new Manufacturer
            {
                Id                 = mfgModel.Id,
                Name               = mfgModel.Name,
                OurAbbreviation    = mfgModel.OurAbbreviation,
                ParentCorp         = mfgModel.ParentCorp,
                DefaultCategory    = mfgModel.DefaultCategory,
                DefaultGoodsOrigin = mfgModel.DefaultGoodsOrigin,
                SourceOfOriginInfo = mfgModel.SourceOfOriginInfo,
                WebSite            = mfgModel.WebSite,
            };

            return mfgDb;
        }
    }
}
