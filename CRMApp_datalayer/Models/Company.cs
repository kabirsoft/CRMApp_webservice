using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRMApp_datalayer.Models
{
    public class Company
    {
        [Key]
        public int Id { get; set; }
        [Required(ErrorMessage ="Name is required")]
        public string Name { get; set; }
        [Required(ErrorMessage ="Address is required")]
        public string Address { get; set; }
        public string PostAddress { get; set; }
        public string Telephone { get; set; }

        [DataType(DataType.EmailAddress, ErrorMessage = "Email address is not valid")]
        [Required(ErrorMessage = "Email is required")]
        public string Email { get; set; }

        public DateTime Created { get; set; } = DateTime.Now;
        public DateTime Updated { get; set; } = DateTime.Now;

        //One company has many customer
        public virtual ICollection<Customer> Customers { get; set; }
    }
}
