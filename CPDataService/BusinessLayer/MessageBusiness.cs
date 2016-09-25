using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using CPDataService.Mapping;
using System.Data.Entity;
namespace CPDataService.BusinessLayer
{
    public class MessageBusiness : BaseBusiness
    {
        public int SendMessage(CPDataService.Model.Message message)
        {
            var entity= message.ToEntity();
            context.Messages.Add(entity);
            ItemBusiness itemBusiness = new ItemBusiness();
            itemBusiness.AddItemBuying(message.ItemId, message.UserFromId);
            return context.SaveChanges();
        }

        public List<CPDataService.Model.Message> GetAllReceivedMessage(long toUserId)
        {
           List<CPDataService.Model.Message> message = new List<Model.Message>();
           var messageforUser = context.Messages.Where(a => a.UserIdTo == toUserId).ToList();
           var groupByMessage = messageforUser.GroupBy(a => new { a.ItemId, a.UserIdFrom });
           foreach (var messages in groupByMessage)
           {
              message.Add(messages.OrderByDescending(a => a.SendDate).First().ToModel());
           }
           return message;
        }

        public List<CPDataService.Model.Message> GetMessagesForItemSelling(long itemId, long toUserId)
        {
           List<CPDataService.Model.Message> message = new List<Model.Message>();
           var messageforUser = context.Messages.Include(a =>a.ItemOffer).Include(c=>c.User)
               .Where(a => a.UserIdTo == toUserId && a.ItemId == itemId).ToList();
           
            //find best offer if Any
           //var bestOfferAmount = messageforUser.Max(a => a.ItemOffer.Amount);
           var groupByMessage = messageforUser.GroupBy(a => new { a.ItemId, a.UserIdFrom });

           foreach (var messages in groupByMessage)
           {
               message.Add(messages.OrderByDescending(a => a.SendDate).First().ToModel());
           }
           return message;
        }

        public List<CPDataService.Model.Message> GetMessagesForItemBuying( long itemId,long fromUserId)
        {
            List<CPDataService.Model.Message> message = new List<Model.Message>();
            var messageforUser = context.Messages.Where(a => (a.UserIdFrom == fromUserId || a.UserIdFrom == fromUserId )  && a.ItemId == itemId).ToList();
            if (messageforUser.Any()) 
            {
                message = messageforUser.ToModels();
            }
            return message;
        }

        public List<CPDataService.Model.Message> GetMessagthread(long toUserId, long fromUserId,long itemId)
        {
            List<CPDataService.Model.Message> message = new List<Model.Message>();
            var messageforUser = context.Messages.Where(a => (a.UserIdFrom == fromUserId || a.UserIdFrom == toUserId) && (a.UserIdTo == toUserId || a.UserIdTo == fromUserId) && a.ItemId == itemId).ToList();
            if (messageforUser.Any())
            {
                message = messageforUser.ToModels();
            }
            return message;
        }

        public List<CPDataService.Model.Message> GetMessagthread(long messageId)
        {
            List<CPDataService.Model.Message> message = new List<Model.Message>();
            var messageforUser = context.Messages.Where(a => a.MessgeId ==  messageId).ToList();
            if (messageforUser.Any())
            {
                message = messageforUser.ToModels();
            }
            return message;
        }


    }
}