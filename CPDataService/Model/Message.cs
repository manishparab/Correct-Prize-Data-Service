using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace CPDataService.Model
{
    public class Message
    {
        [DataMember]
        public Int64 MessageId { get; set; }
        [DataMember]
        public Int64 UserFromId{ get; set; } 
        [DataMember]
        public Int64 UserTo { get; set; }
        [DataMember]
        public string message { get; set; }
        [DataMember]
        public string SendDate { get; set; }
        [DataMember]
        public bool? IsRead { get; set; }
        [DataMember]
        public bool? IsFlagged { get; set; }
        [DataMember]
        public Int64 ItemId { get; set; }
        [DataMember]
        public long? OfferItemId { get; set; }
        [DataMember]
        public string ContactImageUrl { get; set; }
        [DataMember]
        public double OfferAmount { get; set; }

        [DataMember]
        public string ContactName { get; set; }
      
    }
}