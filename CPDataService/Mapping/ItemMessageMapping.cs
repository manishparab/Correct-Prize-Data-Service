using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CPDataService.Mapping
{
    public static class ItemMessageMapping
    {
        public static Message ToEntity(this CPDataService.Model.Message message)
        {
            Message entity = new Message()
            {
                UserIdFrom = message.UserFromId,
                UserIdTo = message.UserTo,
                Messge = message.message,
                SendDate = DateTime.Now,
                IsRead = message.IsRead.GetValueOrDefault(),
                IsFlagged = message.IsFlagged.GetValueOrDefault(),
                ItemId = message.ItemId,
                ItemOfferId = message.OfferItemId.HasValue ? message.OfferItemId : (long?)null
            };

            return entity;

        }

        public static CPDataService.Model.Message ToModel(this Message entity, double bestOfferAmount =0)
        {
            CPDataService.Model.Message model = new Model.Message()
            {
                UserFromId = entity.UserIdFrom,
                UserTo = entity.UserIdTo,
                message = entity.Messge,
                SendDate = entity.SendDate.ToString("MM/dd/yyyy hh:mm:ss"),
                IsRead = entity.IsRead,
                IsFlagged = entity.IsFlagged,
                ItemId = entity.ItemId,
                ContactImageUrl = entity.User ==  null ? string.Empty : entity.User.UserDetails.First().ProfileImagePath,
                OfferAmount = entity.ItemOffer == null ? 0 : entity.ItemOffer.Amount,
                ContactName = entity.User == null? string.Empty : entity.User.UserDetails.First().DisplayName
            };
            return model;
        }

        public static List<CPDataService.Model.Message> ToModels(this List<Message> entities)
        {
            var lst = new List<CPDataService.Model.Message>();
            foreach (var item in entities)
            {
                lst.Add(item.ToModel());
            }
            return lst;
        }
    }
}