using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CPDataService.Mapping
{
    public static class ItemForSellMapping
    {
        public static Model.Item ToModel(this CPDataService.ItemsForSell itemToSell)
        {
            Model.Item item = new Model.Item();
            item.ItemId = itemToSell.ItemId.ToString();
            item.UserId = itemToSell.UserId.ToString();
            item.Name = itemToSell.Name;
            item.Description = itemToSell.Description;
            item.Cost = itemToSell.Cost.ToString();
            item.IsSold = itemToSell.IsSold;
            item.Category = itemToSell.ItemCatId.ToString();
            item.CategoryDesc = itemToSell.ItemCategory != null ? itemToSell.ItemCategory.Name : null;
            item.IsNegotiable = itemToSell.IsNegotialble.ToString();
            item.LocationLat = itemToSell.LocationLat.GetValueOrDefault().ToString();
            item.LocationLong = itemToSell.LocationLong.GetValueOrDefault().ToString();
            item.LocationName = itemToSell.PostalCodeLocation;
            item.Condition = itemToSell.ItemCondition.ToString();
            item.ConditionDesc = itemToSell.ItemCondition1 != null? itemToSell.ItemCondition1.ConditionName : string.Empty;
            item.DeliveryType = itemToSell.DeliveryType.ToString();
            item.DeliveryTypeDesc = itemToSell.DeliveryType ==true ? "Deliverable" : "Pick up";
            item.DatePosted = itemToSell.DatePosted.ToString("dd/MM/yyyy");
            item.PostalCode = string.IsNullOrEmpty(itemToSell.PostalCode) ? "" : itemToSell.PostalCode;
            if (itemToSell.ItemOffers !=  null)
            {
                if (itemToSell.ItemOffers.Any())
                {
                   item.BestOfferAmount = itemToSell.ItemOffers.OrderByDescending(c => c.Amount).First().Amount;
                }
            }
            item.ItemImagePaths = itemToSell.ItemImages.Any() ? itemToSell.ItemImages.OrderBy(c=>c.ImageOrder).Select(a => a.ImagePath).ToList() : new List<string>();
            item.ItemUser = itemToSell.User.ToModel();
            return item;
        }

       
        
        public static List<Model.Item> ToModels(this List<CPDataService.ItemsForSell> itemsToSell)
        {
            List<Model.Item> items =  new List<Model.Item>();
            foreach (var item in itemsToSell)
            {
                items.Add(item.ToModel());
            }

            return items;
        }

    }
}