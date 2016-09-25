﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace CPDataService.Model
{
    [DataContract]
    public class ItemCategory
    {
        [DataMember]
        public string Name { get; set; }

        [DataMember]
        public int Id { get; set; }
    }
}