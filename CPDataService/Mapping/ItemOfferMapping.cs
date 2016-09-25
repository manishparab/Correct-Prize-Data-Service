using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CPDataService.Mapping
{
    public static class ItemOfferMapping
    {
        public static ItemOffer ToEntity(this Model.ItemOffer model)
        {
            ItemOffer offer = new ItemOffer()
            {
                Amount = model.Amount,
                ItemId = model.ItemId,
                UserId = model.UserFromId,
                CreatedDate = DateTime.Now
            };
            return offer;
        }
    }
}