using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CPDataService.Mapping
{
    public static class ItemCategoryMapping
    {
        public static Model.ItemCategory ToModel(this CPDataService.ItemCategory itemCategory)
        {
            return new Model.ItemCategory() { Name = itemCategory.Name , Id = itemCategory.ItemCatId};
        }

        public static List<Model.ItemCategory> ToModels(this List<CPDataService.ItemCategory> itemCategories)
        {
            List<Model.ItemCategory> lstItemCategories = new List<Model.ItemCategory>();
            foreach (var category in itemCategories)
            {
                lstItemCategories.Add(category.ToModel());
            }

            return lstItemCategories;   
        }
        
    }
}