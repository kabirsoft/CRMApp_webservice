using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRMApp_datalayer.ViewModels
{
    public class CustomerViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public string PostAddress { get; set; }
        public string Telephone { get; set; }
        public string Fax { get; set; }
        public int? CompanyId { get; set; }
        public DateTime Updated { get; set; }
        public List<TypeViewModel> TypeVmList { get; set; }

    }
}
