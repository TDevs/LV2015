﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Oas.Infrastructure.Domain
{
    public class CarItem
    {
        public Guid Id { get; set; }

        public Guid CarId { get; set; }

        public Guid BusinessId { get; set; }

        public Decimal UnitPrice { get; set; }

        public string Description { get; set; }

        [ForeignKey("CarId")]
        public Car Car { get; set; }

        [ForeignKey("BusinessId")]
        public Business Business { get; set; }

        public virtual ICollection<Image> Images { get; set; }
    }
}
