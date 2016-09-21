using IC.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IC.Business.Interfaces
{
    interface IAccountHandler
    {
        OperationResult VerifyCredentials(string userName, string password);
    }
}
