using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using Oas.Infrastructure.Services;


namespace Oas.LV2015.Controllers
{
    [Authorize]
    [RoutePrefix("api/Account")]
    public class AccountController : ApiController
    {
        #region private fields
        private readonly IAccountService accountService =null;
        #endregion
        public AccountController(IAccountService accountService)
        {
            this.accountService = accountService;
        }
    }
}
