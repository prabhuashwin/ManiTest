using IC.Business;
using IC.Business.DTO;
using IC.DTO;
using IC.Framework.Exception;
using IC.Framework.Logging;
using IC.Framework.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Reflection;
using System.Web.Http;

namespace IC.WebApp.Controllers
{
    public class UserAPIController : BaseApiController
    {
        [HttpPost]
        [Route("api/user/UpdateAPNSToken/")]
        public HttpResponseMessage UpdateAPNSToken([FromBody]UserDTO user)
        {
            try
            {
                user.GuardNull("user");
                OperationResult operationResult = new OperationResult();
                operationResult = new UserManager().UpdateAPNSToken(user.EmailId, user.APNSToken);
                

                return this.Request.CreateResponse(HttpStatusCode.OK, operationResult, "text/json");
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

        [HttpPost]
        [Route("api/user/RegisterUser/")]
        public HttpResponseMessage RegisterUser([FromBody]UserDTO user)
        {
            try
            {
                OperationResult operationResult = new OperationResult();
                //Business Logic
                var userInfo = (UserDTO)operationResult.Data;
                UserManager jobManager = new UserManager();
                operationResult = new UserManager().RegisterUser(user.EmailId,user.Password,user.APNSToken,user.Name,user.PhoneNumber,user.City);

                return this.Request.CreateResponse(HttpStatusCode.OK, operationResult, "text/json");
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
