using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Oas.Infrastructure.Domain;


namespace Oas.Infrastructure
{
    public class BusinessCriteria
    {
        public string Name { get; set; }
        public Guid? CategoryId { get; set; }

        public List<Guid?> CategoryIds { get; set; }

        public Guid? UserId { get; set; }

        public Status? Status { get; set; }

        public double? Latitude { get; set; }

        public double? Longtitude { get; set; }

        public double? Radius { get; set; }

        public int? Skip { get; set; }

        public int? Take { get; set; }

        public string OrderBy { get; set; }

        public bool IsListing { get; set; }

        public string City { get; set; }

        public string State { get; set; }

        public string Country { get; set; }
    }
}
