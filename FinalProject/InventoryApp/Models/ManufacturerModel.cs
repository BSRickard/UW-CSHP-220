using System;

namespace InventoryApp.Models
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

        public InventoryRepository.MfgModel ToRepositoryModel()
        {
            var repositoryModel = new InventoryRepository.MfgModel
            {
                Id                 = Id,
                Name               = Name,
                OurAbbreviation    = OurAbbreviation,
                ParentCorp         = ParentCorp,
                DefaultCategory    = DefaultCategory,
                DefaultGoodsOrigin = DefaultGoodsOrigin,
                SourceOfOriginInfo = SourceOfOriginInfo,
                WebSite            = WebSite,
            };

            return repositoryModel;
        }

        public static MfgModel ToModel(InventoryRepository.MfgModel repositoryModel)
        {
            var mfgModel = new MfgModel
            {
                Id                 = repositoryModel.Id,
                Name               = repositoryModel.Name,
                OurAbbreviation    = repositoryModel.OurAbbreviation,
                ParentCorp         = repositoryModel.ParentCorp,
                DefaultCategory    = repositoryModel.DefaultCategory,
                DefaultGoodsOrigin = repositoryModel.DefaultGoodsOrigin,
                SourceOfOriginInfo = repositoryModel.SourceOfOriginInfo,
                WebSite            = repositoryModel.WebSite,
            };

            return mfgModel;
        }

        public MfgModel Clone()
        {
            return (MfgModel)MemberwiseClone();
        }
    }
}
