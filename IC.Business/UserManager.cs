using IC.DataAccess;
using IC.DataAccess.Infrastructure;
using IC.DataModels;
using IC.DTO;
using IC.Framework.Constants;
using IC.Framework.Exception;
using IC.Framework.Logging;
using IC.Framework.Utilities;
using IC.NotificationHandler;
using Newtonsoft.Json.Linq;
using System;
using System.Reflection;

namespace IC.Business
{
    public class UserManager
    {
        public OperationResult UpdateAPNSToken(string userEmail, string apnsToken)
        {
            try
            {
                userEmail.GuardNullEmpty("User Email");
                apnsToken.GuardNullEmpty("APNS Token");
                using (UnitOfWork uow = new UnitOfWork())
                {
                    var userDB = uow.GetDbInterface<UserDB>();
                    bool isSuccess = userDB.UpdateAPNSToken(userEmail, apnsToken);
                    return new OperationResult()
                    {
                        Success = isSuccess,
                        Message = isSuccess ? "Updated successfully" : "Unable to update apns token",
                        MCode = isSuccess ? MessageCode.OperationSuccessful : MessageCode.OperationFailed
                    };
                }
            }
            catch (SIPException exception)
            {
                LogUtilities.LogException(exception, LogPriorityID.High, MethodBase.GetCurrentMethod().DeclaringType.Name, MethodBase.GetCurrentMethod().Name);
                return exception.Result;
            }
            catch (Exception exception)
            {
                LogUtilities.LogException(exception, LogPriorityID.High, MethodBase.GetCurrentMethod().DeclaringType.Name, MethodBase.GetCurrentMethod().Name);
                throw;
            }
        }

        public OperationResult RegisterUser(string EmailId, string Password, string APNSToken, string Name, string PhoneNumber, string City)
        {
            try
            {
                bool isSuccess;
                EmailId.GuardNullEmpty("EmailId");
                Password.GuardNullEmpty("Password");
                APNSToken.GuardNullEmpty("APNSToken");
                Name.GuardNullEmpty("Name");
                PhoneNumber.GuardNullEmpty("PhoneNumber");
                City.GuardNullEmpty("City");

                UserInformation user = default(UserInformation);

                using (UnitOfWork uow = new UnitOfWork())
                {
                    var userDB = uow.GetDbInterface<UserDB>();
                    isSuccess = userDB.RegisterUser(EmailId, Password, APNSToken, Name, PhoneNumber, City);

                }

                SendPushNotification pushNotify = new SendPushNotification();
                pushNotify.Send(APNSToken);

                //SendPushNotification();

                //if (isSuccess)
                //{
                //    NotificationHandler.NotificationHandler handler = new NotificationHandler.NotificationHandler();
                //    handler.Send("", user.APNSToken, "Registration success.", ICConstant.MsgForUserRegistration, "");
                //}

                return new OperationResult()
                {
                    Success = true,
                    Message = "User registered successfully",
                    MCode = MessageCode.OperationSuccessful,
                    Data = isSuccess
                };
            }
            catch (SIPException exception)
            {
                LogUtilities.LogException(exception, LogPriorityID.High, MethodBase.GetCurrentMethod().DeclaringType.Name, MethodBase.GetCurrentMethod().Name);
                return exception.Result;
            }
            catch (Exception exception)
            {
                LogUtilities.LogException(exception, LogPriorityID.High, MethodBase.GetCurrentMethod().DeclaringType.Name, MethodBase.GetCurrentMethod().Name);
                throw;
            }
        }
        
    }
}
