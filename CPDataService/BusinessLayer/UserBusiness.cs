using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using CPDataService.Mapping;
using System.Data.Entity;
namespace CPDataService.BusinessLayer
{
    public class UserBusiness : BaseBusiness
    {
        public Model.UserEntity AuthenticateUser(string email, string password)
        {
            Model.UserEntity user = null;
            var users = context.Users.Include(d => d.UserDetails).Where(a => (a.Email == email || (a.UserDetails.Any(c => c.PhoneNo == email))) && a.Password == password).ToList();
            if (users.Any())
            {
                user = users.First().ToModel();
            }
            return user;
        }

        public Model.UserEntity UpdateUser(Model.UserEntity user)
        {
            Model.UserEntity userEntity = null;
            var usersDB = context.Users.Include(d => d.UserDetails).Where(a => a.UserId == user.UserId).ToList();
            if (usersDB.Any())
            {
                var userDB = usersDB.First();
                if (!string.IsNullOrEmpty(user.DisplayName))
                {
                    userDB.UserDetails.First().DisplayName = user.DisplayName;
                }

                if (!string.IsNullOrEmpty(user.ProfileImagePath))
                {
                    userDB.UserDetails.First().ProfileImagePath = user.ProfileImagePath;
                }

                if (!string.IsNullOrEmpty(user.PostalCode))
                {
                    userDB.UserDetails.First().PostalCode = user.PostalCode;
                    userDB.UserDetails.First().LocationLong = user.LocationLng;
                    userDB.UserDetails.First().LocationLat = user.LocationLat;
                    userDB.UserDetails.First().LocationName = user.LocationName;
                    userDB.UserDetails.First().Country = user.Country;

                    
                }
               
                userDB.Email = user.Email;
                context.SaveChanges();
                userEntity = userDB.ToModel();
            }
            return userEntity;
        }

        public bool isEmailAlreadyRegistered(string email)
        {
            bool emailRegistered = false;
            var users = context.Users.Include(d => d.UserDetails).Where(a => a.Email == email);
            if (users.Any())
            {
                emailRegistered = true;
            }
            return emailRegistered;
        }

        public Model.UserEntity RegisterUser(string email, string password, string displayName)
        {
            // check if the user is already registerd , if yes then return true
            // else register new user
            Model.UserEntity returnUser = null;
            try
            {
                var authenticatedUser = isEmailAlreadyRegistered(email);
                if (!authenticatedUser)
                {
                    var user = new User
                    {
                        Email = email,
                        Password = password,
                        CreatedDate = DateTime.Now,
                        ModifiedDate = DateTime.Now
                    };                  
                  
                    context.Users.Add(user);
                    context.SaveChanges();
                    var userId = user.UserId;
                    var userDetails = new UserDetail
                    {
                        UserId = userId,
                        DisplayName = displayName,
                        JoinedDate = DateTime.Now,
                        UserType = 1

                    };

                    context.UserDetails.Add(userDetails);
                    context.SaveChanges();
                    user.UserDetails.Add(userDetails);
                    returnUser = user.ToModel();
                    returnUser.IsEmailAlreadyRegistered = false;
                }
                else
                {
                    returnUser = new Model.UserEntity();
                    returnUser.IsEmailAlreadyRegistered = true;
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }

            return returnUser;
        }
    }
}