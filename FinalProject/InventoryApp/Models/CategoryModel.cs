using System;

namespace InventoryApp.Models
{
    public class CategoryModel
    {
        public int    Id { get; set; }
        public int?   Parent { get; set; }
        public string Description { get; set; }

        public InventoryRepository.CategoryModel ToRepositoryModel()
        {
            var repositoryModel = new InventoryRepository.CategoryModel
            {
                Id          = Id,
                Parent      = Parent,
                Description = Description,
            };

            return repositoryModel;
        }

        public static CategoryModel ToModel(InventoryRepository.CategoryModel repositoryModel)
        {
            var categoryModel = new CategoryModel
            {
                Id = repositoryModel.Id,
                Parent = repositoryModel.Parent,
                Description = repositoryModel.Description,
            };

            return categoryModel;
        }

        public CategoryModel Clone()
        {
            return (CategoryModel)MemberwiseClone();
        }
    }
}
