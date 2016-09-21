using IC.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace IC.WebApp.Controllers
{
    public class BaseApiController : ApiController
    {
        /// <summary>
        /// On Exception ,Create a JSON Response {Success:false,Message:Exception.Message}
        /// </summary>
        /// <param name="exception">The exception</param>
        /// <returns>HttpResponseMessage-CreateResponse that will return the exception message HttpStatusCode.BadRequest</returns>
        public HttpResponseMessage GetExceptionAsJsonResponse(Exception exception)
        {
            OperationResult operationResult = new OperationResult();
            operationResult.Success = false;
            operationResult.Message = exception.Message;
            return this.Request.CreateResponse(HttpStatusCode.BadRequest, operationResult, "text/json");
        }      
    }
}
