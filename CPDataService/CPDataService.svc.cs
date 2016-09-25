using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Activation;
using System.ServiceModel.Web;
using System.Text;
using CPDataService.BusinessLayer;
using CPDataService.Model;
using Newtonsoft.Json;
using System.Device.Location;
namespace CPDataService
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "Service1" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select Service1.svc or Service1.svc.cs at the Solution Explorer and start debugging.
    [ServiceContract(Namespace = "")]
    [AspNetCompatibilityRequirements(RequirementsMode = AspNetCompatibilityRequirementsMode.Allowed)]
    public class ServiceCpData
    {
        [WebGet(UriTemplate = "GetData?value={value}", ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Wrapped)]
        [OperationContract]
        public string GetData(int value)
        {

            return string.Format("You entered: {0}", value);
        }
        [WebGet(UriTemplate = "AuthenticateUser?authToken={authToken}&Password={Password}", 
            ResponseFormat = WebMessageFormat.Json, 
            BodyStyle = WebMessageBodyStyle.WrappedRequest)]
        [OperationContract]
        public UserEntity AuthenticateUser(string authToken, string password)
        {
            UserEntity entity = new UserEntity();
            try
            {
              
                using (var business = new UserBusiness())
                {
                    entity = business.AuthenticateUser(authToken, password);
                    //if (retUser != null)
                    //{
                    //    entity.UserId = retUser.UserId;
                    //}
                }
               
            }
            catch (Exception exception)
            {

                string error = exception.Message;
            }
            return entity;
        }

        [OperationContract]
        [WebInvoke(Method = "GET",
           UriTemplate = "GetItemCategory",
            ResponseFormat = WebMessageFormat.Json,
            RequestFormat = WebMessageFormat.Json,
            BodyStyle = WebMessageBodyStyle.Bare)]
        public List<Model.ItemCategory> GetItemCategory()
        {
            List<Model.ItemCategory> itemCategories = new List<Model.ItemCategory>();
            using (ItemCategoryBusiness business =  new ItemCategoryBusiness())
            {
               itemCategories =  business.GetAllItemCategories();

            }

            return itemCategories;
        }


        [OperationContract]
        [WebInvoke(Method = "POST",
           UriTemplate = "RegisterUser",
            ResponseFormat = WebMessageFormat.Json,
            RequestFormat = WebMessageFormat.Json,
            BodyStyle = WebMessageBodyStyle.Bare)]
        public UserEntity RegisterUser(UserEntity user)
        {
            UserEntity entity = new UserEntity();
            using (var business = new UserBusiness())
            {

                var retUser = business.RegisterUser(user.Email, user.Password, user.DisplayName);
                if (retUser != null)
                {
                    entity = retUser;
                }

            }
            return entity;
        }


        [OperationContract]
        [WebInvoke(Method = "POST",
           UriTemplate = "EditUser",
            ResponseFormat = WebMessageFormat.Json,
            RequestFormat = WebMessageFormat.Json,
            BodyStyle = WebMessageBodyStyle.Bare)]
        public UserEntity EditUser(UserEntity user)
        {
            UserEntity entity = new UserEntity();
            using (var business = new UserBusiness())
            {

                var retUser = business.UpdateUser(user);
                if (retUser != null)
                {
                    entity = retUser;
                }

            }
            return entity;
        }



        [OperationContract]
        [WebInvoke(Method = "POST",
           UriTemplate = "GetItemDetails",
            ResponseFormat = WebMessageFormat.Json,
            RequestFormat = WebMessageFormat.Json,
            BodyStyle = WebMessageBodyStyle.WrappedRequest)]
        public Item GetItemDetails(string itemId)
        {
            Item entity = new Item();
            using (var business = new ItemBusiness())
            {

                var items = business.GetItem(long.Parse(itemId));
                if (items != null)
                {
                    entity = items;
                }

            }
            return entity;
        }


        [OperationContract]
        [WebInvoke(Method = "POST",
           UriTemplate = "GetAllItems",
            ResponseFormat = WebMessageFormat.Json,
            RequestFormat = WebMessageFormat.Json,
            BodyStyle = WebMessageBodyStyle.WrappedRequest)]
        public List<Item> GetAllItems(long userId, int itemsToSkip, float lat, float lang, int distance, string filtertext)
        {
            List<Item> items = null;
            using (var business = new ItemBusiness())
            {

                items = business.GetAllItems(userId, itemsToSkip, lat, lang, distance, filtertext);
                
            }
            return items;
        }


        [OperationContract]
        [WebInvoke(Method = "POST",
           UriTemplate = "GetItemsForSell",
            ResponseFormat = WebMessageFormat.Json,
            RequestFormat = WebMessageFormat.Json,
            BodyStyle = WebMessageBodyStyle.WrappedRequest)]
        public List<Item> GetItemsForSell(long userId)
        {
            List<Item> items = null;
            using (var business = new ItemBusiness())
            {

               items = business.GetItemsForSell(userId);
            }
            return items;
        }


        [OperationContract]
        [WebInvoke(Method = "POST",
           UriTemplate = "GetItemsBuying",
            ResponseFormat = WebMessageFormat.Json,
            RequestFormat = WebMessageFormat.Json,
            BodyStyle = WebMessageBodyStyle.WrappedRequest)]
        public List<Item> GetItemsBuying(string userId)
        {
            List<Item> items = null;
            using (var business = new ItemBusiness())
            {

                items = business.GetBuyingItems(long.Parse(userId));


            }
            return items;
        }



        [OperationContract]
        [WebInvoke(Method = "POST",
           UriTemplate = "GetItemsWatching",
            ResponseFormat = WebMessageFormat.Json,
            RequestFormat = WebMessageFormat.Json,
            BodyStyle = WebMessageBodyStyle.WrappedRequest)]
        public List<Item> GetItemsWatching(string userId)
        {
            List<Item> items = null;
            using (var business = new ItemBusiness())
            {

                items = business.GetWatchingItems(long.Parse(userId));


            }
            return items;
        }


        [OperationContract]
        [WebInvoke(Method = "POST",
           UriTemplate = "SaveItemImages",
            ResponseFormat = WebMessageFormat.Json,
            RequestFormat = WebMessageFormat.Json,
            BodyStyle = WebMessageBodyStyle.Bare)]
        public GenericClass SaveItemImages(ItemImages itemImages)
        {
            GenericClass genericclass = new GenericClass();
            using (var business = new ItemImagesBusiness())
            {
                 genericclass.Id = business.SaveItemImages(itemImages).ToString();

            }
            return genericclass;
        }


        [OperationContract]
        [WebInvoke(Method = "POST",
           UriTemplate = "DeleteItemImages",
            ResponseFormat = WebMessageFormat.Json,
            RequestFormat = WebMessageFormat.Json,
            BodyStyle = WebMessageBodyStyle.WrappedRequest)]
        public GenericClass DeleteItemImages(long itemId)
        {
            GenericClass genericclass = new GenericClass();
            using (var business = new ItemImagesBusiness())
            {
                genericclass.Id = business.DeleteItemImages(itemId).ToString();

            }
            return genericclass;
        }

        //[WebGet(UriTemplate = "SaveItemImages?path={path}",
        //    ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Wrapped)]
        //[OperationContract]
        //public string SaveItemImages(string path)
        //{
        //    if (string.IsNullOrEmpty(path))
        //    {
        //        return null;
        //    }

        //    var paths = path.Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries);

        //    return "";

        //}

        [WebInvoke(Method = "POST",
            UriTemplate = "SaveItem",
            ResponseFormat = WebMessageFormat.Json,
            RequestFormat = WebMessageFormat.Json,
            BodyStyle = WebMessageBodyStyle.Bare)]
        [OperationContract]
        public GenericClass SaveItem(Item item)
        {
            long retValue =-1;
            using (var business = new ItemBusiness())
            {
                retValue = business.SaveItem(item);
            }

            List<GenericClass> objclass = new List<GenericClass>();
            objclass.Add(new GenericClass() { Id = retValue.ToString() });


            GenericClass obj = new GenericClass();
            obj.Id = retValue.ToString();

            //JsonSerializer serializer = new JsonSerializer();

            //return JsonConvert.SerializeObject(objclass);

            return obj;
        }


        [WebInvoke(Method = "POST",
           UriTemplate = "EditItem",
           ResponseFormat = WebMessageFormat.Json,
           RequestFormat = WebMessageFormat.Json,
           BodyStyle = WebMessageBodyStyle.Bare)]
        [OperationContract]
        public GenericClass EditItem(Item item)
        {
            long retValue = -1;
            using (var business = new ItemBusiness())
            {
                retValue = business.EditItem(item);
            }
            List<GenericClass> objclass = new List<GenericClass>();
            objclass.Add(new GenericClass() { Id = retValue.ToString() });
            GenericClass obj = new GenericClass();
            obj.Id = retValue.ToString();
            return obj;
        }

        [WebInvoke(Method = "POST",
           UriTemplate = "UpdateSellStatus",
           ResponseFormat = WebMessageFormat.Json,
           RequestFormat = WebMessageFormat.Json,
           BodyStyle = WebMessageBodyStyle.Wrapped)]
        [OperationContract]
        public GenericClass UpdateSellStatus(long itemId, bool isSold)
        {
            long retValue = -1;
            using (var business = new ItemBusiness())
            {
                retValue = business.UpdateSellStatus(itemId, isSold);
            }
            List<GenericClass> objclass = new List<GenericClass>();
            objclass.Add(new GenericClass() { Id = retValue.ToString() });
            GenericClass obj = new GenericClass();
            obj.Id = retValue.ToString();
            return obj;
        }

        [WebInvoke(Method = "GET",
            UriTemplate = "helloWorld",
            ResponseFormat = WebMessageFormat.Json,
            RequestFormat = WebMessageFormat.Json,
            BodyStyle = WebMessageBodyStyle.Wrapped)]
        [OperationContract]
         public string helloWorld()
        {
            return "Hello world";
        }


        [OperationContract]
        [WebInvoke(Method = "POST",
           UriTemplate = "SendMessage",
            ResponseFormat = WebMessageFormat.Json,
            RequestFormat = WebMessageFormat.Json,
            BodyStyle = WebMessageBodyStyle.Bare)]
        public GenericClass SendMessage(CPDataService.Model.Message message)
        {
            long retValue = -1;
            using (var business = new MessageBusiness())
            {

                retValue = business.SendMessage(message);
            }
            return new GenericClass() { Id = retValue.ToString() };
        }

        [OperationContract]
        [WebInvoke(Method = "POST",
           UriTemplate = "MakeOffer",
            ResponseFormat = WebMessageFormat.Json,
            RequestFormat = WebMessageFormat.Json,
            BodyStyle = WebMessageBodyStyle.Bare)]
        public GenericClass MakeOffer(CPDataService.Model.ItemOffer offer)
        {
            long retValue = -1;
            using (var business = new OfferBusiness())
            {

                retValue = business.SaveOffer(offer);
            }
            return new GenericClass() { Id = retValue.ToString() };
        }

    
        [OperationContract]
        [WebInvoke(Method = "POST",
           UriTemplate = "GetMessagesForItemSelling",
            ResponseFormat = WebMessageFormat.Json,
            RequestFormat = WebMessageFormat.Json,
            BodyStyle = WebMessageBodyStyle.WrappedRequest)]
        public List<Model.Message> GetMessagesForItemSelling(string itemId, string toUserId)
        {
            List<Model.Message> messages = null;
            using (var business = new MessageBusiness())
            {
                messages = business.GetMessagesForItemSelling(long.Parse(itemId), long.Parse(toUserId));
            }
            return messages;
        }

        [OperationContract]
        [WebInvoke(Method = "POST",
           UriTemplate = "GetMessagesForItemBuying",
            ResponseFormat = WebMessageFormat.Json,
            RequestFormat = WebMessageFormat.Json,
            BodyStyle = WebMessageBodyStyle.WrappedRequest)]
        public List<Model.Message> GetMessagesForItemBuying(string itemId, string fromUserId)
        {
            List<Model.Message> messages = null;
            using (var business = new MessageBusiness())
            {
                messages = business.GetMessagesForItemBuying(long.Parse(itemId), long.Parse(fromUserId));
            }
            return messages;
        }

        [OperationContract]
        [WebInvoke(Method = "POST",
           UriTemplate = "GetMessagthread",
            ResponseFormat = WebMessageFormat.Json,
            RequestFormat = WebMessageFormat.Json,
            BodyStyle = WebMessageBodyStyle.WrappedRequest)]
        public List<Model.Message> GetMessagthread(long toUserId, long fromUserId, long itemId)
        {
            List<Model.Message> messages = null;
            using (var business = new MessageBusiness())
            {
                messages = business.GetMessagthread(toUserId, fromUserId,itemId);
            }
            return messages;
        }

      

    }
}
