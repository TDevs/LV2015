using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using Oas.Infrastructure.Criteria;
using Oas.Infrastructure.Services;

namespace Oas.LV2015.Controllers
{
    public class CarRentingServiceController : ApiController
    {
        #region fields
        private readonly ICarRentingService carRentingService = null;
        #endregion
        public CarRentingServiceController(ICarRentingService vehicleService)
        {
            this.carRentingService = vehicleService;
        }


        [HttpGet]
        public HttpResponseMessage Search(CarCriteria criteria)
        {
            var result = carRentingService.SearchCar(criteria);
            var totalRecords = result.Count();
            HttpContext.Current.Response.Headers.Add("X-InlineCount", totalRecords.ToString());
            return Request.CreateResponse(HttpStatusCode.OK, result);
        }

    }
}
