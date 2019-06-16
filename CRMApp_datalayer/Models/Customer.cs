using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRMApp_datalayer.Models
{
    public class Customer
    {
        [Key]
        public int Id { get; set; }
        [Required(ErrorMessage = "Name is required")]
        public string Name { get; set; }        
        [Required(ErrorMessage = "Address is required")]
        public string Address { get; set; }
        public string PostAddress { get; set; }
        public string Telephone { get; set; }
        public string Fax { get; set; }
        public DateTime Created { get; set; } = DateTime.Now;
        public DateTime Updated { get; set; } = DateTime.Now;

        //Cusotmer belongs to One company
        [ForeignKey("Company")]
        public int? CompanyId { get; set; }
        public virtual Company Company { get; set; }

        //One customer has many contact
        public virtual ICollection<Contact> Contacts { get; set; }

        //One customer has many type
        public virtual ICollection<Customer_CustomerType> Customer_CustomerTypes{ get; set; }





    }
}
