using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CPDataService.Mapping
{
    public static class UserMapping
    {
        public static Model.UserEntity ToModel(this User user)
        {
            UserDetail userDetail = user.UserDetails.First();
            Model.UserEntity userentity = new Model.UserEntity()
            {
                UserId = user.UserId,
                UserTypeId = userDetail.UserType,
                DisplayName = userDetail.DisplayName,
                Email = user.Email,
                LocationLat = userDetail.LocationLat,
                LocationLng = userDetail.LocationLong,
                PostalCode = userDetail.PostalCode,
                LocationName = userDetail.LocationName,
                Country = userDetail.Country,
                ProfileImagePath = userDetail.ProfileImagePath,
                CreatedDate = user.CreatedDate.ToString("MM/dd/yyyy")
            };
            return userentity;
        }

     
    }
}