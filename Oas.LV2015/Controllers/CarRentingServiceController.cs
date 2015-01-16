using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using Oas.Infrastructure.Criteria;
using Oas.Infrastructure.Domain;
using Oas.Infrastructure.Services;

namespace Oas.LV2015.Controllers
{
    public class CarRentingServiceController : ApiController
    {
        #region fields

        private readonly ICarRentingService carRentingService = null;

        #endregion

        public CarRentingServiceController(ICarRentingService carRentingService)
        {
            this.carRentingService = carRentingService;
        }

        #region Car management

        [HttpGet]
        public HttpResponseMessage Search(CarCriteria criteria)
        {
            var result = carRentingService.SearchCar(criteria);
            var totalRecords = result.Count();
            HttpContext.Current.Response.Headers.Add("X-InlineCount", totalRecords.ToString());
            return Request.CreateResponse(HttpStatusCode.OK, result);
        }

        [HttpGet]
        public HttpResponseMessage GetCar(Guid id)
        {
            var car = carRentingService.GetCar(id);
            return Request.CreateResponse(HttpStatusCode.OK, car);
        }

        [HttpPost]
        public HttpResponseMessage AddCar([FromBody]Car car)
        {
            var opStatus = carRentingService.AddCar(car);
            if (opStatus.Status)
            {
                var response = Request.CreateResponse<Car>(HttpStatusCode.Created, car);
                string uri = Url.Link("DefaultApi", new { id = car.Id });
                response.Headers.Location = new Uri(uri);
                return response;
            }
            return Request.CreateErrorResponse(HttpStatusCode.NotFound, opStatus.ExceptionMessage);
        }

        [HttpPut]
        public HttpResponseMessage UpdateCar(int id, [FromBody]Car car)
        {
            var opStatus = carRentingService.UpdateCar(car);
            if (opStatus.Status)
            {
                return Request.CreateResponse<Car>(HttpStatusCode.Accepted, car);
            }
            return Request.CreateErrorResponse(HttpStatusCode.NotModified, opStatus.ExceptionMessage);
        }

        [HttpDelete]
        public HttpResponseMessage DeleteCar(Guid id)
        {
            var opStatus = carRentingService.DeleteCar(id);

            if (opStatus.Status)
            {
                return Request.CreateResponse(HttpStatusCode.OK);
            }
            else
            {
                return Request.CreateErrorResponse(HttpStatusCode.NotFound, opStatus.ExceptionMessage);
            }
        }

        #endregion

    }
}
