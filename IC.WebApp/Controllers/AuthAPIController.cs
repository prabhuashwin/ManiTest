using IC.Business;
using IC.Business.DTO;
using IC.DTO;
using IC.Framework.Exception;
using IC.Framework.Logging;
using IC.Framework.Utilities;
using System;
using System.Web;
using System.Net;
using System.Net.Http;
using System.Reflection;
using System.Web.Http;

namespace IC.WebApp.Controllers 
{
    public class AuthAPIController : BaseApiController
    {
        [HttpPost]
        [Route("api/authorization/login/")]
        public HttpResponseMessage Login([FromBody]UserDTO user)
        {
            try
            {
                OperationResult operationResult = new OperationResult();
                AccountHandler accountHandler = new AccountHandler();
                user.GuardNull("User Details");
                var result = accountHandler.VerifyCredentials(user.EmailId, user.Password);
                if (result.Success)
                {
                    var userDetails = (UserDTO)result.Data;
                    if (userDetails.UserType != UserType.Administrator)
                    {
                        operationResult.Success = result.Success;
                        operationResult.MCode = result.MCode;
                        operationResult.Message = result.Message;
                        //var token = CipherHandler.GenerateHashWithSalt(user.UserName + user.Password);
                        //JobManager jobManager = new JobManager();
                        ////bool isAnyEmergencyAvailable = userDetails.UserType == UserType.Mechanic ? jobManager.IsAnyEmergency(userDetails.UserID) : false;
                        operationResult.Data = new { UserInfo = result.Data };
                    }
                    else
                    {
                        operationResult.Data = null;
                        operationResult.MCode = MessageCode.InvalidCredentials;
                        operationResult.Message = "Please provide valid username/password.";
                        operationResult.Success = false;
                    }
                }
                else
                {
                    operationResult = result;
                }

                return Request.CreateResponse(HttpStatusCode.OK, operationResult, "text/json");
            }
            catch (SIPException exception)
            {
                LogUtilities.LogException(exception, LogPriorityID.High, MethodBase.GetCurrentMethod().DeclaringType.Name, MethodBase.GetCurrentMethod().Name);
                return this.Request.CreateResponse(HttpStatusCode.OK, exception.Result, "text/json");
            }
            catch (Exception exception)
            {
                LogUtilities.LogException(exception, LogPriorityID.High, MethodBase.GetCurrentMethod().DeclaringType.Name, MethodBase.GetCurrentMethod().Name);
                return this.GetExceptionAsJsonResponse(exception);
            }
        }
    }
}
