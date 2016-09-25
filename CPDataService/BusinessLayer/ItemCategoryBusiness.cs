using CPDataService.Mapping;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CPDataService.BusinessLayer
{
    public class ItemCategoryBusiness : BaseBusiness
    {
      
        public List<Model.ItemCategory> GetAllItemCategories()
        {
            return context.ItemCategories.ToList().ToModels();
        }
    }
}