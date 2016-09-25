using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace CPDataService.Model
{
    [DataContract]
    public class Item
    {
        [DataMember]
        public string ItemId { get; set; }

        [DataMember]
        public string UserId { get; set; }
        [DataMember]
        public string Name { get; set; }
        [DataMember]
        public string Description { get; set; }
        [DataMember]
        public string Cost { get; set; }
        //[DataMember]
        //public string AvailableQunatity { get; set; }
        [DataMember]
        public string IsNegotiable { get; set; }
        [DataMember]
        public string LocationLat { get; set; }
        [DataMember]
        public string LocationLong { get; set; }
        [DataMember]
        public string Condition { get; set; }

        [DataMember]
        public string ConditionDesc { get; set; }

        [DataMember]
        public string DeliveryType { get; set; }

        [DataMember]
        public string DeliveryTypeDesc { get; set; }

        [DataMember]
        public string PostalCode { get; set; }

        [DataMember]
        public string Category { get; set; }

        [DataMember]
        public string CategoryDesc { get; set; }

        //
        [DataMember]
        public string DatePosted { get; set; }

        [DataMember]
        public bool IsSold { get; set; }

        [DataMember]
        public List<string> ItemImagePaths { get; set; }

        [DataMember]
        public double BestOfferAmount { get; set;}

        [DataMember]
        public UserEntity ItemUser { get; set; }

        [DataMember]
        public string LocationName { get; set; }
       
    }
}