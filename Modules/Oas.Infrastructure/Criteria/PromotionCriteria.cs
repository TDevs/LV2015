using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Oas.Infrastructure.Domain;


namespace Oas.Infrastructure
{
    public class PromotionCriteria
    {
        public string Name { get; set; }

        public Status? Status { get; set; }

        public double? Latitude { get; set; }

        public double? Longtitude { get; set; }

        public double? Radius { get; set; }

        public int? Skip { get; set; }
        
        public int? Take { get; set; }

    }
}
