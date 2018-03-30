using System.Collections.Generic;
using System.Linq;
using InventoryDB;

namespace InventoryRepository
{
    public class CategoryModel
    {
        public int    Id { get; set; }
        public int?   Parent { get; set; }
        public string Description { get; set; }
    }

    public partial class InventoryRepository
    {
        public CategoryModel Add(CategoryModel categoryModel)
        {
            var categoryDb = ToDbModel(categoryModel);

            DatabaseManager.Instance.Categories.Add(categoryDb);
            DatabaseManager.Instance.SaveChanges();

            categoryModel = new CategoryModel
            {
                Id          = categoryDb.Id,
                Parent      = categoryDb.Parent,
                Description = categoryDb.Description,
            };
            return categoryModel;
        }

        public List<CategoryModel> GetAllCategories()
        {
            // Use .Select() to map the database contacts to CategoryModel
            var items = DatabaseManager.Instance.Categories
                .Select(t => new CategoryModel
                {
                    Id          = t.Id,
                    Parent      = t.Parent,
                    Description = t.Description,
                }).ToList();

            return items;
        }

        public bool Update(CategoryModel categoryModel)
        {
            var original = DatabaseManager.Instance.Categories.Find(categoryModel.Id);

            if (original != null)
            {
                DatabaseManager.Instance.Entry(original).CurrentValues.SetValues(ToDbModel(categoryModel));
                DatabaseManager.Instance.SaveChanges();
            }

            return false;
        }

        public bool RemoveCategory(int categoryId)
        {
            var items = DatabaseManager.Instance.Categories.Where(t => t.Id == categoryId);

            if (0 == items.Count())
            {
                return false;
            }

            DatabaseManager.Instance.Categories.Remove(items.First());
            DatabaseManager.Instance.SaveChanges();

            return true;
        }

        private Category ToDbModel(CategoryModel categoryModel)
        {
            var categoryDb = new Category
            {
                Id          = categoryModel.Id,
                Parent      = categoryModel.Parent,
                Description = categoryModel.Description,
            };

            return categoryDb;
        }
    }
}
