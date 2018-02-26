using PodcastFeedGenerator.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PodcastFeedGenerator.Repositories
{
    public class CategoryRepository
    {
        private ApplicationDbContext context;

        public CategoryRepository(ApplicationDbContext context)
        {
            this.context = context;
        }

        public Category Create(string parentCategory, string subCategory)
        {
            var category = new Category()
            {
                ParentCategory = parentCategory,
                Subcategory = subCategory ?? String.Empty,
            };

            context.Categories.Add(category);
            context.SaveChanges();

            return category;
        }

        public Category Find(int categoryId)
        {
            return context.Categories.Find(categoryId);
        }

        public Category Find(int? categoryId)
        {
            if (categoryId.HasValue == false)
            {
                return null;
            }

            return Find(categoryId.Value);
        }

        public Category FindByName(string parentCategory, string subCategory = null)
        {
            Category foundCategory;

            if (String.IsNullOrEmpty(subCategory))
            {
                foundCategory = context.Categories.FirstOrDefault(c => c.ParentCategory == parentCategory);
            }
            else
            {
                foundCategory = context.Categories.FirstOrDefault(c => c.ParentCategory == parentCategory && c.Subcategory == subCategory);
            }

            return foundCategory;
        }

        public Category FindOrCreate(string parentCategory, string subCategory = null)
        {
            if (String.IsNullOrEmpty(parentCategory))
            {
                return null;
            }

            Category foundCategory = FindByName(parentCategory, subCategory);

            if (foundCategory == null)
            {
                foundCategory = Create(parentCategory, subCategory);
            }

            return foundCategory;
        }

        private Category Create(Category entity)
        {
            throw new NotImplementedException();
        }

        private Category Update(Category entity)
        {
            throw new NotImplementedException();
        }

        public Category CreateOrUpdate(Category entity)
        {
            if (entity.CategoryID == 0)
            {
                return Create(entity);
            }
            else
            {
                return Update(entity);
            }
        }
    }
}