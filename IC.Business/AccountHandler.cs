using IC.Business.DTO;
using IC.DataAccess;
using IC.DataAccess.Infrastructure;
using IC.DTO;
using IC.Framework.Exception;
using IC.Framework.Logging;
using IC.Framework.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace IC.Business
{
    public class AccountHandler
    {
        public OperationResult VerifyCredentials(string userName, string password)
        {
            try
            {
                userName.GuardNullEmpty("User Name");
                password.GuardNullEmpty("Password");
                using (UnitOfWork uow = new UnitOfWork())
                {
                    var v = uow.GetDbInterface<UserDB>();
                    var userDetail = v.GetUser(userName);

                    ////TODO: Need to uncomment after Mobile side implementaion done.
                    ////string decryptedpassword = SIPAuthentication.Decrypt(password);

                    ////if (userDetail != null && decryptedpassword.Equals(password))
                    if (userDetail != null && userDetail.Password.Equals(password))
                    {
                        UserDTO user = new UserDTO
                        {
                            Id = userDetail.Id,
                            EmailId = userDetail.EmailId,
                            Password=userDetail.Password,
                            APNSToken=userDetail.APNSToken,
                            Name=userDetail.Name,
                            PhoneNumber=userDetail.PhoneNumber,
                            City=userDetail.City,
                            CreatedDateTime= userDetail.CreatedDateTime.GetValueOrDefault()
                        };

                        return new OperationResult
                        {
                            Success = true,
                            Data = user,
                            MCode = MessageCode.OperationSuccessful,
                            Message = "Valid User logged in"
                        };
                    }
                }

                return new OperationResult
                {
                    Success = false,
                    MCode = MessageCode.InvalidCredentials,
                    Message = "Please provide valid username/password."
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
