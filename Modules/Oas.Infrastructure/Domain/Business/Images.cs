using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Oas.Infrastructure.Domain
{
    public class Image
    {
        [Key]
        public Guid Id { get; set; }
        public Guid BusinessId { get; set; }
        public string Caption { get; set; }
        public string Url { get; set; }
        public bool IsProfileImage { get; set; }
        [ForeignKey("BusinessId")]
        public virtual Business Business { get; set; }
    }
}