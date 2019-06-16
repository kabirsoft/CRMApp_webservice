using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRMApp_datalayer.Models
{
    public class Customer_CustomerType
    {
        [Key]
        public int Id { get; set; }

        [ForeignKey("Customer")]
        public int? CustomerId { get; set; }
        public virtual Customer Customer { get; set; }

        [ForeignKey("CustomerType")]
        public int? CustomerTypeId { get; set; }
        public virtual CustomerType CustomerType { get; set; }

    }
}
