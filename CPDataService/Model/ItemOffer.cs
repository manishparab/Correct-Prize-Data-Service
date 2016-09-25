using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace CPDataService.Model
{
    public class ItemOffer
    {
        [DataMember]
        public long UserFromId { get; set; }

        [DataMember]
        public long ItemId { get; set; }

        [DataMember]
        public float Amount { get; set; }

        [DataMember]
        public string OfferDate { get; set; }
    }
}