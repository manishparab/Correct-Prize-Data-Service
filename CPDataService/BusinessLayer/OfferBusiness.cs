using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using CPDataService.Mapping;

namespace CPDataService.BusinessLayer
{
    public class OfferBusiness : BaseBusiness
    {
        public Int64 SaveOffer(Model.ItemOffer offer)
        {
            var entity = offer.ToEntity();
            context.ItemOffers.Add(entity);
            context.SaveChanges();
            return entity.ItemOfferId;
        }

        
    }
}