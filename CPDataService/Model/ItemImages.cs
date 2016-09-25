using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace CPDataService.Model
{
    [DataContract]
    public class ItemImages
    {
       [DataMember] public string UserId;
       [DataMember]
       public string ItemId;
       [DataMember]
       public string ItemImage1;
       [DataMember]
       public string ItemImage2;
       [DataMember]
       public string ItemImage3;
       [DataMember]
       public string ItemImage4;
       [DataMember]
       public string ItemImage5;
    }
}