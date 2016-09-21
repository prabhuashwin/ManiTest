using IC.DataAccess.Interfaces;
using IC.DataModels;
using IC.DTO;
using IC.Framework.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace IC.DataAccess
{
    public class UserDB : IUserDB
    {

        private IndianChopstixEntities dbContext;

        /// <summary>
        /// Initializes a new instance of the UserDB class
        /// </summary>
        /// <param name="dbContext">The data base Context</param>
        public UserDB(IndianChopstixEntities dbContext)
        {
            this.dbContext = dbContext;
        }

        public UserInformation GetUser(string userName)
        {
            try
            {
                UserInformation userDetail = this.dbContext.UserInformations.Where(a => a.EmailId.Equals(userName)).FirstOrDefault();
                return userDetail;
            }
            catch (Exception exception)
            {
                LogUtilities.LogException(exception, LogPriorityID.High, MethodBase.GetCurrentMethod().DeclaringType.Name, MethodBase.GetCurrentMethod().Name);
                throw;
            }
        }

        public bool UpdateAPNSToken(string userEmail, string apnsToken)
        {
            try
            {
                var user = this.dbContext.UserInformations.Where(a => a.EmailId == userEmail).FirstOrDefault();
                user.APNSToken = apnsToken;
                this.dbContext.Entry(user).State = System.Data.Entity.EntityState.Modified;
                return this.dbContext.SaveChanges() > 0;
            }
            catch (Exception exception)
            {
                LogUtilities.LogException(exception, LogPriorityID.High, MethodBase.GetCurrentMethod().DeclaringType.Name, MethodBase.GetCurrentMethod().Name);
                throw;
            }
        }

        public bool RegisterUser(string EmailId, string Password, string APNSToken, string Name, string PhoneNumber, string City)
        {
            try
            {
                UserInformation userInfoDB = new UserInformation();
                userInfoDB.EmailId = EmailId;
                userInfoDB.Password = Password;
                userInfoDB.APNSToken = APNSToken;
                userInfoDB.Name = Name;
                userInfoDB.PhoneNumber = PhoneNumber;
                userInfoDB.City = City;
                userInfoDB.CreatedDateTime = DateTime.UtcNow;
                this.dbContext.Entry(userInfoDB).State = System.Data.Entity.EntityState.Added;
                return this.dbContext.SaveChanges() > 0;
            }
            catch (Exception exception)
            {
                LogUtilities.LogException(exception, LogPriorityID.High, MethodBase.GetCurrentMethod().DeclaringType.Name, MethodBase.GetCurrentMethod().Name);
                throw;
            }
        }
    }
}
