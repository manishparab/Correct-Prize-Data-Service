using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using CPDataService.Model;
using CPDataService.Mapping;
using System.Data.Entity;
using System.Device.Location;
using System.Data.SqlClient;
using System.Data;

namespace CPDataService.BusinessLayer
{
    public class ItemBusiness : BaseBusiness
    {
        public long SaveItem(Item item)
        {
            var itemForSell = new ItemsForSell
            {
                UserId = long.Parse(item.UserId),
                Cost = double.Parse(item.Cost),
                Name = item.Name,
                Description = item.Description.Replace("\r\n", "<br><br>").Replace("\n", "<br>"),
                DeliveryType = bool.Parse(item.DeliveryType),
                //AvailableQuantity = Int32.Parse(item.AvailableQunatity),
                DatePosted = DateTime.Now,
                CreatedDate = DateTime.Now,
                ModifiedDate = DateTime.Now,
                IsNegotialble = bool.Parse(item.IsNegotiable),
                LocationLat = string.IsNullOrEmpty(item.LocationLat) ? (double?)null : double.Parse(item.LocationLat),
                LocationLong = string.IsNullOrEmpty(item.LocationLong) ? (double?)null : double.Parse(item.LocationLong),
                PostalCodeLocation = item.LocationName,
                ItemCondition = int.Parse(item.Condition),
                ItemCatId = int.Parse(item.Category),
                PostalCode = item.PostalCode,
                IsSold = false
            };

            context.ItemsForSells.Add(itemForSell);
            context.SaveChanges();
            return itemForSell.ItemId;

        }

        public long EditItem(Item item)
        {
            ItemsForSell itmForSell = null;
            long itemID = long.Parse(item.ItemId);
            var items = context.ItemsForSells.Where(a => a.ItemId == itemID).ToList();
            if (items.Any())
            {
                itmForSell = items.First();
                //itmForSell.UserId = long.Parse(item.UserId);
                itmForSell.Cost = double.Parse(item.Cost);
                itmForSell.Name = item.Name;
                itmForSell.Description = item.Description.Replace("\r\n", "<br><br>").Replace("\n", "<br>");
                itmForSell.DeliveryType = bool.Parse(item.DeliveryType);
                //AvailableQuantity = Int32.Parse(item.AvailableQunatity);
                //itmForSell.DatePosted = DateTime.Now;
                //itmForSell.CreatedDate = DateTime.Now;
                itmForSell.ModifiedDate = DateTime.Now;
                itmForSell.IsNegotialble = bool.Parse(item.IsNegotiable);
                itmForSell.LocationLat = string.IsNullOrEmpty(item.LocationLat) ? (double?)null : double.Parse(item.LocationLat);
                itmForSell.LocationLong = string.IsNullOrEmpty(item.LocationLong) ? (double?)null : double.Parse(item.LocationLong);
                itmForSell.PostalCodeLocation = item.LocationName;
                itmForSell.ItemCondition = int.Parse(item.Condition);
                itmForSell.ItemCatId = int.Parse(item.Category);
                itmForSell.PostalCode = item.PostalCode;
                itmForSell.IsSold = item.IsSold;

            }

            context.SaveChanges();
            return itmForSell.ItemId;

        }

        public long UpdateSellStatus(long itemId, bool isSold)
        {
            ItemsForSell itmForSell = null;
            var items = context.ItemsForSells.Where(a => a.ItemId == itemId).ToList();
            if (items.Any())
            {
                itmForSell = items.First();
                itmForSell.IsSold = isSold;
            }
            context.SaveChanges();
            return itmForSell.ItemId;
        }

        public List<Item> GetAllItems(long userId, int itemsToSkip, float lat, float lang, int distance, string searchString)
        {
            var coord = new GeoCoordinate(lat, lang);
            List<Item> lstItems = null;

            var parameters = new[] { 
        new SqlParameter("@lat",lat), 
        new SqlParameter("@lng", lang) , 
        new SqlParameter("@distance", distance) ,
          new SqlParameter("@cat", DBNull.Value) ,  
        new SqlParameter("@currentuserId", userId),
        new SqlParameter("@searchString", searchString)  
    };

            var filteritems = context.Database.SqlQuery<GetfilteredItem_Result>("GetfilteredItem @lat,@lng,@distance,@cat,@currentuserId,@searchString", parameters).Select(c => c.ItemId).ToList();

            var items = context.ItemsForSells.Include(co => co.ItemCondition1).
                                            Include(co => co.ItemCategory).
                                            Include(co => co.User).
                                            Include(co => co.User.UserDetails).
                                            Where(a => filteritems.Any(c => c == a.ItemId) && !a.IsSold).ToList();
            lstItems = items.ToModels();

            return lstItems;
        }



        public List<Item> GetItemsForSell(long userId)
        {
            List<Item> lstItems = null;
            var items = context.ItemsForSells.Include(i => i.ItemCondition1).
                                             Include(i => i.ItemCategory).
                                             Include(i => i.ItemImages).
                                             Include(i => i.ItemOffers).
                                             Where(a => a.UserId == userId);
            lstItems = items.OrderBy(a => a.IsSold).ToList().ToModels();
            return lstItems;
        }

        public int AddItemBuying(long itemId, long userId)
        {
            context.ItemUserBuyings.Add(new ItemUserBuying() { ItemId = itemId, UserId = userId });
            return context.SaveChanges();
        }

        public List<Item> GetBuyingItems(long userId)
        {
            List<Item> lstItems = null;
            var itemsBuying = context.ItemUserBuyings.Where(a => a.UserId == userId).ToList();
            var itemsIds = itemsBuying.Select(b => b.ItemId).ToList();
            var items = context.ItemsForSells.Include("ItemCondition1").Include("ItemCategory").Include("ItemImages").Where(a => itemsIds.Any(c => c == a.ItemId)).ToList();
            lstItems = items.OrderBy(a => a.IsSold).ToList().ToModels();
            return lstItems;
        }

        public List<Item> GetWatchingItems(long userId)
        {
            List<Item> lstItems = null;
            var itemsWatching = context.ItemUserWatchings.Where(a => a.UserId == userId);
            var itemsIds = itemsWatching.Select(b => b.ItemId).ToList();
            var items = context.ItemsForSells.Include("ItemCondition1").Include("ItemCategory").Include("ItemImages").Where(a => itemsIds.Any(c => c == a.ItemId));
            lstItems = items.ToList().ToModels();
            return lstItems;
        }

        public Item GetItem(long itemId)
        {
            ItemsForSell item = null;

            var items = context.ItemsForSells.Include(co => co.ItemCondition1).
                                            Include(co => co.ItemCategory).
                                            Include(co => co.User).
                                            Include(co => co.User.UserDetails).
                                            Where(a => a.ItemId == itemId);
            if (items.Any())
            {
                item = items.First();
            }
            return item.ToModel();
        }
    }
}