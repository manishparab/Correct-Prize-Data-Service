using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace CPDataService.Model
{
    [DataContract]
    public class UserEntity
    {
        [DataMember]
        public long UserId { get; set; }
        [DataMember]
        public string DisplayName { get; set; }
        [DataMember]
        public string Email { get; set; }
        //[DataMember] public string Password { get; set; }

        [DataMember]
        public int UserTypeId { get; set; }

        [DataMember]
        public decimal? LocationLng { get; set; }

        [DataMember]
        public decimal? LocationLat { get; set; }

        [DataMember]
        public string PostalCode { get; set; }

        [DataMember]
        public string LocationName { get; set; }

        [DataMember]
        public string Country { get; set; }

        [DataMember]
        public string CreatedDate { get; set; }

        [DataMember]
        public string Password { get; set; }

        [DataMember]
        public string ProfileImagePath { get; set; }

        [DataMember]
        public bool IsEmailAlreadyRegistered { get; set; }

    }

}