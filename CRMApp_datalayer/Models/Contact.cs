using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRMApp_datalayer.Models
{
    public class Contact
    {
        [Key]
        public int Id { get; set; }
        [Display(Name = "Last Name")]
        [Required(ErrorMessage = "Last name is required")]
        public string LastName { get; set; }
        [Display(Name = "First Name")]
        public string FirstName { get; set; }
        public string Title { get; set; }
        public string Telephone { get; set; }
        [Required(ErrorMessage = "Mobile is required")]
        public string Mobile { get; set; }

        [DataType(DataType.EmailAddress, ErrorMessage = "Email address is not valid")]
        [Required(ErrorMessage = "Email is required")]
        public string Email { get; set; }

        public DateTime Created { get; set; } = DateTime.Now;
        public DateTime Updated { get; set; } = DateTime.Now;

        public string FullName
        {
            get
            {
                return FirstName + " " + LastName;
            }
        }

        //One contact belongs to one customer
        [ForeignKey("Customer")]
        public int? CustomerId { get; set; }
        public virtual Customer Customer { get; set; }
    }
}
